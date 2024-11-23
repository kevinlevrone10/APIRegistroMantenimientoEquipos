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
    public class TrabajadoresController : ControllerBase
    {

        private readonly ApplicationDbContext _context;


        public TrabajadoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetEstadosTrabajadores")]
        public async Task<ActionResult<IEnumerable<EstadoTrabajador>>> GetEstadosTrabajadores()
        {
            return await _context.EstadoTrabajador.ToListAsync();
        }

        // GET: api/Trabajadores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TrabajadorDto>>> GetTrabajadores()
        {
        
            var trabajadores = await _context.Trabajadores
                .Include(t => t.Estado)  
                .ToListAsync();

            var trabajadoresDto = trabajadores.Select(t => new TrabajadorDto
            {
                Id = t.Id,
                Nombre = t.Nombre,
                Apellido = t.Apellido,
                Correo = t.Correo,
                Telefono = t.Telefono,
                Cargo = t.Cargo,
                EstadoNombre = t.Estado.Nombre,  
                FechaContratacion = t.FechaContratacion
            }).ToList();

            return Ok(trabajadoresDto);
        }


        [HttpPost]
        public async Task<ActionResult<Trabajador>> PostTrabajador(CrearTrabajadorDto trabajadorDto)
        {
            if (trabajadorDto == null)
            {
                return BadRequest("El trabajador es nulo.");
            }

            var estado = _context.EstadoTrabajador.Find(trabajadorDto.EstadoId);
            if (estado == null)
            {
                return BadRequest("El Estado no existe.");
            }

            var trabajador = new Trabajador
            {
                Nombre = trabajadorDto.Nombre,
                Apellido = trabajadorDto.Apellido,
                Correo = trabajadorDto.Correo,
                Telefono = trabajadorDto.Telefono,
                Cargo = trabajadorDto.Cargo,
                EstadoId = trabajadorDto.EstadoId,
                FechaContratacion = trabajadorDto.FechaContratacion
            };

            _context.Trabajadores.Add(trabajador);
            _context.SaveChanges();

            return Ok(new TrabajadorDto
            {
                Id = trabajador.Id,
                Nombre = trabajador.Nombre,
                Apellido = trabajador.Apellido,
                Correo = trabajador.Correo,
                Telefono = trabajador.Telefono,
                Cargo = trabajador.Cargo,
                FechaContratacion = trabajador.FechaContratacion
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TrabajadorDto>> GetTrabajador(int id)
        {
            var trabajador = await _context.Trabajadores
                .Include(t => t.Estado)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (trabajador == null)
            {
                return NotFound();
            }

            var trabajadorDto = new TrabajadorDto
            {
                Id = trabajador.Id,
                Nombre = trabajador.Nombre,
                Apellido = trabajador.Apellido,
                Correo = trabajador.Correo,
                Telefono = trabajador.Telefono,
                Cargo = trabajador.Cargo,
                EstadoNombre = trabajador.Estado.Nombre,
                FechaContratacion = trabajador.FechaContratacion
            };

            return Ok(trabajadorDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrabajador(int id, TrabajadorUpdateDto trabajadorDto)
        {
            if (id != trabajadorDto.Id)
            {
                return BadRequest();
            }

            var trabajador = await _context.Trabajadores.FindAsync(id);
            if (trabajador == null)
            {
                return NotFound();
            }

            trabajador.Nombre = trabajadorDto.Nombre;
            trabajador.Apellido = trabajadorDto.Apellido;
            trabajador.Correo = trabajadorDto.Correo;
            trabajador.Telefono = trabajadorDto.Telefono;
            trabajador.Cargo = trabajadorDto.Cargo;
            trabajador.EstadoId = trabajadorDto.EstadoId;
            trabajador.FechaContratacion = trabajadorDto.FechaContratacion;

            _context.Entry(trabajador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrabajadorExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrabajador(int id)
        {
            var trabajador = await _context.Trabajadores.FindAsync(id);
            if (trabajador == null)
            {
                return NotFound();
            }

            _context.Trabajadores.Remove(trabajador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TrabajadorExists(int id)
        {
            return _context.Trabajadores.Any(e => e.Id == id);
        }

     



    }
}
