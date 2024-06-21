namespace Vozila.DTOs
{
    public class RecenzijaGetDTO
    {
        public int Id { get; set; }
        public string Naslov { get; set; } = "";
        public string Opis { get; set; } = "";
        public int Ocena { get; set; }
    }
}
