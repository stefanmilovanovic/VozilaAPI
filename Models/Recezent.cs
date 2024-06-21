namespace Vozila.Models
{
    public class Recezent
    {
        public int Id { get; set; }
        public string Ime { get; set; } = "";
        public string Prezime { get; set; } = "";
        public ICollection<Recenzija> Recenzije { get; set; }   
    }
}
