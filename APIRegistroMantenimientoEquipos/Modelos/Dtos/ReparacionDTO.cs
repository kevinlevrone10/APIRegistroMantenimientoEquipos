namespace APIRegistroMantenimientoEquipos.Modelos.Dtos
{
    public class ReparacionDTO
    {
        public int Id { get; set; }
        public int EquipoId { get; set; }
        public string EquipoNombre { get; set; }
        public int TrabajadorId { get; set; }
        public string TrabajadorNombre { get; set; }
        public int EstadoId { get; set; }
        public string EstadoNombre { get; set; }
        public string Descripcion { get; set; }
        public string Diagnostico { get; set; } // Nuevo
        public string Solucion { get; set; }    // Nuevo
        public decimal? CostoReparacion { get; set; } // Nuevo
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Observaciones { get; set; }
    }

    public class CrearActualizarReparacionDTO
    {
        public int EquipoId { get; set; }
        public int TrabajadorId { get; set; }
        public int EstadoId { get; set; }
        public string Descripcion { get; set; }
        public string Diagnostico { get; set; } // Nuevo
        public string Solucion { get; set; }    // Nuevo
        public decimal? CostoReparacion { get; set; } // Nuevo
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Observaciones { get; set; }
    }


    public class EstadoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

}
