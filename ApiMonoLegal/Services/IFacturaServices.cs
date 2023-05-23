using ApiMonoLegal.Models;
using System.Net.Mail;
using System.Net;

namespace ApiMonoLegal.Services
{
    public interface IFacturaServices
    {
        //La interfaz recopila los metodos del servicio (Factura)

        //LISTA DE ELEMENTOS (facturas)
        public List<Factura> Get();
        //CREAR ELEMENTO (factura)
        public Factura Create(Factura factura);
        //ACTUALIZAR ELEMENTO (facturas)
        public void Update(string codigoFactura, Factura factura);
        //ELIMINAR ELEMENTO (facturas)

        public void Delete(string codigoFactura);
        //Envio de correo

        public void SendEmail(string codigoFactura);
        //busca una factura
        public Factura BuscarFactura(string codigoFactura);

        //cambia el estado de la factura segun el caso
        public Factura CambioEstadoFactura(String estadoActual, Factura factura);

    }
}
