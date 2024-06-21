using Vozila.DTOs;
using Vozila.Models;

namespace Vozila.Interfaces
{
    public interface IProizvodjacRepository
    {
        ICollection<Proizvodjac> VratiSveProizvodjace();
        Proizvodjac VratiProizvodjacaPoId(int proizvodjacid);
        bool ProizvodjacPostoji(int proizvodjacId);
        bool DodajProizvodjaca(ProizvodjacPostDTO proizvodjacPostDTO);
        bool IzmeniProizvodjaca(ProizvodjacPutDTO proizvodjacPutDTO);
        bool IzbrisiProizvodjaca(int proizvodjacId);
        bool Save();
    }
}
