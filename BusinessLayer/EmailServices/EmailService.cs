using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using BusinessLayer.JWTAuthentication;
using CommonLayer.RequestModel;
using Microsoft.Extensions.Configuration;

namespace BusinessLayer.EmailServices
{
    public class EmailService
    {
        private readonly IConfiguration config;
        UserAuthenticationJWT jWT;
        public EmailService(IConfiguration config)
        {
            this.config = config;
            jWT = new UserAuthenticationJWT(config);
        }

        public void SendPasswordResetLinkEmail(ForgetPasswordModel resetLink)
        {
            try
            {
                string HtmlBody;
                using (StreamReader streamReader = new StreamReader(config["IssuerEmailDetail:HtmlBodyFile"], Encoding.UTF8))
                {
                    HtmlBody = streamReader.ReadToEnd();
                }
                var token = jWT.GenerateCustomerPasswordResetJWT(resetLink);
                HtmlBody = HtmlBody.Replace("JwtToken", token);
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(config["IssuerEmailDetail:Email"]);
                message.To.Add(new MailAddress(resetLink.Email));
                message.Subject = "Reset Password";
                message.IsBodyHtml = true;
                message.Body = HtmlBody;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; 
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(config["IssuerEmailDetail:Email"], config["IssuerEmailDetail:Password"]);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception) {
                throw;
            }
        }
    }
}
