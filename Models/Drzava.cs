namespace Vozila.Models
{
    public class Drzava
    {
        public int Id { get; set; }
        public string Naziv { get; set; } = "";
        public ICollection<Proizvodjac> Proizvodjaci { get; set; }
    }
}
