using System;
using System.Threading.Tasks;
using LMS.Server.Repositories;
using LMS.Shared.DTOs;
using LMS.Shared.Entities;

namespace LMS.Server.Services
{
    public class FeedService : IFeedService
    {
        private readonly IGenericRepository<Feed> _feedRepository;
        private readonly IGenericRepository<FeedRecord> _feedRecordRepository;

        public FeedService(IGenericRepository<Feed> feedRepository, IGenericRepository<FeedRecord> feedRecordRepository)
        {
            _feedRepository = feedRepository;
            _feedRecordRepository = feedRecordRepository;
        }

        public async Task<FeedRecordDto> AddFeedRecordAsync(FeedRecordDto dto)
        {
            var feed = await _feedRepository.GetByIdAsync(dto.FeedID);
            if (feed == null)
            {
                throw new Exception("Belirtilen yem bulunamadı.");
            }

            // Service katmanında Stok kontrolü (0'ın altına düşemez)
            if (feed.StockQuantity < dto.QuantityKg)
            {
                throw new InvalidOperationException($"Yetersiz stok! Mevcut stok: {feed.StockQuantity} {feed.Unit}, İstenilen miktar: {dto.QuantityKg} {feed.Unit}");
            }

            // Otomatik stok düşme işlemi
            feed.StockQuantity -= dto.QuantityKg;

            var record = new FeedRecord
            {
                AnimalID = dto.AnimalID,
                FeedID = dto.FeedID,
                QuantityKg = dto.QuantityKg,
                RecordDate = dto.RecordDate
            };

            _feedRepository.Update(feed);
            await _feedRecordRepository.AddAsync(record);
            await _feedRecordRepository.SaveChangesAsync();

            dto.FeedRecordID = record.FeedRecordID;
            return dto;
        }
    }
}
