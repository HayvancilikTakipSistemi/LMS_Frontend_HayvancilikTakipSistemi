using Blazored.LocalStorage;
using LMS.Client.Auth;
using LMS.Shared.DTOs.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Net.Http.Headers;

namespace LMS.Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient, AuthenticationStateProvider authStateProvider, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
        }

        /// <summary>
        /// Register a new farmer account
        /// </summary>
        public async Task<bool> RegisterAsync(string firstName, string lastName, string email, string password, string phoneNumber = "", string address = "")
        {
            try
            {
                var registerDto = new AuthDto
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    Password = password,
                    PhoneNumber = phoneNumber,
                    Address = address
                };

                var response = await _httpClient.PostAsJsonAsync("api/auth/register", registerDto);
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Registration error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Login with email and password
        /// </summary>
        public async Task<bool> LoginAsync(string email, string password)
        {
            try
            {
                var loginDto = new AuthDto
                {
                    Email = email,
                    Password = password
                };

                var response = await _httpClient.PostAsJsonAsync("api/auth/login", loginDto);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
                    if (result?.Token != null)
                    {
                        // Store token in localStorage
                        await _localStorage.SetItemAsync("token", result.Token);
                        await _localStorage.SetItemAsync("userId", result.UserID);
                        await _localStorage.SetItemAsync("email", result.Email);
                        await _localStorage.SetItemAsync("fullName", result.FullName);
                        await _localStorage.SetItemAsync("farmerId", result.FarmerId.ToString());
                        await _localStorage.SetItemAsync("role", result.Role);

                        // Notify auth state provider
                        ((CustomAuthStateProvider)_authStateProvider).NotifyUserAuthentication(result.Token);
                        
                        // Set authorization header for future requests
                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
                        
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login error: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Logout and clear stored credentials
        /// </summary>
        public async Task LogoutAsync()
        {
            try
            {
                await _httpClient.PostAsync("api/auth/logout", null);
                
                // Clear localStorage
                await _localStorage.RemoveItemAsync("token");
                await _localStorage.RemoveItemAsync("userId");
                await _localStorage.RemoveItemAsync("email");
                await _localStorage.RemoveItemAsync("fullName");
                await _localStorage.RemoveItemAsync("farmerId");
                await _localStorage.RemoveItemAsync("role");

                // Notify auth state provider
                ((CustomAuthStateProvider)_authStateProvider).NotifyUserLogout();
                
                // Clear authorization header
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Logout error: {ex.Message}");
            }
        }

        /// <summary>
        /// Get stored JWT token from localStorage
        /// </summary>
        public async Task<string?> GetTokenAsync()
        {
            try
            {
                return await _localStorage.GetItemAsync<string>("token");
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Check if user is authenticated
        /// </summary>
        public async Task<bool> IsAuthenticatedAsync()
        {
            try
            {
                var token = await GetTokenAsync();
                return !string.IsNullOrEmpty(token);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Refresh JWT token
        /// </summary>
        public async Task RefreshTokenAsync()
        {
            try
            {
                var userId = await _localStorage.GetItemAsync<string>("userId");
                var email = await _localStorage.GetItemAsync<string>("email");
                var fullName = await _localStorage.GetItemAsync<string>("fullName");
                var farmerIdStr = await _localStorage.GetItemAsync<string>("farmerId");
                var role = await _localStorage.GetItemAsync<string>("role");

                if (string.IsNullOrEmpty(userId))
                    return;

                var currentAuthResponse = new AuthResponseDto
                {
                    UserID = userId,
                    Email = email,
                    FullName = fullName,
                    FarmerId = int.TryParse(farmerIdStr, out var id) ? id : 0,
                    Role = role
                };

                var response = await _httpClient.PostAsJsonAsync("api/auth/refresh", currentAuthResponse);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
                    if (result?.Token != null)
                    {
                        await _localStorage.SetItemAsync("token", result.Token);
                        ((CustomAuthStateProvider)_authStateProvider).NotifyUserAuthentication(result.Token);
                        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.Token);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Token refresh error: {ex.Message}");
            }
        }
    }
}
