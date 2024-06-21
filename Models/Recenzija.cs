namespace Vozila.Models
{
    public class Recenzija
    {
        public int Id { get; set; }
        public string Naslov { get; set; } = "";
        public string Opis { get; set; } = "";
        public int Ocena { get; set; }
        public Vozilo Vozilo { get; set; }
        public Recezent Recezent { get; set; }

    }
}
