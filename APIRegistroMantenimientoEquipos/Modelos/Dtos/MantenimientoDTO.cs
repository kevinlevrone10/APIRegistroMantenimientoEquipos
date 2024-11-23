namespace APIRegistroMantenimientoEquipos.Modelos.Dtos
{
    public class MantenimientoDTO
    {
        public int Id { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Descripcion { get; set; }
        public string Observaciones { get; set; }

        // Relaciones
        public string EquipoNombre { get; set; }
        public string TrabajadorNombre { get; set; }
        public string EstadoNombre { get; set; }
        public string TipoNombre { get; set; }

    }


    public class MantenimientoCreateDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Descripcion { get; set; }
        public string Observaciones { get; set; }
        public int EquipoId { get; set; }
        public int TrabajadorId { get; set; }
        public int EstadoId { get; set; }
        public int TipoId { get; set; }
    }

    public class MantenimientoUpdateDTO
    {
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string Descripcion { get; set; }
        public string Observaciones { get; set; }
        public int EstadoId { get; set; }
    }

    public class TipoMantenimientoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }

    public class EstadoMantenimientoDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }




}
