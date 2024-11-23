using System.ComponentModel.DataAnnotations;

namespace APIRegistroMantenimientoEquipos.Modelos.Dtos
{
    public class CrearTrabajadorDto
    {
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Apellido { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Correo { get; set; }

        [Required]
        [StringLength(20)]
        public string Telefono { get; set; }

        [Required]
        [StringLength(100)]
        public string Cargo { get; set; }

        [Required]
        public int EstadoId { get; set; }

        [Required]
        public DateTime FechaContratacion { get; set; }
    }

    public class TrabajadorDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Correo { get; set; }

        public string Telefono { get; set; }

        public string Cargo { get; set; }

        public string EstadoNombre { get; set; }  // Solo el nombre del estado

        public DateTime FechaContratacion { get; set; }

    }

    public class TrabajadorUpdateDto
    {

        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Apellido { get; set; }

        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required]
        public string Cargo { get; set; }

        [Required]
        public int EstadoId { get; set; }

        [Required]
        public DateTime FechaContratacion { get; set; }
    }

}
