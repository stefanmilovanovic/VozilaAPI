using Vozila.Models;

namespace Vozila.DTOs
{
    public class VoziloPutDTO
    {
        public int Id { get; set; }
        public string Marka { get; set; } = "";
        public string Model { get; set; } = "";
        public int Godiste { get; set; }
        public double Cena { get; set; }
        public int ProizvodjacId { get; set; }
        public int[] VoziloKategorije { get; set; }
    }
}
