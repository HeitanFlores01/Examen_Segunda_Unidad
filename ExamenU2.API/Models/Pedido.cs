namespace ExamenU2.API.Models
{
    public class ArticuloInput
    {
        public string? NombreProducto { get; set; }
        public double PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
    }

    public class ArticuloDetalle
    {
        public string? NombreProducto { get; set; }
        public double PrecioUnitario { get; set; }
        public int Cantidad { get; set; }
        public double Subtotal { get; set; }
    }

    public class CotizacionResponse
    {
        public List<ArticuloDetalle> DetalleArticulos { get; set; } = new();
        public double SubtotalGeneral { get; set; }
        public string? PorcentajeDescuentoAplicado { get; set; }
        public double MontoISV { get; set; }
        public double TotalAPagar { get; set; }
    }
}