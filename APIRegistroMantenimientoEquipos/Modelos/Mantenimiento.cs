using System.ComponentModel.DataAnnotations;

namespace APIRegistroMantenimientoEquipos.Modelos
{
    public class Mantenimiento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EquipoId { get; set; }
        public Equipo Equipo { get; set; }

        [Required]
        public int TrabajadorId { get; set; }
        public Trabajador Trabajador { get; set; }

        [Required]
        public DateTime FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        [Required]
        [StringLength(500)]
        public string Descripcion { get; set; }

        [Required]
        public int TipoId { get; set; }
        public TipoMantenimiento Tipo { get; set; }

        [Required]
        public int EstadoId { get; set; }  // Asegúrate de que esta propiedad sea única en el modelo.
        public EstadoMantenimiento Estado { get; set; }

        [StringLength(500)]
        public string Observaciones { get; set; }
    }


    public class EstadoMantenimiento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [StringLength(200)]
        public string Descripcion { get; set; }
    }

    public class TipoMantenimiento
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
