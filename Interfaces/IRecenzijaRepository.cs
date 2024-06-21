using Vozila.DTOs;
using Vozila.Models;

namespace Vozila.Interfaces
{
    public interface IRecenzijaRepository
    {
        ICollection<Recenzija> VratiSveRecenzije();
        Recenzija VratiRecenziju(int recenzijaId);
        bool RecenzijaPostoji(int recenzijaId);
        ICollection<RecenzijaFullGetDTO> VratiFullRecenzije();
        bool DodajRecenziju(RecenzijaPostDTO recenzijaPostDTO);
        bool IzmeniRecenziju(RecenzijaPutDTO recenzijaPutDTO);
        bool ObrisiRecenziju(int recenzijaId);
        bool Save();
    }
}
