using Microsoft.EntityFrameworkCore;
using Vozila.Data;
using Vozila.DTOs;
using Vozila.Interfaces;
using Vozila.Models;

namespace Vozila.Repository
{
    public class VoziloRepository : IVoziloRepository
    {
        private readonly DataContext _dataContext;

        public VoziloRepository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public bool DodajVozilo(VoziloPostDTO voziloPostDTO)
        {
            Vozilo novoVozilo = new Vozilo();
            novoVozilo.Marka = voziloPostDTO.Marka;
            novoVozilo.Model = voziloPostDTO.Model;
            novoVozilo.Godiste = voziloPostDTO.Godiste;
            novoVozilo.Cena = voziloPostDTO.Cena;

            var proizvodjac = _dataContext.Proizvodjaci.FirstOrDefault(p => p.Id == voziloPostDTO.ProizvodjacId);
            novoVozilo.Proizvodjac = proizvodjac;
            _dataContext.Add(novoVozilo);

            if (voziloPostDTO.VoziloKategorije.Length > 0)
            {
                foreach (var kategorija in _dataContext.Kategorije.Where(c=>voziloPostDTO.VoziloKategorije.Contains(c.Id)))
                {
                    VoziloKategorija vk = new VoziloKategorija()
                    {
                        Vozilo = novoVozilo,
                        Kategorija = kategorija
                    };
                    _dataContext.VoziloKategorije.Add(vk);
                }
            }
            return Save();
        }

        public bool IzmeniVozilo(VoziloPutDTO voziloPutDTO)
        {
            var voziloZaIzmenu = _dataContext.Vozila.FirstOrDefault(v => v.Id == voziloPutDTO.Id);
            voziloZaIzmenu.Marka = voziloPutDTO.Marka;
            voziloZaIzmenu.Model = voziloPutDTO.Model;
            voziloZaIzmenu.Cena = voziloPutDTO.Cena;
            voziloZaIzmenu.Godiste = voziloPutDTO.Godiste;

            Proizvodjac proizvodjac = _dataContext.Proizvodjaci.FirstOrDefault(p => p.Id == voziloPutDTO.ProizvodjacId);
            voziloZaIzmenu.Proizvodjac = proizvodjac;
            _dataContext.Vozila.Update(voziloZaIzmenu);
            // Brisanje svih vozilo-kategorija datog vozila
            foreach(var vk in _dataContext.VoziloKategorije.Where(v => v.VoziloId == voziloPutDTO.Id))
            {
                _dataContext.VoziloKategorije.Remove(vk);
            };

            //Dodavanje novih
            var kategorije = _dataContext.Kategorije.Where(k=>voziloPutDTO.VoziloKategorije.Contains(k.Id)).ToList();
            foreach(var kategorija in kategorije)
            {
                VoziloKategorija vk = new VoziloKategorija()
                {
                    Vozilo = voziloZaIzmenu,
                    Kategorija = kategorija
                };
                _dataContext.VoziloKategorije.Add(vk);
            }
            return Save();
        }

        public bool ObrisiVozilo(int voziloId)
        {
            var voziloZaBrisanje = _dataContext.Vozila.FirstOrDefault(v=>v.Id == voziloId);
            _dataContext.Vozila.Remove(voziloZaBrisanje);
            return Save();
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool VoziloPostoji(int voiloId)
        {
            return _dataContext.Vozila.Any(v => v.Id == voiloId);
        }

        public VoziloGetFullDTO VratiFullVozilo(int voziloId)
        {
            VoziloGetFullDTO voziloFull = new VoziloGetFullDTO();
            var vozilo = _dataContext.Vozila.Where(v => v.Id == voziloId).Include(v => v.Proizvodjac).FirstOrDefault();
            voziloFull.Id = voziloId;
            voziloFull.Model = vozilo.Model;
            voziloFull.Marka = vozilo.Marka;
            voziloFull.Godiste = vozilo.Godiste;
            voziloFull.Cena = vozilo.Cena;
            voziloFull.Proizvodjac = vozilo.Proizvodjac.Naziv;

            var kategorije = _dataContext.VoziloKategorije.Where(vk => vk.VoziloId == voziloId).Select(vk => vk.Kategorija);
            List<string> kategorijeNaziv = new List<string>();
            foreach (Kategorija k in kategorije)
            {
                kategorijeNaziv.Add(k.Naziv);
            }
            voziloFull.Kategorije = kategorijeNaziv.ToArray();
            return voziloFull;

        }

        public ICollection<Vozilo> VratiSvaVozila()
        {
            return _dataContext.Vozila.ToList();
        }

        public Vozilo VratiVozilo(int voziloId)
        {
            return _dataContext.Vozila.FirstOrDefault(v => v.Id == voziloId);
        }
    }
}
