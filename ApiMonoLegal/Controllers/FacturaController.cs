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
    }
}
