using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ordering.application.Contracts.Infrastructure;
using ordering.application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ordering.infrastructure.Mail
{
   public class EmailService :IEmailService
    {

        public EmailSettings _emailSettings { get; }
        public ILogger<EmailService> _logger { get; }

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> SendEmail(Email email)
        {

           
            SmtpClient smtp = new SmtpClient(Constants.ServidorSMTP, Constants.PortaSMTP); //conectar com a classe constant eviando o servidor e a porta

            smtp.EnableSsl = true; //seguridade sim.

            smtp.UseDefaultCredentials = false; //informando que não vai usar o padrão de logar para enviar email


            smtp.Credentials = new NetworkCredential(Constants.Usuario, Constants.Senha);

            //passer o email e a senha para que seja feito o envio

            MailMessage message = new MailMessage(); //primeiro você cria um objeto que vai lidar com as mensagems


           

            message.From = new MailAddress(_emailSettings.FromAddress);
            message.To.Add(email.To);
            message.Subject = email.Subject;

            

            message.IsBodyHtml = true;
            message.Body = $"<p> {email.Body} <p />";


            // para enviar
            await smtp.SendMailAsync(message);
          

            _logger.LogInformation("Email Sent.");
            return true;


        }


    }
}
