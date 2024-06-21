using Vozila.Data;
using Vozila.DTOs;
using Vozila.Interfaces;
using Vozila.Models;

namespace Vozila.Repository
{
    public class ProizvodjacRepository : IProizvodjacRepository
    {
        private readonly DataContext _dataContext;

        public ProizvodjacRepository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public bool DodajProizvodjaca(ProizvodjacPostDTO proizvodjacPostDTO)
        {
            var drzava = _dataContext.Drzave.FirstOrDefault(d => d.Id == proizvodjacPostDTO.DrzavaId);
            Proizvodjac novi = new Proizvodjac()
            {
                Naziv = proizvodjacPostDTO.Naziv,
                Drzava = drzava
            };
            _dataContext.Proizvodjaci.Add(novi);
            return Save();
        }

        public bool IzbrisiProizvodjaca(int proizvodjacId)
        {
            var proizvodjacZaBrisanje = _dataContext.Proizvodjaci.FirstOrDefault(p=>p.Id == proizvodjacId);
            _dataContext.Proizvodjaci.Remove(proizvodjacZaBrisanje);
            return Save();
        }

        public bool IzmeniProizvodjaca(ProizvodjacPutDTO proizvodjacPutDTO)
        {
            var drzava = _dataContext.Drzave.FirstOrDefault(d=>d.Id == proizvodjacPutDTO.DrzavaId);
            var proizvodjacZaIzmenu = _dataContext.Proizvodjaci.FirstOrDefault(p => p.Id == proizvodjacPutDTO.Id);
            proizvodjacZaIzmenu.Drzava = drzava;
            proizvodjacZaIzmenu.Naziv = proizvodjacPutDTO.Naziv;
            _dataContext.Proizvodjaci.Update(proizvodjacZaIzmenu);
            return Save();
        }

        public bool ProizvodjacPostoji(int proizvodjacId)
        {
            return _dataContext.Proizvodjaci.Any(p => p.Id == proizvodjacId);
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public Proizvodjac VratiProizvodjacaPoId(int proizvodjacid)
        {
            return _dataContext.Proizvodjaci.FirstOrDefault(p => p.Id == proizvodjacid);
        }

        public ICollection<Proizvodjac> VratiSveProizvodjace()
        {
            return _dataContext.Proizvodjaci.ToList();
        }
    }
}
