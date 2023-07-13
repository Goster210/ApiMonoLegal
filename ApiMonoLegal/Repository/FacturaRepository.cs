using ApiMonoLegal.Models;
using ApiMonoLegal.Services;
using MongoDB.Driver;

namespace ApiMonoLegal.Repository
{
    public class FacturaRepository:IFacturaRepository
    {
        private IMongoCollection<Factura> _Facturas;

        //constructor
        public FacturaRepository(IClienteSettings settings)
        {
            var clienteMongo = new MongoClient(settings.Server);
            //conexion con la base de datos
            var db = clienteMongo.GetDatabase(settings.Database);
            //conexion con la coleccion (facturas)
            _Facturas = db.GetCollection<Factura>(settings.Collection);
        }

        // lista de todas las facturas
        public List<Factura> ListaFacturas()
        {
            return _Facturas.Find(d => true).ToList();
        }
        // crear una factura
        public Factura CrearFactura(Factura factura)
        {
            _Facturas.InsertOne(factura);
            return factura;
        }
        // actualizar una factura
        public void ActiualizarFactura(string codigoFactura, Factura factura)
        {
            _Facturas.ReplaceOne(factura => factura.codigoFactura == codigoFactura, factura);
        }
        // eliminar una factura
        public void EliminarFactura(string codigoFactura)
        {
            _Facturas.DeleteOne(d => d.codigoFactura == codigoFactura);
        }
        // buscar una factura
        public Factura BuscarFactura(string codigoFactura)
        {
            Factura factura = ListaFacturas().Find(d => d.codigoFactura == codigoFactura);
            return factura;
        }

        //cambia el estado de la factura segun el caso

        // envio de correo
        public void CambioEstado(Factura factura, string codigoFactura, Factura facturaNuevoEstado)
        {
            _Facturas.ReplaceOne(factura => factura.codigoFactura == codigoFactura, facturaNuevoEstado);
        }
    }
}
