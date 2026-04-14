namespace LMS.Shared
{
    public class YemKayitDto
    {
        public int YemKayitID { get; set; }
        public int YemID { get; set; }
        public int HayvanID { get; set; }
        public decimal KullanilanMiktar { get; set; }
        public DateTime KayitTarihi { get; set; } = DateTime.Now;
    }
}