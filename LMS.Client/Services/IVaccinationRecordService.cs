using System.Collections.Generic;
using System.Threading.Tasks;
using LMS.Shared.DTOs;

namespace LMS.Client.Services
{
    public interface IVaccinationRecordService
    {
        Task<List<VaccinationRecordDto>> GetVaccinationRecordsAsync();
        Task<VaccinationRecordDto?> GetVaccinationRecordByIdAsync(int id);
        Task<VaccinationRecordDto?> AddVaccinationRecordAsync(VaccinationRecordDto record);
        Task<bool> DeleteVaccinationRecordAsync(int id);
    }
}
