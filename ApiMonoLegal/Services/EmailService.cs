﻿using ApiMonoLegal.Models;
using System.Net.Mail;
using System.Net;


namespace ApiMonoLegal.Services
{
    public class EmailService : IEmailService
    {

        //constructor
        public EmailService() { }
        //envia el correo electronica gracias a diversos metodos
        public void SendEmail(Factura factura)
        {

            string estadoFactura = factura.estado.ToString();
            string correoUsuario = factura.correo.ToString();
            string mensaje = "";

            
            if (estadoFactura == "primerrecordatorio")
            {
                mensaje = "El estado de su factura es: "
                    + estadoFactura
                    + " por tal motivo a pasado a segundorecordatorio";

                Console.WriteLine(dataSendEmail(correoUsuario, mensaje));

            }
            else if (estadoFactura == "segundorecordatorio")
            {
               
                mensaje = "El estado de su factura es: "
            + estadoFactura
            + " por tal motivo lo vamos a desactivar";

                Console.WriteLine(dataSendEmail(correoUsuario, mensaje));

             
            }

            

        }
         //envia el correo electronico al usuario junto con el mensaje
        public String dataSendEmail(String correoUsuario, String mensaje) {

            var fromAddress = new MailAddress("rjuanjoser@gmail.com");
            var toAddress = new MailAddress(correoUsuario);
            string fromPassword = "xzoebcfaniajcebp";
            string subject = "Estado de su factura (MONO-LEGAL-PRUEBA)";
            string body = mensaje;

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            };
            try
            {
                smtp.Send(message);
                return "Se envio el correo";
            }
            catch (Exception ex)
            {
                return "Error en el envio del Correo -> "+ ex;
            }
            

        }
   

    }
}
