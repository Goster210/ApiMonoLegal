using ApiMonoLegal.Models;
using ApiMonoLegal.Repository;
using MongoDB.Driver;


namespace ApiMonoLegal.Services
{
    public class FacturaServices:IFacturaServices
    {

        private IEmailService _emailService;
        private IFacturaRepository _facturaRepository;
        //constructor
        public FacturaServices(IFacturaRepository facturaRepository, IEmailService emailService) 
        {
            _facturaRepository = facturaRepository;
            //servicio de email
            _emailService = emailService;
        }

        // lista de todas las facturas
        public List<Factura> Get() {
            List<Factura>  lista = _facturaRepository.ListaFacturas();
            return lista;
        }
        // crear una factura
        public Factura Create(Factura factura) {
            _facturaRepository.CrearFactura(factura);
            return factura;
        }
        // actualizar una factura
        public void Update(string codigoFactura, Factura factura) {
            _facturaRepository.ActiualizarFactura(codigoFactura, factura);
        }
        // eliminar una factura
        public void Delete(string codigoFactura)
        {
            _facturaRepository.EliminarFactura(codigoFactura);
        }
        // buscar una factura
        public Factura BuscarFactura(string codigoFactura)
        {
            Factura factura = _facturaRepository.BuscarFactura(codigoFactura);
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
            _facturaRepository.CambioEstado(factura, codigoFactura, facturaNuevoEstado);


        }
    }
}
