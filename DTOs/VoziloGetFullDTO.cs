using Vozila.Models;

namespace Vozila.DTOs
{
    public class VoziloGetFullDTO
    {
        public int Id { get; set; }
        public string Marka { get; set; } = "";
        public string Model { get; set; } = "";
        public int Godiste { get; set; }
        public double Cena { get; set; }
        public string Proizvodjac { get; set; }
        public string[] Kategorije { get; set; }
    }
}
