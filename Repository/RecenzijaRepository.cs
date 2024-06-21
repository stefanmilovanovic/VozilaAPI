using Microsoft.EntityFrameworkCore;
using Vozila.Data;
using Vozila.DTOs;
using Vozila.Interfaces;
using Vozila.Models;

namespace Vozila.Repository
{
    public class RecenzijaRepository : IRecenzijaRepository
    {
        private readonly DataContext _dataContext;

        public RecenzijaRepository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public bool DodajRecenziju(RecenzijaPostDTO recenzijaPostDTO)
        {
            Recenzija novaReceznija = new Recenzija();
            Recezent recezent = _dataContext.Recezenti.FirstOrDefault(r => r.Id == recenzijaPostDTO.Recezent);
            Vozilo vozilo = _dataContext.Vozila.FirstOrDefault(v => v.Id == recenzijaPostDTO.Vozilo);
            novaReceznija.Naslov = recenzijaPostDTO.Naslov;
            novaReceznija.Opis = recenzijaPostDTO.Opis;
            novaReceznija.Ocena = recenzijaPostDTO.Ocena;
            novaReceznija.Vozilo = vozilo;
            novaReceznija.Recezent = recezent;
            _dataContext.Recenzije.Add(novaReceznija);
            return Save();
        }

        public bool IzmeniRecenziju(RecenzijaPutDTO recenzijaPutDTO)
        {
            var recenzijaZaIzmenu = _dataContext.Recenzije.FirstOrDefault(r => r.Id == recenzijaPutDTO.Id);
            Vozilo vozilo = _dataContext.Vozila.FirstOrDefault(v=>v.Id==recenzijaPutDTO.Vozilo);
            Recezent recezent = _dataContext.Recezenti.FirstOrDefault(r=>r.Id==recenzijaPutDTO.Recezent);
            recenzijaZaIzmenu.Ocena = recenzijaPutDTO.Ocena;
            recenzijaZaIzmenu.Naslov = recenzijaPutDTO.Naslov;
            recenzijaZaIzmenu.Opis = recenzijaPutDTO.Opis;
            recenzijaZaIzmenu.Vozilo = vozilo;
            recenzijaZaIzmenu.Recezent = recezent;
            _dataContext.Recenzije.Update(recenzijaZaIzmenu);
            return Save();
        }

        public bool ObrisiRecenziju(int recenzijaId)
        {
            var recenzijaZaBrisanje = _dataContext.Recenzije.FirstOrDefault(r => r.Id == recenzijaId);
            _dataContext.Recenzije.Remove(recenzijaZaBrisanje);
            return Save();
        }

        public bool RecenzijaPostoji(int recenzijaId)
        {
            return _dataContext.Recenzije.Any(r=>r.Id == recenzijaId); 
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public ICollection<RecenzijaFullGetDTO> VratiFullRecenzije()
        {
            List<RecenzijaFullGetDTO> recenzije = new List<RecenzijaFullGetDTO>();
            foreach(var recenzija in _dataContext.Recenzije.Include(r=>r.Recezent).Include(r=>r.Vozilo))
            {
                recenzije.Add(new RecenzijaFullGetDTO()
                {
                    Id = recenzija.Id,
                    Naslov = recenzija.Naslov,
                    Opis = recenzija.Opis,
                    Ocena = recenzija.Ocena,
                    Vozilo = (recenzija.Vozilo.Marka+" "+recenzija.Vozilo.Model),
                    Recezent = (recenzija.Recezent.Ime+" "+recenzija.Recezent.Prezime)
                });
            }
            return recenzije;
        }

        public Recenzija VratiRecenziju(int recenzijaId)
        {
            return _dataContext.Recenzije.FirstOrDefault(r => r.Id == recenzijaId);
        }

        public ICollection<Recenzija> VratiSveRecenzije()
        {
            return _dataContext.Recenzije.ToList();
        }
    }
}
