using APIRegistroMantenimientoEquipos.Data;
using APIRegistroMantenimientoEquipos.Modelos.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace APIRegistroMantenimientoEquipos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AuthController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDTO loginDto)
        {
            var administrador = _context.Usuarios.FirstOrDefault(u => u.Correo == loginDto.Correo);

            if (administrador == null)
            {
                return Unauthorized("Usuario no encontrado");
            }

            if (administrador.PasswordHash != HashPassword(loginDto.Password))
            {
                return Unauthorized("Contraseña incorrecta");
            }

            return Ok("Inicio de sesión exitoso");
        }


        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }



    }
}
