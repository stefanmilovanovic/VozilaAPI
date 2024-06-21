using Vozila.DTOs;
using Vozila.Models;

namespace Vozila.Interfaces
{
    public interface IVoziloRepository
    {
        ICollection<Vozilo> VratiSvaVozila();
        Vozilo VratiVozilo(int voziloId);
        bool VoziloPostoji(int voiloId);
        VoziloGetFullDTO VratiFullVozilo(int voziloId);
        bool DodajVozilo(VoziloPostDTO voziloPostDTO);
        bool IzmeniVozilo(VoziloPutDTO voziloPutDTO);
        bool ObrisiVozilo(int voziloId);
        bool Save();
    }
}
