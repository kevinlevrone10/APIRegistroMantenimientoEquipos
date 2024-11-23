using APIRegistroMantenimientoEquipos.Data;
using APIRegistroMantenimientoEquipos.Modelos;
using APIRegistroMantenimientoEquipos.Modelos.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIRegistroMantenimientoEquipos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EquipoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EquipoDTO>>> GetEquipos()
        {
            var equipos = await _context.Equipos
                .Include(e => e.Estado)
                .Select(e => new EquipoDTO
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Modelo = e.Modelo,
                    NumeroSerie = e.NumeroSerie,
                    FechaAdquisicion = e.FechaAdquisicion,
                    EstadoId = e.EstadoId
                })
                .ToListAsync();

            return Ok(equipos);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<EquipoDTO>> GetEquipo(int id)
        {
            var equipo = await _context.Equipos
                .Include(e => e.Estado)
                .Where(e => e.Id == id)
                .Select(e => new EquipoDTO
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Modelo = e.Modelo,
                    NumeroSerie = e.NumeroSerie,
                    FechaAdquisicion = e.FechaAdquisicion,
                    EstadoId = e.EstadoId
                })
                .FirstOrDefaultAsync();
            if (equipo == null)
            {
                return NotFound();
            }

            return Ok(equipo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutEquipo(int id, EquipoDTO equipoDto)
        {
            if (id != equipoDto.Id)
            {
                return BadRequest();
            }

            var equipo = await _context.Equipos.FindAsync(id);

            if (equipo == null)
            {
                return NotFound();
            }

            // Actualizar propiedades del equipo
            equipo.Nombre = equipoDto.Nombre;
            equipo.Modelo = equipoDto.Modelo;
            equipo.NumeroSerie = equipoDto.NumeroSerie;
            equipo.FechaAdquisicion = equipoDto.FechaAdquisicion;
            equipo.EstadoId = equipoDto.EstadoId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<EquipoDTO>> PostEquipo(EquipoDTOCreate equipoDto)
        {
            var estado = await _context.EstadosEquipo
                .FirstOrDefaultAsync(e => e.Id == equipoDto.EstadoId);

            if (estado == null)
            {
                return BadRequest("Estado no válido.");
            }

            var equipo = new Equipo
            {
                Nombre = equipoDto.Nombre,
                Modelo = equipoDto.Modelo,
                NumeroSerie = equipoDto.NumeroSerie,
                FechaAdquisicion = equipoDto.FechaAdquisicion,
                EstadoId = estado.Id
            };

            _context.Equipos.Add(equipo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEquipo), new { id = equipo.Id }, equipoDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipo(int id)
        {
            var equipo = await _context.Equipos.FindAsync(id);

            if (equipo == null)
            {
                return NotFound();
            }

            _context.Equipos.Remove(equipo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EquipoExists(int id)
        {
            return _context.Equipos.Any(e => e.Id == id);
        }

    }
}
