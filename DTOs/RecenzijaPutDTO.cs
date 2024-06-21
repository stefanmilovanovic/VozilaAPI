using Vozila.Models;

namespace Vozila.DTOs
{
    public class RecenzijaPutDTO
    {
        public int Id { get; set; }
        public string Naslov { get; set; } = "";
        public string Opis { get; set; } = "";
        public int Ocena { get; set; }
        public int Vozilo { get; set; }
        public int Recezent { get; set; }
    }
}
