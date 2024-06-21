namespace Vozila.DTOs
{
    public class VoziloGetDTO
    {
        public int Id { get; set; }
        public string Marka { get; set; } = "";
        public string Model { get; set; } = "";
        public int Godiste { get; set; }
        public double Cena { get; set; }
    }
}
