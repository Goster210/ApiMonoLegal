using ApiMonoLegal.Models;

namespace ApiMonoLegal.Repository
{
    public interface IFacturaRepository
    {
        public List<Factura> ListaFacturas();
        //CREAR ELEMENTO (factura)
        public Factura CrearFactura(Factura factura);
        //ACTUALIZAR ELEMENTO (facturas)
        public void ActiualizarFactura(string codigoFactura, Factura factura);
        //ELIMINAR ELEMENTO (facturas)
        public void EliminarFactura(string codigoFactura);
        //Envio de correo
        public void CambioEstado(Factura factura, string codigoFactura, Factura facturaNuevoEstado);
        //busca una factura
        public Factura BuscarFactura(string codigoFactura);


    }
}
