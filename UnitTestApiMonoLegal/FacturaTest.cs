
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
            //ESTA PRUEBA ES SATISFACTORIA SI EN LA BASE DE DATOS HAY UNA O MAS FACTURA 
            var clienteSettings = new Mock<IClienteSettings>();
            clienteSettings.Setup(s => s.Server).Returns("mongodb://localhost:27017");
            clienteSettings.Setup(s => s.Database).Returns("ClienteMonoLegal");
            clienteSettings.Setup(s => s.Collection).Returns("Factura");

            // Crear una instancia de facturaServices utilizando el mock de IClienteSettings
            var emailServices = new EmailService();
            var facturaServices = new FacturaServices(clienteSettings.Object, emailServices);
            var facturaController = new FacturaController(facturaServices);

            var result = facturaController.Get();
            //esta prueba compreba que la lista no sea nula (vacia)
            Assert.NotNull(result);
            //comprueba que el metodo get retorne una accion result de tipo lista facturas
            var actionResult = Assert.IsType<ActionResult<List<Factura>>>(result);
            //si la respuesta del metodo es ok captura el resultado del mismo (la lista de facturas)
            var okResult = Assert.IsType<OkObjectResult>(actionResult.Result);
            //captura la lista de las facturas 
            var facturasResultantes = Assert.IsType<List<Factura>>(okResult.Value);
            //comprobamos que las lista de facturas tenga almenos una factura
            Assert.True(facturasResultantes.Count > 0);

        }



        [Fact]
        public void Create()
        {

            //creo un objeto de tipo (FacturaService)
            var facturaServices = new Mock<IFacturaServices>();
            // creo un objeto de tipo factura
            var newFactura = new Factura(
                "P-00003",
                "Daniel Felipe Cordoba",
                "rjuanjoser@gmail.com",
                "Paipa",
                10548,
                3000,
                200,
                55,
                12,
                null,
                "primerrecordatorio",
                false,
                null
            );


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
            var factura = new Factura(
                "P-00003",
                "Daniel Felipe Cordoba",
                "rjuanjoser@gmail.com",
                "Paipa",
                10548,
                3000,
                200,
                55,
                12,
                null,
                "primerrecordatorio",
                false,
                null
            );
            
            var controller = new FacturaController(facturaServicesMock.Object);
            var result = controller.Update(factura);
    
            var okResult = Assert.IsType<OkObjectResult>(result);
            var updatedFactura = Assert.IsType<Factura>(okResult.Value);


            Assert.Equal(factura.codigoFactura, updatedFactura.codigoFactura);

        }

        [Fact]
        public void Delete()
        {
            var facturaServices = new Mock<IFacturaServices>();
            var newfactura = new Factura(
                "P-00003",
                "Daniel Felipe Cordoba",
                "rjuanjoser@gmail.com",
                "Paipa",
                10548,
                3000,
                200,
                55,
                12,
                null,
                "primerrecordatorio",
                false,
                null
            );

            //Llamo a mi controlador y le envio mi objeto facturaServices
            var facturaController = new FacturaController(facturaServices.Object);
            //Creo una factura
                     
            facturaController.Create(newfactura);

            //elimino dicha factura
            var deleteFactura = facturaController.Delete(newfactura.codigoFactura);

            var okResult = Assert.IsType<OkObjectResult>(deleteFactura);
            var codigoFacturaEliminada = Assert.IsType<String>(okResult.Value);

            //miramos que el codigo de la factura sea el mismo que el de la factura eliminada 
            Assert.Equal(newfactura.codigoFactura, codigoFacturaEliminada);

        }

        [Fact]
        public void sendEmail()
        {

            //pendiente 
        }
    }
    }