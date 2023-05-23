using ApiMonoLegal.Models;

namespace ApiMonoLegal.Services
{
    public interface IEmailService
    {

        public Factura SendEmail(string codigoFactura, Factura factura);
        public String dataSendEmail(String correoUsuario, String mensaje);
        public Factura cambioEstadoFactura(String estadoActual, Factura factura);


    }
}
