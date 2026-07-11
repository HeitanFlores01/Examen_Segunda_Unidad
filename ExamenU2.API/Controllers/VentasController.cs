using Microsoft.AspNetCore.Mvc;
using ExamenU2.API.Models;

namespace ExamenU2.API.Controllers
{
    [ApiController]
    [Route("api/ventas")]
    public class VentasController : ControllerBase
    {
        [HttpPost("analizar")]
        public IActionResult AnalizarVentas([FromBody] VentasInput input)
        {
            // Validamos que hay ventas registradas 
            if (input == null || input.VentasDiarias.Count == 0)
                return BadRequest("Debe ingresar las ventas diarias.");

            // Iniciamos variables de control
            double totalVendido = 0;
            double ventaMasAlta = double.MinValue;
            double ventaMasBaja = double.MaxValue;
            int diasSobreMeta = 0;

            // Usamos un ciclo for para recorrer las ventas diarias del mes
            for (int i = 0; i < input.VentasDiarias.Count; i++)
            {
                double venta = input.VentasDiarias[i];
                totalVendido += venta;

                // Determinamos los extremos y las ventas mayores a 3000
                if (venta > ventaMasAlta) ventaMasAlta = venta;
                if (venta < ventaMasBaja) ventaMasBaja = venta;
                if (venta > 3000.00) diasSobreMeta++;
            }

            double promedioDiario = totalVendido / input.VentasDiarias.Count;
            double comision = 0;

            // Calculamos la comisión dependiendo de los rangos 
            if (totalVendido <= 50000.00)
                comision = totalVendido * 0.03;
            else if (totalVendido > 50000.00 && totalVendido <= 100000.00)
                comision = totalVendido * 0.05;
            else
                comision = totalVendido * 0.08;

            // Retornamos las respuestas
            var respuesta = new VentasResponse
            {
                NombreVendedor = input.NombreVendedor,
                TotalVendido = totalVendido,
                PromedioDiario = promedioDiario,
                VentaMasAlta = ventaMasAlta,
                VentaMasBaja = ventaMasBaja,
                DiasSobreMeta = diasSobreMeta,
                Comision = comision,
            };

            return Ok(respuesta);
        }
    }
}