
using Xunit;
using ApiMonoLegal.Services;
using ApiMonoLegal.Controllers;
using Microsoft.AspNetCore.Mvc;
using ApiMonoLegal.Models;
using Moq;

namespace UnitTestApiMonoLegal


{
    public class FacturaTest
    {


        [Fact]
        public void Get()
        {
 
            var clienteSettings = new Mock<IClienteSettings>();
            clienteSettings.Setup(s => s.Server).Returns("mongodb://localhost:27017");
            clienteSettings.Setup(s => s.Database).Returns("ClienteMonoLegal");
            clienteSettings.Setup(s => s.Collection).Returns("Factura");

            // Crear una instancia de facturaServices utilizando el mock de IClienteSettings
            var facturaServices = new FacturaServices(clienteSettings.Object);
            var facturaController = new FacturaController(facturaServices);

            var result = facturaController.Get();
            //retorna la lista de Facturas por tal motivo no es un valor nulo
            Assert.NotNull(result);

        }



        [Fact]
        public void Create()
        {

            //creo un objeto de tipo (FacturaService)
            var facturaServices = new Mock<IFacturaServices>();
            // creo un objeto de tipo factura
            var newFactura = new Factura
            {
                codigoFactura = "P-00003",
                cliente = "Daniel Felipe Cordoba",
                correo = "rjuanjoser@gmail.com",
                ciudad = "Paipa",
                nit = 10548,
                totalFactura = 3000,
                subTotal = 200,
                iva = 55,
                retencion = 12,
                fechaCreacion = null,
                estado = "primerrecordatorio",
                pagada = false,
                fechaPago = null
            };

   
            //Llamo a mi controlador y le envio mi objeto facturaServices
            var facturaController = new FacturaController(facturaServices.Object);
            //Conexion con el controlador para la creacion de la factura
            var result = facturaController.Create(newFactura);

            var actionResult = Assert.IsType<ActionResult<Factura>>(result);
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            var facturaResultante = Assert.IsType<Factura>(okResult.Value);
            //se comprueba que la nueva factura tiene los mismo datos que resultante retornada por el metodo Create
            Assert.Equal(newFactura, facturaResultante);


        }

        [Fact]
        public void Update()
        {
            var facturaServicesMock = new Mock<IFacturaServices>();
            var factura = new Factura
            {
                codigoFactura = "P-00003",
                cliente = "Daniel Felipe Cordoba",
                correo = "rjuanjoser@gmail.com",
                ciudad = "Paipa",
                nit = 10548,
                totalFactura = 3000,
                subTotal = 200,
                iva = 55,
                retencion = 12,
                fechaCreacion = null,
                estado = "primerrecordatorio",
                pagada = false,
                fechaPago = null
            };
            
            var controller = new FacturaController(facturaServicesMock.Object);
            var result = controller.Update(factura);
    
            var okResult = Assert.IsType<OkObjectResult>(result);
            var updatedFactura = Assert.IsType<Factura>(okResult.Value);


            Assert.Equal(factura.codigoFactura, updatedFactura.codigoFactura);

        }
    }
    }