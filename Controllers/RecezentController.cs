using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vozila.DTOs;
using Vozila.Interfaces;
using Vozila.Models;

namespace Vozila.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecezentController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRecezentRepository _recezentRepository;

        public RecezentController(IMapper mapper,IRecezentRepository recezentRepository)
        {
            this._mapper = mapper;
            this._recezentRepository = recezentRepository;
        }

        [HttpGet]
        [ProducesResponseType(200,Type=typeof(ICollection<RecezentGetDTO>))]
        public IActionResult VratiSveRecezente()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var recezenti = _mapper.Map<ICollection<RecezentGetDTO>>(_recezentRepository.VratiRecezente());
            return Ok(recezenti);
        }
        [HttpGet("{id}")]
        public IActionResult VratiRecezentaPoId(int id)
        {
            if (!_recezentRepository.RecezentPostoji(id))
            {
                return NotFound();
            }
            var recezent = _mapper.Map<RecezentGetDTO>(_recezentRepository.VratiRecezentaPoId(id));
            return Ok(recezent);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        public IActionResult DodajRecezenta([FromBody] RecezentPostDTO recezentPostDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (!_recezentRepository.DodajRecezenta(recezentPostDTO))
            {
                ModelState.AddModelError("", "Greska!");
                return StatusCode(500, ModelState);
            }

            return Ok("Uspeh!");
        }

        [HttpPut]
        public IActionResult IzmeniRecezenta([FromBody] RecezentPutDTO recezentPutDTO)
        {
            if (!_recezentRepository.RecezentPostoji(recezentPutDTO.Id))
            {
                return NotFound();
            }

            if (!_recezentRepository.IzmeniRecezenta(recezentPutDTO))
            {
                ModelState.AddModelError("", "Greska");
                return StatusCode(500, ModelState);
            }
            return Ok("Uspeh!");
        }

        [HttpDelete("{id}")]
        public IActionResult IzbrisiRecezenta(int id)
        {
            if (!_recezentRepository.RecezentPostoji(id))
            {
                return NotFound();
            }

            if (!_recezentRepository.IzbrisiRecezenta(id))
            {
                ModelState.AddModelError("", "Greska!");
                return StatusCode(500, "");
            }
            return Ok("Uspeh!");
        }
    }
}
