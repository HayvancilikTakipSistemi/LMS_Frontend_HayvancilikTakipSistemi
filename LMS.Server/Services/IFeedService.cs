using System.Threading.Tasks;
using LMS.Shared.DTOs;

namespace LMS.Server.Services
{
    public interface IFeedService
    {
        Task<FeedRecordDto> AddFeedRecordAsync(FeedRecordDto dto);
    }
}
