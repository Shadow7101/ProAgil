using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;
using ProAgil.WebAPI.Dtos;

namespace ProAgil.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController : ControllerBase
    {
        private readonly IProAgilRepository _repo;
        private readonly IMapper _mapper;
        public EventoController(IProAgilRepository repo, IMapper mapper)
        {
            this._mapper = mapper;
            this._repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _repo.GetAllEvento(true);

                var result = _mapper.Map<EventoDTO[]>(eventos);

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de Dados Falhou: {ex.Message}");
            }
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload()
        {
            try
            {
                if (Request.Form.Files.Count == 0)
                {
                    return BadRequest("O arquivo não foi enviado");
                }

                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                fileName = fileName.Replace("\"", string.Empty).Replace(" ", string.Empty);
                var fullPath = Path.Combine(pathToSave, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                return Ok(new { Salvo = true, FileName = fileName });
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Falha no upload: {ex.Message}");
            }
        }


        [HttpGet("{EventoId}")]
        public async Task<IActionResult> Get(int EventoId)
        {
            try
            {
                var evento = await _repo.GetEventoById(EventoId, true);

                var result = _mapper.Map<EventoDTO>(evento);

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de Dados Falhou: {ex.Message}");
            }
        }

        [HttpGet("getByTema/{tema}")]
        public async Task<IActionResult> Get(string tema)
        {
            try
            {
                var eventos = await _repo.GetAllEventoByTema(tema, true);

                var result = _mapper.Map<EventoDTO[]>(eventos);

                return Ok(result);
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de Dados Falhou: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(EventoDTO model)
        {
            try
            {
                var evento = _mapper.Map<Evento>(model);

                _repo.Add(evento);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/evento/{evento.Id}", _mapper.Map<EventoDTO>(evento));
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de Dados Falhou: {ex.Message}");
            }

            return BadRequest();
        }


        [HttpPut("{EventoId}")]
        public async Task<IActionResult> HttpPut(int EventoId, EventoDTO model)
        {
            try
            {
                var evento = await _repo.GetEventoById(EventoId, false);
                if (evento == null) return NotFound();

                _mapper.Map(model, evento);

                _repo.Update(evento);

                if (await _repo.SaveChangesAsync())
                {
                    return Created($"/api/evento/{model.Id}", _mapper.Map<EventoDTO>(evento));
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de Dados Falhou: {ex.Message}");
            }
            return BadRequest();
        }


        [HttpDelete("{EventoId}")]
        public async Task<IActionResult> Delete(int EventoId)
        {
            try
            {
                var evento = await _repo.GetEventoById(EventoId, false);
                if (evento == null) return NotFound();

                _repo.Delete(evento);

                if (await _repo.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, $"Banco de Dados Falhou: {ex.Message}");
            }

            return BadRequest();
        }



    }
}