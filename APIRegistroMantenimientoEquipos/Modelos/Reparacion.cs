using System.ComponentModel.DataAnnotations;

namespace APIRegistroMantenimientoEquipos.Modelos
{
    public class Reparacion
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
        public string DescripcionProblema { get; set; }

        [StringLength(500)]
        public string Diagnostico { get; set; }

        [StringLength(500)]
        public string Solucion { get; set; }

        [Required]
        public int EstadoId { get; set; }
        public EstadoReparacion Estado { get; set; }

        [StringLength(500)]
        public string Observaciones { get; set; }

        public decimal? CostoReparacion { get; set; }
    }


    public class EstadoReparacion
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
