namespace Vozila.Models
{
    public class Vozilo
    {
        public int Id { get; set; }
        public string Marka { get; set; } = "";
        public string Model { get; set; } = "";
        public int Godiste { get; set; }
        public double Cena { get; set; }
        public Proizvodjac Proizvodjac { get; set; }
        public ICollection<Recenzija> Recenzije { get; set; }
        public ICollection<VoziloKategorija> VoziloKategorije { get; set; }

    }
}
