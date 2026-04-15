namespace LMS.Shared.DTOs
{
    public class CiftciGelirRaporuDto
    {
        public int CiftciID { get; set; }
        public string AdSoyad { get; set; } = string.Empty;
        public int IslemSayisi { get; set; }
        public decimal ToplamGelir { get; set; }
    }
}
