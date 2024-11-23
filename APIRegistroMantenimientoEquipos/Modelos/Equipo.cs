using System.ComponentModel.DataAnnotations;

namespace APIRegistroMantenimientoEquipos.Modelos
{
    public class Equipo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(50)]
        public string Modelo { get; set; }

        [Required]
        [StringLength(50)]
        public string NumeroSerie { get; set; }

        [Required]
        public DateTime FechaAdquisicion { get; set; }

        [Required]
        public int EstadoId { get; set; }
        public EstadoEquipo Estado { get; set; }

        public ICollection<Mantenimiento> Mantenimientos { get; set; }
        public ICollection<Reparacion> Reparaciones { get; set; }
    }

    public class EstadoEquipo
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

