using Vozila.DTOs;
using Vozila.Models;

namespace Vozila.Interfaces
{
    public interface IRecezentRepository
    {
        ICollection<Recezent> VratiRecezente();
        Recezent VratiRecezentaPoId(int recezentId);
        bool RecezentPostoji(int recezentId);
        bool DodajRecezenta(RecezentPostDTO recezentPostDTO);
        bool IzmeniRecezenta(RecezentPutDTO recezentPutDTO);
        bool IzbrisiRecezenta(int recezentId);
        bool Save();
    }
}
