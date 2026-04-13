using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Server.Repositories;
using LMS.Shared.DTOs;
using LMS.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FeedController : ControllerBase
    {
        private readonly IGenericRepository<Feed> _feedRepository;

        public FeedController(IGenericRepository<Feed> feedRepository)
        {
            _feedRepository = feedRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedDto>>> GetAllFeeds()
        {
            var feeds = await _feedRepository.GetAllAsync();
            var dtos = feeds.Select(f => new FeedDto
            {
                FeedID = f.FeedID,
                FeedTypeID = f.FeedTypeID,
                FeedName = f.FeedName,
                StockQuantity = f.StockQuantity,
                Unit = f.Unit
            });
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FeedDto>> GetFeedById(int id)
        {
            var feed = await _feedRepository.GetByIdAsync(id);
            if (feed == null) return NotFound();

            var dto = new FeedDto
            {
                FeedID = feed.FeedID,
                FeedTypeID = feed.FeedTypeID,
                FeedName = feed.FeedName,
                StockQuantity = feed.StockQuantity,
                Unit = feed.Unit
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<FeedDto>> CreateFeed([FromBody] FeedDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var feed = new Feed
            {
                FeedTypeID = dto.FeedTypeID,
                FeedName = dto.FeedName,
                StockQuantity = dto.StockQuantity,
                Unit = dto.Unit
            };

            await _feedRepository.AddAsync(feed);
            await _feedRepository.SaveChangesAsync();

            dto.FeedID = feed.FeedID;
            return CreatedAtAction(nameof(GetFeedById), new { id = feed.FeedID }, dto);
        }
    }
}
