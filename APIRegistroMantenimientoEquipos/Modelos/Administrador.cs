using System.ComponentModel.DataAnnotations;

namespace APIRegistroMantenimientoEquipos.Modelos
{
    public class Administrador
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Correo { get; set; }

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }

    }
}
