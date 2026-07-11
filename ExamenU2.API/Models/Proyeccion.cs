namespace ExamenU2.API.Models
{
    public class ProyeccionInput
    {
        public string? NombreProducto { get; set; } 
        public int ExistenciaActual { get; set; }
        public int PromedioVentaDiaria { get; set; }
        public int PuntoDeReorden { get; set; }
    }

    public class DiaSimulacion
    {
        public int NumDia { get; set; }
        public int StockRestante { get; set; }
    }

    public class ProyeccionResponse
    {
        public string? NombreProducto { get; set; }
        public List<DiaSimulacion> DetalleDiaADia { get; set; } = new();
        public string? DiaAlcanzoPuntoReorden { get; set; }
    }
}