using Vozila.DTOs;
using Vozila.Models;

namespace Vozila.Interfaces
{
    public interface IKategorijaRepository
    {
        ICollection<Kategorija> VratiSveKategorije();
        Kategorija VratiKategoriju(int kategorijaId);
        bool KategorijaPostoji(int kategorijaId);
        bool DodajKategoriju(KategorijaPostDTO kategorijaPostDTO);
        bool IzmeniKategoriju(KategorijaPutDTO kategorijaPutDTO);
        bool IzbrisiKategoriju(int kategorijaId);
        bool Save();
    }
}
