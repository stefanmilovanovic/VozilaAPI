namespace Vozila.Models
{
    public class Proizvodjac
    {
        public int Id { get; set; }
        public string Naziv { get; set; } = "";
        public Drzava Drzava { get; set; }
        public ICollection<Vozilo> Vozila { get; set; }
    }
}
