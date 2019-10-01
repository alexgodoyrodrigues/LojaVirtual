using LojaVirtual.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace LojaVirtual.Libraries.Email
{
    public class GerenciarEmail
    {
        SmtpClient _smtp;
        IConfiguration _configuration;

        public GerenciarEmail(SmtpClient smtp, IConfiguration configuration)
        {
            _smtp = smtp;
            _configuration = configuration;
        }

        public void EnviarContatoPorEmail(Contato contato)
        {
            string corpoMsg = string.Format("<h2>Contato - Loja Virtual</h2>" +
                    "<b>Nome:</b> {0} <br/>" +
                    "<b>E-mail:</b> {1} <br/>" +
                    "<b>Texto:</b> {2} <br/>" +
                    "<br/> E-mail enviado automaticamente do site LojaVirtual",
                    contato.Nome, contato.Email, contato.Texto
                );

            MailMessage mensagem = new MailMessage();
            mensagem.IsBodyHtml = true;
            mensagem.From = new MailAddress(_configuration.GetValue<string>("Email:Username"));
            mensagem.To.Add(contato.Email);
            mensagem.Subject = "Contato - LojaVirtual - E-mail: " + contato.Email;
            mensagem.Body = corpoMsg;

            _smtp.Send(mensagem);
        }

        public void EnviaSenhaColaboradorPorEmail(Colaborador colaborador)
        {
            string corpoMsg = string.Format("<h2>Colaborador - Loja Virtual</h2>" +
                    "Sua senha é:" +
                    "<h3>{0}</h3>", colaborador.Senha
                );

            MailMessage mensagem = new MailMessage();
            mensagem.IsBodyHtml = true;
            mensagem.From = new MailAddress(_configuration.GetValue<string>("Email:Username"));
            mensagem.To.Add(colaborador.Email);
            mensagem.Subject = "Colaborador - LojaVirtual - Senha do colaborador: " + colaborador.Nome;
            mensagem.Body = corpoMsg;

            _smtp.Send(mensagem);
        }
    }
}
