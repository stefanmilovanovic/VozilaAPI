using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vozila.DTOs;
using Vozila.Interfaces;

namespace Vozila.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoziloController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVoziloRepository _voziloRepository;

        public VoziloController(IMapper mapper, IVoziloRepository voziloRepository)
        {
            this._mapper = mapper;
            this._voziloRepository = voziloRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ICollection<VoziloGetDTO>))]
        public IActionResult VratiSvaVozila()
        {
            var vozila = _mapper.Map<ICollection<VoziloGetDTO>>(_voziloRepository.VratiSvaVozila());
            return Ok(vozila);
        }
        [HttpGet("{id}")]
        public IActionResult VratiVozilo(int id)
        {
            if (!_voziloRepository.VoziloPostoji(id))
            {
                return NotFound();
            }
            var vozilo = _mapper.Map<VoziloGetDTO>(_voziloRepository.VratiVozilo(id));
            return Ok(vozilo);
        }
        [HttpGet("{id}/full")]
        public IActionResult VratiVoziloFull(int id)
        {
            if (!_voziloRepository.VoziloPostoji(id))
            {
                return NotFound();
            }
            var vozilo = _voziloRepository.VratiFullVozilo(id);
            return Ok(vozilo);
        }
        [HttpPost]
        public IActionResult DodajVozilo(VoziloPostDTO voziloPostDTO)
        {
            if (voziloPostDTO == null)
            {
                return BadRequest();
            }

            if (!_voziloRepository.DodajVozilo(voziloPostDTO))
            {
                ModelState.AddModelError("", "Greska");
            }
            return Ok("Uspeh!");

        }
        [HttpPut]
        public IActionResult IzmeniVozilo([FromBody] VoziloPutDTO voziloPutDTO)
        {
            if (voziloPutDTO == null)
            {
                return BadRequest();
            }

            if (!_voziloRepository.VoziloPostoji(voziloPutDTO.Id))
            {
                return NotFound();
            }

            if (!_voziloRepository.IzmeniVozilo(voziloPutDTO))
            {
                ModelState.AddModelError("", "Greska");
                return StatusCode(500, ModelState);
            }

            return Ok("Uspeh!");
        }
        [HttpDelete("{id}")]
        public IActionResult ObrisiVozilo(int id)
        {
            if (!_voziloRepository.VoziloPostoji(id))
            {
                return NotFound();
            }
            if (!_voziloRepository.ObrisiVozilo(id))
            {
                ModelState.AddModelError("", "Greska");
                return StatusCode(500, ModelState);
            }
            return Ok("Uspeh!");
        }
    }
}
