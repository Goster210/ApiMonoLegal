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
        //CREAR UNA FACTURA
        [HttpPost("crear")]
        public ActionResult <Factura> Create(Factura factura) {

            _facturaServices.Create(factura);
            return Ok(factura);
        }
        //EDITAR UNA FACTURA
        [HttpPut("editar")]
        public ActionResult Update(Factura factura)
        {
            _facturaServices.Update(factura.codigoFactura, factura);
            return Ok(factura);
        }
        //ELIMINAR UNA FACTURA
        [HttpDelete("eliminar/{codigoFactura}")]
        public ActionResult Delete(String codigoFactura)
        {
            _facturaServices.Delete(codigoFactura);
            return Ok();
        }
        //ENVIAR CORREO PARA MODIFICAR ESTADOS DE LAS FACTURAS
        [HttpGet("enviarCorreo/{codigoFactura}")]
        public ActionResult SendEmail(String codigoFactura)
        {
            _facturaServices.SendEmail(codigoFactura);


            return Ok();
        }

    }
}
