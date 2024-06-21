using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vozila.DTOs;
using Vozila.Interfaces;

namespace Vozila.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KategorijaController : ControllerBase
    {
        private readonly IKategorijaRepository _kategorijaRepository;
        private readonly IMapper _mapper;

        public KategorijaController(IKategorijaRepository kategorijaRepository,IMapper mapper)
        {
            this._kategorijaRepository = kategorijaRepository;
            this._mapper = mapper;
        }
        [HttpGet]
        [ProducesResponseType(200,Type=typeof(ICollection<KategorijaGetDTO>))]
        public IActionResult VratiSveKategorije()
        {
            var sveKategorije = _mapper.Map<ICollection<KategorijaGetDTO>>(_kategorijaRepository.VratiSveKategorije());
            return Ok(sveKategorije);
        }

        [HttpGet("{id}")]
        public IActionResult VratiKategorijuPoId(int id)
        {
            if (!_kategorijaRepository.KategorijaPostoji(id))
            {
                return NotFound();
            }
            var kategorija = _mapper.Map<KategorijaGetDTO>(_kategorijaRepository.VratiKategoriju(id));
            return Ok(kategorija);
        }

        [HttpPost]
        public IActionResult DodajKategoriju([FromBody] KategorijaPostDTO kategorijaPostDTO)
        {
            if(kategorijaPostDTO == null)
            {
                return BadRequest();
            };
            if (!_kategorijaRepository.DodajKategoriju(kategorijaPostDTO))
            {
                ModelState.AddModelError("", "Greska!");
                return StatusCode(500, ModelState);
            }
            return Ok("Uspeh!");
        }
        [HttpPut]
        public IActionResult IzmeniKategoriju([FromBody] KategorijaPutDTO kategorijaPutDTO)
        {
            if (!_kategorijaRepository.KategorijaPostoji(kategorijaPutDTO.Id))
            {
                return NotFound();
            }

            if (!_kategorijaRepository.IzmeniKategoriju(kategorijaPutDTO))
            {
                ModelState.AddModelError("", "Greska");
                return StatusCode(5000, ModelState);
            }
            return Ok("Uspeh!");
        }

        [HttpDelete("{id}")]
        public IActionResult IzbrisiKategoriju(int id)
        {
            if (!_kategorijaRepository.KategorijaPostoji(id))
            {
                return NotFound();
            }

            if (!_kategorijaRepository.IzbrisiKategoriju(id))
            {
                ModelState.AddModelError("", "Greska");
                return StatusCode(500, ModelState);
            }
            return Ok("Uspeh!");
        }
    }
}
