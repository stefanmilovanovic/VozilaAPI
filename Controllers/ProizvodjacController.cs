using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vozila.DTOs;
using Vozila.Interfaces;

namespace Vozila.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProizvodjacController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProizvodjacRepository _proizvodjacRepository;
        private readonly IDrzavaRepository _drzavaRepository;

        public ProizvodjacController(IMapper mapper,IProizvodjacRepository proizvodjacRepository,IDrzavaRepository drzavaRepository)
        {
            this._mapper = mapper;
            this._proizvodjacRepository = proizvodjacRepository;
            this._drzavaRepository = drzavaRepository;
        }

        [HttpGet]
        [ProducesResponseType(200,Type= typeof(ICollection<ProizvodjacGetSimpleDTO>))]
        public IActionResult VratiSveProizvodjace()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var sviProizvodjaci = _mapper.Map<ICollection<ProizvodjacGetSimpleDTO>>(_proizvodjacRepository.VratiSveProizvodjace());
            return Ok(sviProizvodjaci);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200,Type=typeof(ProizvodjacGetSimpleDTO))]
        [ProducesResponseType(400)]
        public IActionResult VratiProizvodjacaPoId(int id)
        {
            if (!_proizvodjacRepository.ProizvodjacPostoji(id))
            {
                return NotFound();
            }
            var proizvodjac = _mapper.Map<ProizvodjacGetSimpleDTO>(_proizvodjacRepository.VratiProizvodjacaPoId(id));
            return Ok(proizvodjac);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult DodajProizvodjaca([FromBody] ProizvodjacPostDTO proizvodjacPostDTO)
        {
            if (!_drzavaRepository.DrzavaPostoji(proizvodjacPostDTO.DrzavaId))
            {
                return NotFound();
            }

            if (!_proizvodjacRepository.DodajProizvodjaca(proizvodjacPostDTO))
            {
                ModelState.AddModelError("", "Greska");
                return StatusCode(500, ModelState);
            }
            return Ok("Uspeh!");
        }
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult IzmeniProizvodjaca([FromBody] ProizvodjacPutDTO proizvodjacPutDTO)
        {
            if(proizvodjacPutDTO == null)
            {
                return BadRequest();
            }
            if (!_drzavaRepository.DrzavaPostoji(proizvodjacPutDTO.DrzavaId))
            {
                return NotFound();
            }
            if (!_proizvodjacRepository.IzmeniProizvodjaca(proizvodjacPutDTO))
            {
                ModelState.AddModelError("", "Greska");
                return StatusCode(500, ModelState);
            }
            return Ok("Uspeh!");
        }

        [HttpDelete("{id}")]
        public IActionResult IzbrisiProizvodjaca(int id)
        {
            if (!_proizvodjacRepository.ProizvodjacPostoji(id))
            {
                return NotFound();
            }

            if (!_proizvodjacRepository.IzbrisiProizvodjaca(id))
            {
                ModelState.AddModelError("", "Greska");
                return StatusCode(500, ModelState);
            }
            return Ok("Uspeh!");
        }
    }
}
