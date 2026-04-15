using LMS.Shared.Entities;
using LMS.Server.Models;
using LMS.Server.Repositories;
using LMS.Server.Services;
using LMS.Shared.DTOs.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly IGenericRepository<Farmer> _farmerRepository;

        public AuthController(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ITokenService tokenService,
            IGenericRepository<Farmer> farmerRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
            _farmerRepository = farmerRepository;
        }

        /// <summary>
        /// Register a new farmer account
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Check if email already exists
                var existingUser = await _userManager.FindByEmailAsync(dto.Email);
                if (existingUser != null)
                    return BadRequest(new { message = "Email already registered" });

                // Create new farmer for this user
                var newFarmer = new Farmer
                {
                    FirstName = dto.FirstName ?? "User",
                    LastName = dto.LastName ?? "",
                    Email = dto.Email,
                    PhoneNumber = dto.PhoneNumber,
                    Address = dto.Address
                };

                await _farmerRepository.AddAsync(newFarmer);
                await _farmerRepository.SaveChangesAsync();

                // Create AppUser
                var appUser = new AppUser
                {
                    UserName = dto.Email,
                    Email = dto.Email,
                    FullName = $"{dto.FirstName} {dto.LastName}",
                    FarmerId = newFarmer.FarmerID
                };

                var createUserResult = await _userManager.CreateAsync(appUser, dto.Password);
                if (!createUserResult.Succeeded)
                {
                    return BadRequest(new { message = "User creation failed", errors = createUserResult.Errors });
                }

                // Assign Farmer role
                var assignRoleResult = await _userManager.AddToRoleAsync(appUser, "Farmer");
                if (!assignRoleResult.Succeeded)
                {
                    return BadRequest(new { message = "Failed to assign role", errors = assignRoleResult.Errors });
                }

                return Ok(new { message = "Registration successful", farmerId = newFarmer.FarmerID });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during registration", error = ex.Message });
            }
        }

        /// <summary>
        /// Login with email and password
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var user = await _userManager.FindByEmailAsync(dto.Email);
                if (user == null)
                    return Unauthorized(new { message = "Invalid email or password" });

                var passwordValid = await _userManager.CheckPasswordAsync(user, dto.Password);
                if (!passwordValid)
                    return Unauthorized(new { message = "Invalid email or password" });

                // Get user roles
                var roles = await _userManager.GetRolesAsync(user);

                // Generate JWT token
                var token = _tokenService.GenerateJwtToken(user, roles);

                return Ok(new AuthResponseDto
                {
                    Token = token,
                    UserID = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    FarmerId = user.FarmerId ?? 0,
                    Role = roles.FirstOrDefault() ?? "User"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred during login", error = ex.Message });
            }
        }

        /// <summary>
        /// Refresh JWT token (optional - for token refresh flow)
        /// </summary>
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] AuthResponseDto dto)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(dto.UserID);
                if (user == null)
                    return Unauthorized(new { message = "User not found" });

                var roles = await _userManager.GetRolesAsync(user);
                var newToken = _tokenService.GenerateJwtToken(user, roles);

                return Ok(new AuthResponseDto
                {
                    Token = newToken,
                    UserID = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    FarmerId = user.FarmerId ?? 0,
                    Role = roles.FirstOrDefault() ?? "User"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Token refresh failed", error = ex.Message });
            }
        }

        /// <summary>
        /// Logout (token invalidation on client side - stateless JWT)
        /// </summary>
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            // With JWT, logout is handled client-side by removing the token
            return Ok(new { message = "Logged out successfully" });
        }
    }
}
