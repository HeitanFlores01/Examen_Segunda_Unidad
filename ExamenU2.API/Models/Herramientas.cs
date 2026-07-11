namespace ExamenU2.API.Models
{
    public class HerramientaInput
    {
        public string? Nombre { get; set; }
        public double PrecioCompra { get; set; }
        public double PrecioVenta { get; set; }
        public int Cantidad { get; set; }
    }

    public class HerramientaDetalle
    {
        public string? Nombre { get; set; }
        public double Utilidad { get; set; }
        public double Margen { get; set; }
        public double ValorInventario { get; set; }
    }

    public class InventarioResponse
    {
        public List<HerramientaDetalle> Detalle { get; set; } = new();
        public double ValorTotalInventario { get; set; }
        public string? HerramientaMayorMargen { get; set; } 
    }
}