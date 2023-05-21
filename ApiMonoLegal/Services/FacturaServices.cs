using ApiMonoLegal.Models;
using MongoDB.Driver;
using System.Net;
using System.Net.Mail;

namespace ApiMonoLegal.Services
{
    public class FacturaServices:IFacturaServices
    {
        private IMongoCollection<Factura> _Facturas;

        public FacturaServices(IClienteSettings settings) 
        {
            
            var clienteMongo = new MongoClient(settings.Server);
            //conexion con la base de datos
            var db = clienteMongo.GetDatabase(settings.Database);
            //conexion con la coleccion (facturas)
            _Facturas = db.GetCollection<Factura>(settings.Collection);
        }
        public List<Factura> Get() {
            return _Facturas.Find(d => true).ToList();
        }


        public Factura Create(Factura factura) {
            _Facturas.InsertOne(factura);
            return factura;
        }

        public void Update(string codigoFactura, Factura factura) {
            _Facturas.ReplaceOne(factura => factura.codigoFactura == codigoFactura, factura);
        }

        public void Delete(string codigoFactura)
        {
            _Facturas.DeleteOne(d => d.codigoFactura == codigoFactura);
        }


        public void SendEmail(string codigoFactura)
        {

            Factura factura = Get().Find(d => d.codigoFactura == codigoFactura);

            

            string estadoFactura = factura.estado.ToString();
            string correoUsuario = factura.correo.ToString();
            string mensaje = "";

            if (estadoFactura == "primerrecordatorio") {

                mensaje = "El estado de su factura es: " 
                    + estadoFactura 
                    + " por tal motivo a pasado a segundorecordatorio";
                factura.estado = "segundorecordatorio";
                _Facturas.ReplaceOne(factura => factura.codigoFactura == codigoFactura, factura);
            }
            else if(estadoFactura == "segundorecordatorio")
            {
                mensaje = "El estado de su factura es: "
            + estadoFactura
            + " por tal motivo lo vamos a desactivar";
            }


            var fromAddress = new MailAddress("rjuanjoser@gmail.com");
            var toAddress = new MailAddress(correoUsuario);
            string fromPassword = "pmbmunkwazpgysui";
            string subject = "Estado de su factura (MONO-LEGAL-PRUEBA)";
            string body = mensaje;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            };
            smtp.Send(message);
        }
    }
}
