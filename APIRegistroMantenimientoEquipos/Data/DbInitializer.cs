using APIRegistroMantenimientoEquipos.Data;
using APIRegistroMantenimientoEquipos.Modelos;
using System.Security.Cryptography;
using System.Text;

namespace APIRegistroMantenimientoEquipos.Data
{
    public class DbInitializer
    {
        private readonly ApplicationDbContext _context;

        public DbInitializer( ApplicationDbContext context)
        {
            _context = context;
        }


        public void Seed()
        {
     
            if (!_context.Usuarios.Any()) 
            {
                var admin = new Administrador
                {
                    Nombre = "Admin",
                    Correo = "administradorprincipal@gmail.com",
                    PasswordHash = HashPassword("Adminprincipal123x")
                };

                _context.Usuarios.Add(admin); 
                _context.SaveChanges();
            }
        }

        public  string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }





    }
}
