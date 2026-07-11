using Microsoft.AspNetCore.Mvc;
using ExamenU2.API.Models;

namespace ExamenU2.API.Controllers
{
    [ApiController]
    [Route("api/inventario")]
    public class InventarioController : ControllerBase
    {
        [HttpPost("analizar")]
        public IActionResult AnalizarInventario([FromBody] List<HerramientaInput> herramientas)
        {
            // Validamos que el cuerpo de la petición contenga datos
            if (herramientas == null || herramientas.Count == 0)
                return BadRequest("La lista de herramientas no puede estar vacia.");

            var respuesta = new InventarioResponse();
            double maxMargen = -1;

            // Recorremos la lista con el foreach
            foreach (var h in herramientas)
            {
                // Realizamos los cálculos solicitados 
                // Calculo de la utilidad
                double utilidad = h.PrecioVenta - h.PrecioCompra;
                
                // Calculo del margen de ganancia
                double margen = h.PrecioCompra > 0 ? (utilidad / h.PrecioCompra) * 100 : 0;
                
                // Calculo del valor del inventario
                double valorInventario = h.PrecioVenta * h.Cantidad;

                respuesta.Detalle.Add(new HerramientaDetalle
                {
                    Nombre = h.Nombre,
                    Utilidad = utilidad,
                    Margen = margen,
                    ValorInventario = valorInventario
                });

                // Acumulamos el valor real de todo el inventario
                respuesta.ValorTotalInventario += valorInventario;

                // Con un if validamos para encontrar el mayor margen
                if (margen > maxMargen)
                {
                    maxMargen = margen;
                    respuesta.HerramientaMayorMargen = h.Nombre;
                }
            }

            return Ok(respuesta);
        }
    }
}