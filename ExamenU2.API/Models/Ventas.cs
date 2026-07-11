// Models/Ventas.cs
namespace ExamenU2.API.Models
{
    public class VentasInput
    {
        public string? NombreVendedor { get; set; }
        public List<double> VentasDiarias { get; set; } = new();
    }

    public class VentasResponse
    {
        public string? NombreVendedor { get; set; }
        public double TotalVendido { get; set; }
        public double PromedioDiario { get; set; }
        public double VentaMasAlta { get; set; }
        public double VentaMasBaja { get; set; }
        public int DiasSobreMeta { get; set; }
        public double Comision { get; set; } 
    }
}