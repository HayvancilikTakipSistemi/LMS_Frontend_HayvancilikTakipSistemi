using LMS.Shared.DTOs.AI;
using LMS.Shared.Entities;

namespace LMS.Server.Services;

public class HayvanYapayZekaServisi
{
    public AIAnalysisResultDto AnalizEt(Hayvan hayvan)
    {
        var result = new AIAnalysisResultDto
        {
            AnimalId = hayvan.HayvanID,
            AnalysisDate = DateTime.UtcNow
        };

        // Eğer dogum tarihi null ise varsayılan 24 ay
        int yasAy = 24;
        if (hayvan.DogumTarihi.HasValue)
        {
             yasAy = (DateTime.UtcNow.Year - hayvan.DogumTarihi.Value.Year) * 12 + DateTime.UtcNow.Month - hayvan.DogumTarihi.Value.Month;
        }

        decimal tabanRisk = 2.0m;

        if (hayvan.HayvanDurumID == 5) // Ölü (varsayıyoruz)
        {
            result.HealthStatusDescription = "Kayıt durumu: Ölü.";
            result.ProductivityPrediction = "Yok";
            result.ActionRequired = "Sistemden otopsi/imha raporlaması alınabilir.";
            result.RiskScore = 10.0m;
            return result;
        }

        if (hayvan.HayvanDurumID == 4) // Satıldı
        {
            result.HealthStatusDescription = "Hayvan satılmış durumda.";
            result.ProductivityPrediction = "Yok";
            result.ActionRequired = "Yok";
            result.RiskScore = 0.0m;
            return result;
        }

        // Yaş Analizi
        if (yasAy < 2)
        {
            result.HealthStatusDescription = "Yenidoğan kritik gelişim döneminde.";
            result.ActionRequired = "Kolostrum alımı takip edilmeli.";
            tabanRisk += 3.0m;
        }
        else if (yasAy > 84) 
        {
            result.HealthStatusDescription = "İleri yaş grubunda, risk algılandı.";
            result.ProductivityPrediction = "Verim düşüş eğiliminde.";
            result.ActionRequired = "Ayıklama değerlendirilebilir.";
            tabanRisk += 2.5m;
        }
        else
        {
            result.HealthStatusDescription = "Yetişkin üretim çağında.";
            result.ProductivityPrediction = "Optimum verim.";
            result.ActionRequired = "Rutin kontrol.";
        }

        if (hayvan.Cinsiyet == "D" && yasAy > 15)
        {
             result.ProductivityPrediction += " Süt potansiyeli yüksek dönem.";
        }
        else if (hayvan.Cinsiyet == "E" && yasAy > 12)
        {
             result.ProductivityPrediction += " Besi olgunluğuna yaklaşmış.";
        }

        result.RiskScore = Math.Min(10.0m, Math.Max(0.0m, tabanRisk));

        if (result.RiskScore >= 5.0m)
        {
             result.ActionRequired = "❗ DİKKAT: " + result.ActionRequired;
        }

        return result;
    }
}
