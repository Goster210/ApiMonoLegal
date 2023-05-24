using ApiMonoLegal.Models;
using MongoDB.Driver;


namespace ApiMonoLegal.Services
{
    public class FacturaServices:IFacturaServices
    {
        private IMongoCollection<Factura> _Facturas;
        private IEmailService _emailService;
        //constructor
        public FacturaServices(IClienteSettings settings, IEmailService emailService) 
        {
            var clienteMongo = new MongoClient(settings.Server);
            //conexion con la base de datos
            var db = clienteMongo.GetDatabase(settings.Database);
            //conexion con la coleccion (facturas)
            _Facturas = db.GetCollection<Factura>(settings.Collection);
            //servicio de email
            _emailService = emailService;
        }

        // lista de todas las facturas
        public List<Factura> Get() {
            return _Facturas.Find(d => true).ToList();
        }
        // crear una factura
        public Factura Create(Factura factura) {
            _Facturas.InsertOne(factura);
            return factura;
        }
        // actualizar una factura
        public void Update(string codigoFactura, Factura factura) {
            _Facturas.ReplaceOne(factura => factura.codigoFactura == codigoFactura, factura);
        }
        // eliminar una factura
        public void Delete(string codigoFactura)
        {
            _Facturas.DeleteOne(d => d.codigoFactura == codigoFactura);
        }
        // buscar una factura
        public Factura BuscarFactura(string codigoFactura)
        {
            Factura factura = Get().Find(d => d.codigoFactura == codigoFactura);
            return factura;
        }

        //cambia el estado de la factura segun el caso
        public Factura CambioEstadoFactura(String estadoActual, Factura factura)
        {
            if (estadoActual == "primerrecordatorio")
            {
                factura.estado = "segundorecordatorio";

                return factura;
            }
            else if (estadoActual == "segundorecordatorio")
            {
                return factura;
            }
            return factura;
        }
        // envio de correo
        public void SendEmail(string codigoFactura)
        {
            //Busco la factura
            Factura factura = this.BuscarFactura(codigoFactura);
            //envio el email
            _emailService.SendEmail(factura);
            //cambio el estado de la factura
            Factura facturaNuevoEstado = this.CambioEstadoFactura(factura.estado, factura);
            //actualizo mi factura
            _Facturas.ReplaceOne(factura => factura.codigoFactura == codigoFactura, facturaNuevoEstado);
          
        }
    }
}
