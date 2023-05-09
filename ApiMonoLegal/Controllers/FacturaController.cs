using ApiMonoLegal.Models;
using ApiMonoLegal.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiMonoLegal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        public FacturaServices _facturaServices;

        public FacturaController(FacturaServices facturaServices) 
        {
            _facturaServices = facturaServices;
        }

        [HttpGet]
        public ActionResult<List<Factura>> Get() {
            return _facturaServices.Get();
        }

        [HttpPost("crear")]
        public ActionResult <Factura> Create(Factura factura) {

            _facturaServices.Create(factura);
            return Ok(factura);
        }

        [HttpPut("editar")]
        public ActionResult Update(Factura factura)
        {
            _facturaServices.Update(factura.codigoFactura, factura);
            return Ok(factura);
        }

        [HttpDelete("eliminar/{codigoFactura}")]
        public ActionResult Delete(String codigoFactura)
        {
            _facturaServices.Delete(codigoFactura);
            return Ok();
        }

    }
}
