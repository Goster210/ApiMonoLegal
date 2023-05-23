using ApiMonoLegal.Models;

namespace ApiMonoLegal.Services
{
    public interface IEmailService
    {

        public void SendEmail(Factura factura);
        public String dataSendEmail(String correoUsuario, String mensaje);
    }
}
