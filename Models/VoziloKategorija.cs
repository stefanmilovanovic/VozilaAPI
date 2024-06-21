namespace Vozila.Models
{
    public class VoziloKategorija
    {
        public int VoziloId { get; set; }
        public int KategorijaId { get; set; }
        public Vozilo Vozilo { get; set; }  
        public Kategorija Kategorija { get; set; }
    }
}
