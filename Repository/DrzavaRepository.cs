using Microsoft.EntityFrameworkCore;
using Vozila.Data;
using Vozila.DTOs;
using Vozila.Interfaces;
using Vozila.Models;

namespace Vozila.Repository
{
    public class DrzavaRepository : IDrzavaRepository
    {
        private readonly DataContext _dataContext;

        public DrzavaRepository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public bool DodajDrzavu(DrzavaPostDTO drzavaPostDTO)
        {
            Drzava novaDrzava = new Drzava()
            {
                Naziv = drzavaPostDTO.Naziv,
            };
            _dataContext.Drzave.Add(novaDrzava);
            return Save();
        }

        public bool DrzavaPostoji(int drzavaId)
        {
            return _dataContext.Drzave.Any(d=>d.Id == drzavaId);
        }

        public bool IzbrisiDrzavu(int drzavaId)
        {
            var drzavaZaBrisanje = _dataContext.Drzave.FirstOrDefault(d => d.Id == drzavaId);
            _dataContext.Drzave.Remove(drzavaZaBrisanje);
            return Save();
        }

        public bool IzmeniDrzavu(DrzavaPutDTO drzavaPutDTO)
        {
            var drzavaZaIzmenu = _dataContext.Drzave.FirstOrDefault(d => d.Id == drzavaPutDTO.Id);
            drzavaZaIzmenu.Naziv = drzavaPutDTO.Naziv;
            _dataContext.Drzave.Update(drzavaZaIzmenu);
            return Save();
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved>0?true:false;
        }

        public Drzava VratiDrzavuPoId(int drzavaId)
        {
            return _dataContext.Drzave.FirstOrDefault(d=>d.Id==drzavaId);
        }

        public ICollection<Proizvodjac> VratiProizvodjacePoDrzavaId(int drzavaId)
        {
            return _dataContext.Proizvodjaci.Include(p=>p.Drzava).Where(p=>p.Drzava.Id == drzavaId).ToList();
        }

        public ICollection<Drzava> VratiSveDrzave()
        {
            return _dataContext.Drzave.ToList();
        }
    }
}
