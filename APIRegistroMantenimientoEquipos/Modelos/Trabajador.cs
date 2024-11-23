using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APIRegistroMantenimientoEquipos.Modelos
{
    public class Trabajador
    {
        [Key]
        public int Id { get; set; }

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

        [JsonIgnore]
        public EstadoTrabajador Estado { get; set; }

        [Required]
        public DateTime FechaContratacion { get; set; }

        [JsonIgnore]
        public ICollection<Mantenimiento> Mantenimientos { get; set; }
        [JsonIgnore]
        public ICollection<Reparacion> Reparaciones { get; set; }
    }


    public class EstadoTrabajador
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [StringLength(200)]
        public string Descripcion { get; set; } 
    }



}
