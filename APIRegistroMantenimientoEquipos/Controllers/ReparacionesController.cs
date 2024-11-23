using APIRegistroMantenimientoEquipos.Data;
using APIRegistroMantenimientoEquipos.Modelos.Dtos;
using APIRegistroMantenimientoEquipos.Modelos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIRegistroMantenimientoEquipos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReparacionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReparacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReparacionDTO>>> GetReparaciones()
        {
            return await _context.Reparaciones
                .Include(r => r.Equipo)
                .Include(r => r.Trabajador)
                .Include(r => r.Estado)
                .Select(r => new ReparacionDTO
                {
                    Id = r.Id,
                    EquipoId = r.EquipoId,
                    EquipoNombre = r.Equipo.Nombre,
                    TrabajadorId = r.TrabajadorId,
                    TrabajadorNombre = r.Trabajador.Nombre + " " + r.Trabajador.Apellido,
                    EstadoId = r.EstadoId,
                    EstadoNombre = r.Estado.Nombre,
                    Descripcion = r.DescripcionProblema,
                    Diagnostico = r.Diagnostico,
                    Solucion = r.Solucion,      
                    CostoReparacion = r.CostoReparacion, 
                    FechaInicio = r.FechaInicio,
                    FechaFin = r.FechaFin,
                    Observaciones = r.Observaciones
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReparacionDTO>> GetReparacion(int id)
        {
            var reparacion = await _context.Reparaciones
                .Include(r => r.Equipo)
                .Include(r => r.Trabajador)
                .Include(r => r.Estado)
                .Where(r => r.Id == id)
                .Select(r => new ReparacionDTO
                {
                    Id = r.Id,
                    EquipoId = r.EquipoId,
                    EquipoNombre = r.Equipo.Nombre,
                    TrabajadorId = r.TrabajadorId,
                    TrabajadorNombre = r.Trabajador.Nombre + " " + r.Trabajador.Apellido,
                    EstadoId = r.EstadoId,
                    EstadoNombre = r.Estado.Nombre,
                    Descripcion = r.DescripcionProblema,
                    Diagnostico = r.Diagnostico, 
                    Solucion = r.Solucion,       
                    CostoReparacion = r.CostoReparacion, 
                    FechaInicio = r.FechaInicio,
                    FechaFin = r.FechaFin,
                    Observaciones = r.Observaciones
                })
                .FirstOrDefaultAsync();

            if (reparacion == null)
            {
                return NotFound();
            }

            return reparacion;
        }


        [HttpPost]
        public async Task<ActionResult<Reparacion>> PostReparacion(CrearActualizarReparacionDTO dto)
        {
            var reparacion = new Reparacion
            {
                EquipoId = dto.EquipoId,
                TrabajadorId = dto.TrabajadorId,
                EstadoId = dto.EstadoId,
                DescripcionProblema = dto.Descripcion,
                Diagnostico = dto.Diagnostico, 
                Solucion = dto.Solucion,       
                CostoReparacion = dto.CostoReparacion,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
                Observaciones = dto.Observaciones
            };

            _context.Reparaciones.Add(reparacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReparacion), new { id = reparacion.Id }, reparacion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutReparacion(int id, CrearActualizarReparacionDTO dto)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var reparacion = await _context.Reparaciones.FindAsync(id);
            if (reparacion == null)
            {
                return NotFound();
            }

            reparacion.EquipoId = dto.EquipoId;
            reparacion.TrabajadorId = dto.TrabajadorId;
            reparacion.EstadoId = dto.EstadoId;
            reparacion.DescripcionProblema = dto.Descripcion;
            reparacion.Diagnostico = dto.Diagnostico; 
            reparacion.Solucion = dto.Solucion;      
            reparacion.CostoReparacion = dto.CostoReparacion; 
            reparacion.FechaInicio = dto.FechaInicio;
            reparacion.FechaFin = dto.FechaFin;
            reparacion.Observaciones = dto.Observaciones;

            _context.Entry(reparacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReparacionExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReparacion(int id)
        {
            var reparacion = await _context.Reparaciones.FindAsync(id);
            if (reparacion == null)
            {
                return NotFound();
            }

            _context.Reparaciones.Remove(reparacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("EstadosReparaciones")]
        public async Task<ActionResult<IEnumerable<EstadoDTO>>> GetEstados()
        {
            return await _context.EstadosReparacion
                .Select(e => new EstadoDTO
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Descripcion = e.Descripcion
                })
                .ToListAsync();
        }

        private bool ReparacionExists(int id)
        {
            return _context.Reparaciones.Any(e => e.Id == id);
        }




    }
}
