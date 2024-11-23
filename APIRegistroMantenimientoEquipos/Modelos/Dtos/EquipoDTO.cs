using System.ComponentModel.DataAnnotations;

namespace APIRegistroMantenimientoEquipos.Modelos.Dtos
{
    public class EquipoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Modelo { get; set; }
        public string NumeroSerie { get; set; }
        public DateTime FechaAdquisicion { get; set; }
        [Required]
        public int EstadoId { get; set; }
    }


    public class EquipoDTOCreate
    {
        public string Nombre { get; set; }
        public string Modelo { get; set; }
        public string NumeroSerie { get; set; }
        public DateTime FechaAdquisicion { get; set; }
        [Required]
        public int EstadoId { get; set; }
    }
}
