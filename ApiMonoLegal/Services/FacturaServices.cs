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
    }
}
