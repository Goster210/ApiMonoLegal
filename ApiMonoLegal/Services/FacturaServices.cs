using ApiMonoLegal.Models;
using MongoDB.Driver;


namespace ApiMonoLegal.Services
{
    public class FacturaServices:IFacturaServices
    {
        private IMongoCollection<Factura> _Facturas;
        private EmailService emailService;

        public FacturaServices(IClienteSettings settings) 
        {
            var clienteMongo = new MongoClient(settings.Server);
            //conexion con la base de datos
            var db = clienteMongo.GetDatabase(settings.Database);
            //conexion con la coleccion (facturas)
            _Facturas = db.GetCollection<Factura>(settings.Collection);

            emailService = new EmailService();
        }
        public List<Factura> Get() {
            return _Facturas.Find(d => true).ToList();
        }

        public Factura BuscarFactura(string codigoFactura)
        {
            Factura factura = Get().Find(d => d.codigoFactura == codigoFactura);
            return factura;
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
            Factura factura = this.BuscarFactura(codigoFactura);
            Factura nuevaFactura = emailService.SendEmail(codigoFactura, factura);
            _Facturas.ReplaceOne(factura => factura.codigoFactura == codigoFactura, nuevaFactura);
          
        }
    }
}
