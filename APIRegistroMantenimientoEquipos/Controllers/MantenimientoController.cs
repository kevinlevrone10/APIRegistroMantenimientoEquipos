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
    public class MantenimientoController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public MantenimientoController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<MantenimientoDTO>>> GetMantenimientos()
        {
            var mantenimientos = await _context.Mantenimientos
                .Include(m => m.Equipo)
                .Include(m => m.Trabajador)
                .Include(m => m.Estado)
                .Include(m => m.Tipo)
                .Select(m => new MantenimientoDTO
                {
                    Id = m.Id,
                    FechaInicio = m.FechaInicio,
                    FechaFin = m.FechaFin,
                    Descripcion = m.Descripcion,
                    Observaciones = m.Observaciones,
                    EquipoNombre = m.Equipo.Nombre,
                    TrabajadorNombre = $"{m.Trabajador.Nombre} {m.Trabajador.Apellido}",
                    EstadoNombre = m.Estado.Nombre,
                    TipoNombre = m.Tipo.Nombre
                })
                .ToListAsync();

            return Ok(mantenimientos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MantenimientoDTO>> GetMantenimiento(int id)
        {
            var mantenimiento = await _context.Mantenimientos
                .Include(m => m.Equipo)
                .Include(m => m.Trabajador)
                .Include(m => m.Estado)
                .Include(m => m.Tipo)
                .Where(m => m.Id == id)
                .Select(m => new MantenimientoDTO
                {
                    Id = m.Id,
                    FechaInicio = m.FechaInicio,
                    FechaFin = m.FechaFin,
                    Descripcion = m.Descripcion,
                    Observaciones = m.Observaciones,
                    EquipoNombre = m.Equipo.Nombre,
                    TrabajadorNombre = $"{m.Trabajador.Nombre} {m.Trabajador.Apellido}",
                    EstadoNombre = m.Estado.Nombre,
                    TipoNombre = m.Tipo.Nombre
                })
                .FirstOrDefaultAsync();

            if (mantenimiento == null)
            {
                return NotFound();
            }

            return Ok(mantenimiento);
        }

        [HttpPost]
        public async Task<ActionResult<MantenimientoDTO>> PostMantenimiento(MantenimientoCreateDTO mantenimientoDto)
        {
            var equipo = await _context.Equipos.FindAsync(mantenimientoDto.EquipoId);
            var trabajador = await _context.Trabajadores.FindAsync(mantenimientoDto.TrabajadorId);
            var estado = await _context.EstadosMantenimiento.FindAsync(mantenimientoDto.EstadoId);
            var tipo = await _context.TiposMantenimiento.FindAsync(mantenimientoDto.TipoId);

            if (equipo == null || trabajador == null || estado == null || tipo == null)
            {
                return BadRequest("Equipo, Trabajador, Estado o Tipo no válido.");
            }

            var mantenimiento = new Mantenimiento
            {
                FechaInicio = mantenimientoDto.FechaInicio,
                FechaFin = mantenimientoDto.FechaFin,
                Descripcion = mantenimientoDto.Descripcion,
                Observaciones = mantenimientoDto.Observaciones,
                EquipoId = mantenimientoDto.EquipoId,
                TrabajadorId = mantenimientoDto.TrabajadorId,
                EstadoId = mantenimientoDto.EstadoId,
                TipoId = mantenimientoDto.TipoId
            };

            _context.Mantenimientos.Add(mantenimiento);
            await _context.SaveChangesAsync();

            var mantenimientoCreated = new MantenimientoDTO
            {
                FechaInicio = mantenimiento.FechaInicio,
                FechaFin = mantenimiento.FechaFin,
                Descripcion = mantenimiento.Descripcion,
                Observaciones = mantenimiento.Observaciones,
                EquipoNombre = equipo.Nombre,
                TrabajadorNombre = $"{trabajador.Nombre} {trabajador.Apellido}",
                EstadoNombre = estado.Nombre,
                TipoNombre = tipo.Nombre
            };

            return CreatedAtAction(nameof(GetMantenimiento), new { id = mantenimiento.Id }, mantenimientoCreated);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutMantenimiento(int id, MantenimientoUpdateDTO mantenimientoDto)
        {
            var mantenimiento = await _context.Mantenimientos.FindAsync(id);

            if (mantenimiento == null)
            {
                return NotFound();
            }

            var estado = await _context.EstadosMantenimiento.FindAsync(mantenimientoDto.EstadoId);
            if (estado == null)
            {
                return BadRequest("Estado no válido.");
            }

            mantenimiento.FechaInicio = mantenimientoDto.FechaInicio;
            mantenimiento.FechaFin = mantenimientoDto.FechaFin;
            mantenimiento.Descripcion = mantenimientoDto.Descripcion;
            mantenimiento.Observaciones = mantenimientoDto.Observaciones;
            mantenimiento.EstadoId = mantenimientoDto.EstadoId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMantenimiento(int id)
        {
            var mantenimiento = await _context.Mantenimientos.FindAsync(id);

            if (mantenimiento == null)
            {
                return NotFound();
            }

            _context.Mantenimientos.Remove(mantenimiento);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpGet("EstadosMantenimiento")]
        public async Task<ActionResult<IEnumerable<EstadoMantenimientoDTO>>> GetEstadosMantenimiento()
        {
            var estados = await _context.EstadosMantenimiento
                .Select(e => new EstadoMantenimientoDTO
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Descripcion = e.Descripcion
                })
                .ToListAsync();

            return Ok(estados);
        }

        [HttpGet("TiposMantenimiento")]
        public async Task<ActionResult<IEnumerable<TipoMantenimientoDTO>>> GetTiposMantenimiento()
        {
            var tipos = await _context.TiposMantenimiento
                .Select(t => new TipoMantenimientoDTO
                {
                    Id = t.Id,
                    Nombre = t.Nombre,
                    Descripcion = t.Descripcion
                })
                .ToListAsync();

            return Ok(tipos);
        }


    }
}
