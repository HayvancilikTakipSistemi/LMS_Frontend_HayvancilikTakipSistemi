using LMS.Shared;
namespace LMS_Frontend_HayvancilikTakipSistemi.Services {
    public interface IYemService {
        Task<string> YemVerAsync(YemKayitDto kayit);
    }
}