using Microsoft.AspNetCore.Mvc;
using ExamenU2.API.Models;

namespace ExamenU2.API.Controllers
{
    [ApiController]
    [Route("api/proyeccion")]
    public class ProyeccionController : ControllerBase
    {
        [HttpPost("inventario")]
        public IActionResult ProyeccionInventario([FromBody] ProyeccionInput input)
        {
            // Validamos las entradas básicas
            if (input == null || input.PromedioVentaDiaria <= 0)
                return BadRequest("Los datos de entrada son inválidos o el promedio de venta debe ser mayor a cero.");

            // Validamos si estamos ya por abajo del punto de reorden 
            var respuesta = new ProyeccionResponse
            {
                NombreProducto = input.NombreProducto
            };

            int stockActual = input.ExistenciaActual;

            if (stockActual <= input.PuntoDeReorden)
            {
                respuesta.DiaAlcanzoPuntoReorden = "El stock inicial ya se encuentra en el punto de reorden o por debajo de él.";
                return Ok(respuesta);
            }

            // Simulamos el consumo diario 
            int dia = 0;
            while (stockActual > input.PuntoDeReorden)
            {
                // Reducción diaria del stock
                dia++;
                stockActual -= input.PromedioVentaDiaria;

                respuesta.DetalleDiaADia.Add(new DiaSimulacion
                {
                    // Si el stock cae en negativo lo reportamos como 0
                    NumDia = dia,
                    StockRestante = stockActual < 0 ? 0 : stockActual
                });
            }

            // Reportamos el dia exacto en que se debe generar el pedido 
            respuesta.DiaAlcanzoPuntoReorden = $"Día {dia}";
            return Ok(respuesta);
        }
    }
}