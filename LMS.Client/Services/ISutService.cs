using LMS.Shared.Models;

namespace LMS_Frontend_HayvancilikTakipSistemi.Services
{
    public interface ISutService
    {
        Task SutKaydetAsync(SutKaydi kayit);
        Task<List<GunlukSutOzet>> GetRaporAsync(int ciftciId);
    }
}