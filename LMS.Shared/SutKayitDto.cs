namespace LMS.Shared
{
    public class SutKayitDto
    {
        public int SutKayitID { get; set; }
        public int HayvanID { get; set; }
        public decimal MiktarLitre { get; set; }
        public decimal BirimFiyat { get; set; }
        public DateTime KayitTarihi { get; set; } = DateTime.Now;
    }
}