using System.ComponentModel.DataAnnotations;

namespace APIRegistroMantenimientoEquipos.Modelos.Dtos
{
   
    public class LoginDTO
    {
        public string Correo { get; set; }
        public string Password { get; set; }
    }

  
}
