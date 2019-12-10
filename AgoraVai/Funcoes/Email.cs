using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace AgoraVai.Funcoes
{
    public class Email
    {
        public static string EnviarEmail(string emailDestinatario, string assunto, string corpomsg)
        {
            try
            {
                //Cria o endereço de email do remetente
                MailAddress de = new MailAddress("fatecgtaads@gmail.com");
                //Cria o endereço de email do destinatário -->
                MailAddress para = new MailAddress(emailDestinatario);
                MailMessage mensagem = new MailMessage(de, para);
                mensagem.IsBodyHtml = true;
                //Assunto do email
                mensagem.Subject = assunto;
                //Conteúdo do email
                mensagem.Body = corpomsg;
                //Prioridade E-mail
                mensagem.Priority = MailPriority.Normal;

                //Cria o objeto que envia o e-mail
                SmtpClient cliente = new SmtpClient();
                //Envia o email
                cliente.Send(mensagem);
                return "success|E-mail enviado com sucesso";
            }
            catch { return "error|Erro ao enviar e-mail"; }
        }


    }
}