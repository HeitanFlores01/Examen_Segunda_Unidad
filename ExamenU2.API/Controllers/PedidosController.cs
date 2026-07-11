using Microsoft.AspNetCore.Mvc;
using ExamenU2.API.Models;

namespace ExamenU2.API.Controllers
{
    [ApiController]
    [Route("api/pedidos")]
    public class PedidosController : ControllerBase
    {
        [HttpPost("cotizar")]
        public IActionResult CotizarPedido([FromBody] List<ArticuloInput> articulos)
        {
            // Validamos que el pedido contenta al menos un articulo
            if (articulos == null || articulos.Count == 0)
                return BadRequest("El pedido debe contener al menos un artículo.");

            var respuesta = new CotizacionResponse();
            double subtotalGeneral = 0;
            int totalUnidades = 0;

            // Recorremos para calcular el costo individual y general
            foreach (var art in articulos)
            {
                double subtotalArticulo = art.PrecioUnitario * art.Cantidad;
                subtotalGeneral += subtotalArticulo;
                totalUnidades += art.Cantidad;

                respuesta.DetalleArticulos.Add(new ArticuloDetalle
                {
                    NombreProducto = art.NombreProducto,
                    PrecioUnitario = art.PrecioUnitario,
                    Cantidad = art.Cantidad,
                    Subtotal = subtotalArticulo
                });
            }

            // Calculamos los descuentos en los rangos solicitados  
            double porcentajeDescuento = 0;
            if (totalUnidades >= 10 && totalUnidades <= 29)
                porcentajeDescuento = 0.05;
            else if (totalUnidades >= 30)
                porcentajeDescuento = 0.10;

            // Aplicar descuento y luego calcular el 15% de ISV
            double subtotalConDescuento = subtotalGeneral * (1 - porcentajeDescuento);
            double isv = subtotalConDescuento * 0.15;
            double totalAPagar = subtotalConDescuento + isv;

            respuesta.SubtotalGeneral = subtotalGeneral;
            respuesta.PorcentajeDescuentoAplicado = $"{porcentajeDescuento * 100}%";
            respuesta.MontoISV = isv;
            respuesta.TotalAPagar = totalAPagar;

            return Ok(respuesta);
        }
    }
}