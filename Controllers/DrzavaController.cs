using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vozila.DTOs;
using Vozila.Interfaces;
using Vozila.Models;
using Vozila.Services;

namespace Vozila.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrzavaController : ControllerBase
    {
        private readonly IDrzavaRepository _drzavaRepository;
        private readonly IMapper _mapper;

        public DrzavaController(IDrzavaRepository drzavaRepository, IMapper mapper)
        {
            this._drzavaRepository = drzavaRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<DrzavaGetDTO>))]
        public IActionResult VratiSveDrzave()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var sveDrzave = _mapper.Map<ICollection<DrzavaGetDTO>>(_drzavaRepository.VratiSveDrzave());
            return Ok(sveDrzave);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(DrzavaGetDTO))]
        [ProducesResponseType(400)]
        public IActionResult VratiDrzavuPoId(int id)
        {
            if (!_drzavaRepository.DrzavaPostoji(id))
            {
                return NotFound();
            }

            var drzava = _mapper.Map<DrzavaGetDTO>(_drzavaRepository.VratiDrzavuPoId(id));
            return Ok(drzava);
        }

        [HttpGet("{id}/proizvodjaci")]
        [ProducesResponseType(200)]
        public IActionResult VratiProizvodjacePoDrzavaId(int id)
        {
            if (!_drzavaRepository.DrzavaPostoji(id))
            {
                return NotFound();
            }
            ICollection<Proizvodjac> proizvodjaci = _drzavaRepository.VratiProizvodjacePoDrzavaId(id);
            List<ProizvodjacGetDTO> proizvodjaciDTO = new List<ProizvodjacGetDTO>();
            foreach (var pro in proizvodjaci)
            {
                proizvodjaciDTO.Add(new ProizvodjacGetDTO()
                {
                    Id = pro.Id,
                    Naziv = pro.Naziv,
                    Drzava = pro.Drzava.Naziv
                });
            }
            return Ok(proizvodjaciDTO);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        public IActionResult DodajDrzavu([FromBody] DrzavaPostDTO drzavaPostDTO)
        {
            if(drzavaPostDTO == null)
            {
                return BadRequest();
            }
            if (!_drzavaRepository.DodajDrzavu(drzavaPostDTO))
            {
                ModelState.AddModelError("", "Greska prilikom ulaza");
                return StatusCode(500, ModelState);
            }
            Promene.ZapisiUnos();
            return Ok("Uspeh!");
        }

        [HttpPut]
        public IActionResult IzmeniDrzavu([FromBody] DrzavaPutDTO drzavaPutDTO)
        {
            if(drzavaPutDTO == null)
            {
                return BadRequest();
            }
            if (!_drzavaRepository.DrzavaPostoji(drzavaPutDTO.Id))
            {
                return NotFound();
            }

            if (!_drzavaRepository.IzmeniDrzavu(drzavaPutDTO))
            {
                ModelState.AddModelError("", "Greska prilikom unosa");
                return StatusCode(500, ModelState);
            }
            return Ok("Uspeh");
        }

        [HttpDelete("{id}")]
        public IActionResult IzbrisiDrzavuint(int id)
        {
            if (!_drzavaRepository.DrzavaPostoji(id))
            {
                return NotFound();
            }
            if (!_drzavaRepository.IzbrisiDrzavu(id))
            {
                ModelState.AddModelError("", "Greska");
                return StatusCode(500, ModelState);
            }
            Promene.ZapisiBrisanje();
            return Ok("Uspeh");
        }
    }
}
