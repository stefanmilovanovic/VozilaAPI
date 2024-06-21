using Vozila.Data;
using Vozila.DTOs;
using Vozila.Interfaces;
using Vozila.Models;

namespace Vozila.Repository
{
    public class RecezentRepository:IRecezentRepository
    {
        private readonly DataContext _dataContext;

        public RecezentRepository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public bool DodajRecezenta(RecezentPostDTO recezentPostDTO)
        {
            Recezent noviRecezent = new Recezent()
            {
                Ime = recezentPostDTO.Ime,
                Prezime = recezentPostDTO.Prezime
            };
            _dataContext.Recezenti.Add(noviRecezent);
            return Save();
        }

        public bool IzbrisiRecezenta(int recezentId)
        {
            var recezentZaBrisanje = _dataContext.Recezenti.FirstOrDefault(r=>r.Id == recezentId);
            _dataContext.Recezenti.Remove(recezentZaBrisanje);
            return Save();
        }

        public bool IzmeniRecezenta(RecezentPutDTO recezentPutDTO)
        {
            var recezentZaIzmenu = _dataContext.Recezenti.FirstOrDefault(r=>r.Id == recezentPutDTO.Id);
            recezentZaIzmenu.Ime = recezentPutDTO.Ime;
            recezentZaIzmenu.Prezime = recezentPutDTO.Prezime;
            _dataContext.Recezenti.Update(recezentZaIzmenu);
            return Save();
        }

        public bool RecezentPostoji(int recezentId)
        {
            return _dataContext.Recezenti.Any(r=>r.Id == recezentId);
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved>0?true:false;
        }

        public Recezent VratiRecezentaPoId(int recezentId)
        {
            return _dataContext.Recezenti.FirstOrDefault(r => r.Id == recezentId);
        }

        public ICollection<Recezent> VratiRecezente()
        {
            return _dataContext.Recezenti.ToList();
        }
    }
}
