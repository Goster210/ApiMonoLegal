using ApiMonoLegal.Models;
using MongoDB.Driver;

namespace ApiMonoLegal.Services
{
    public class FacturaServices
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
    }
}
