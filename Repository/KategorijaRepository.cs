using Vozila.Data;
using Vozila.DTOs;
using Vozila.Interfaces;
using Vozila.Models;

namespace Vozila.Repository
{
    public class KategorijaRepository : IKategorijaRepository
    {
        private readonly DataContext _dataContext;

        public KategorijaRepository(DataContext dataContext)
        {
            this._dataContext = dataContext;
        }

        public bool DodajKategoriju(KategorijaPostDTO kategorijaPostDTO)
        {
            Kategorija novaKategorija = new Kategorija()
            {
                Naziv = kategorijaPostDTO.Naziv
            };
            _dataContext.Kategorije.Add(novaKategorija);
            return Save();
        }

        public bool IzbrisiKategoriju(int kategorijaId)
        {
            var kategorijaZaBrisanje = _dataContext.Kategorije.FirstOrDefault(k=>k.Id == kategorijaId);
            _dataContext.Kategorije.Remove(kategorijaZaBrisanje);
            return Save();
        }

        public bool IzmeniKategoriju(KategorijaPutDTO kategorijaPutDTO)
        {
            var kategorijaZaIzmenu = _dataContext.Kategorije.FirstOrDefault(k=>k.Id == kategorijaPutDTO.Id);
            kategorijaZaIzmenu.Naziv = kategorijaPutDTO.Naziv;
            _dataContext.Kategorije.Update(kategorijaZaIzmenu);
            return Save();
        }

        public bool KategorijaPostoji(int kategorijaId)
        {
            return _dataContext.Kategorije.Any(k=>k.Id == kategorijaId);
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }

        public Kategorija VratiKategoriju(int kategorijaId)
        {
            return _dataContext.Kategorije.FirstOrDefault(k => k.Id == kategorijaId);
        }

        public ICollection<Kategorija> VratiSveKategorije()
        {
            return _dataContext.Kategorije.ToList();
        }
    }
}
