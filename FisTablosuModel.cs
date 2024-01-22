namespace Fis.mvc.Models
{
    public class FisTablosuModel
    {
        public int id { get; set; }
        public DateTime Tarih { get; set; }
        public int FisNo { get; set; }
        public string Aciklama { get; set; }
        public string Masraf { get; set; }
        public int Kdv { get; set; }
        public decimal Toplam { get; set; }
        public decimal ToplamKdv { get; set; }
        public int Personelid { get; set; }
    }
}
