using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Server.Repositories;
using LMS.Server.Services;
using LMS.Shared.DTOs;
using LMS.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FeedRecordController : ControllerBase
    {
        private readonly IFeedService _feedService;

        public FeedRecordController(IFeedService feedService)
        {
            _feedService = feedService;
        }

        [HttpPost]
        public async Task<ActionResult<FeedRecordDto>> CreateFeedRecord([FromBody] FeedRecordDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                var createdRecord = await _feedService.AddFeedRecordAsync(dto);
                return Ok(createdRecord);
            }
            catch (InvalidOperationException ex)
            {
                // Yetersiz stok gibi iş kuralları ihlalleri
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
