namespace Vozila.Models
{
    public class Kategorija
    {
        public int Id { get; set; }
        public string Naziv { get; set; } = "";
        public ICollection<VoziloKategorija> VoziloKategorije { get; set; }
    }
}
