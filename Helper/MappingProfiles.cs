using AutoMapper;
using Vozila.DTOs;
using Vozila.Models;

namespace Vozila.Helper
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Drzava, DrzavaGetDTO>();
            CreateMap<Proizvodjac,ProizvodjacGetSimpleDTO>();
            CreateMap<Recezent,RecezentGetDTO>();
            CreateMap<Kategorija,KategorijaGetDTO>();
            CreateMap<Recenzija, RecenzijaGetDTO>();
            CreateMap<Vozilo, VoziloGetDTO>();
        }
    }
}
