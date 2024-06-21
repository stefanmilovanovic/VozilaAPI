using Vozila.Models;

namespace Vozila.DTOs
{
    public class RecenzijaFullGetDTO
    {
        public int Id { get; set; }
        public string Naslov { get; set; } = "";
        public string Opis { get; set; } = "";
        public int Ocena { get; set; }
        public string Vozilo { get; set; }
        public string Recezent { get; set; }
    }
}
