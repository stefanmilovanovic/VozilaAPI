using Vozila.DTOs;
using Vozila.Models;

namespace Vozila.Interfaces
{
    public interface IDrzavaRepository
    {
        ICollection<Drzava> VratiSveDrzave();
        Drzava VratiDrzavuPoId(int drzavaId);
        bool DrzavaPostoji(int drzavaId);

        ICollection<Proizvodjac> VratiProizvodjacePoDrzavaId(int drzavaId);
        bool DodajDrzavu(DrzavaPostDTO drzavaPostDTO);
        bool IzmeniDrzavu(DrzavaPutDTO drzavaPutDTO);
        bool IzbrisiDrzavu(int drzavaId);
        bool Save();
    }
}
