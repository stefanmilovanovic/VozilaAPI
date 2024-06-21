using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vozila.DTOs;
using Vozila.Interfaces;

namespace Vozila.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecenzijaController : ControllerBase
    {
        private readonly IRecenzijaRepository _recenzijaRepository;
        private readonly IMapper _mapper;
        private readonly IRecezentRepository _recezentRepository;

        public RecenzijaController(IRecenzijaRepository recenzijaRepository,IMapper mapper,IRecezentRepository recezentRepository)
        {
            this._recenzijaRepository = recenzijaRepository;
            this._mapper = mapper;
            this._recezentRepository = recezentRepository;
        }
        [HttpGet]
        [ProducesResponseType(200,Type=typeof(ICollection<RecenzijaGetDTO>))]
        public IActionResult VratiSveRecenzije()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var recenzije = _mapper.Map<ICollection<RecenzijaGetDTO>>(_recenzijaRepository.VratiSveRecenzije());
            return Ok(recenzije);
        }
        [HttpGet("{id}")]
        public IActionResult CratiRecenzijuPoId(int id)
        {
            if(!_recenzijaRepository.RecenzijaPostoji(id))
            {
                return NotFound();
            }

            var recenzija = _mapper.Map<RecenzijaGetDTO>(_recenzijaRepository.VratiRecenziju(id));
            return Ok(recenzija);
        }

        [HttpGet("full")]
        [ProducesResponseType(200,Type=typeof(ICollection<RecenzijaFullGetDTO>))]
        public IActionResult VratiRecenzijeFull()
        {
            var recenzije = _recenzijaRepository.VratiFullRecenzije();
            return Ok(recenzije);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        public IActionResult DodajRecenziju([FromBody] RecenzijaPostDTO recenzijaPostDTO)
        {
            if (recenzijaPostDTO == null)
            {
                return BadRequest();
            }
            if (!_recezentRepository.RecezentPostoji(recenzijaPostDTO.Recezent))
            {
                return NotFound();
            }
            if (!_recenzijaRepository.DodajRecenziju(recenzijaPostDTO))
            {
                ModelState.AddModelError("", "Greska");
            }
            return Ok("Uspeh!");
        }
        [HttpPut]
        public IActionResult IzmeniRecenziju([FromBody] RecenzijaPutDTO recenzijaPutDTO)
        {
            if (recenzijaPutDTO == null)
            {
                return BadRequest();
            }
            if (!_recezentRepository.RecezentPostoji(recenzijaPutDTO.Recezent))
            {
                return NotFound();
            }
            if (!_recenzijaRepository.IzmeniRecenziju(recenzijaPutDTO))
            {
                ModelState.AddModelError("", "Greska!");
                return StatusCode(500, ModelState);
            }
            return Ok("Uspeh!");
        }

        [HttpDelete("{id}")]
        public IActionResult ObrisiRecenziju(int id)
        {
            if (!_recenzijaRepository.RecenzijaPostoji(id))
            {
                return NotFound();
            }
            if (!_recenzijaRepository.ObrisiRecenziju(id))
            {
                ModelState.AddModelError("", "Greska");
                return StatusCode(500, ModelState);
            }
            return Ok("Uspeh!");
        }
        
    }
}
