using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Pocketses.Core.Attributes;
using Pocketses.Core.Services.Interfaces;

namespace Pocketses.Core.Services
{
    [TransientDependency]
    public class EmailService : IEmailService
    {
        private MailKitEmailSenderOptions _options;

        public EmailService(IOptions<MailKitEmailSenderOptions> options)
        {
            _options = options.Value;
        }


        public async Task SendMail(string receiver, string subject, string body)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_options.SenderEmail),
                Subject = subject,
                Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body }
            };

            if (!string.IsNullOrEmpty(_options.SenderName))
                email.Sender.Name = _options.SenderName;

            email.From.Add(email.Sender);
            email.To.Add(MailboxAddress.Parse(receiver));

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_options.HostAddress, _options.HostPort, _options.HostSecureSocketOptions);
            await smtp.AuthenticateAsync(_options.HostUserName, _options.HostPassword);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }

    public class MailKitEmailSenderOptions
    {
        public string HostAddress { get; set; }
        public int HostPort { get; set; }
        public string HostUserName { get; set; }
        public string HostPassword { get; set; }

        public SecureSocketOptions HostSecureSocketOptions { get; set; }

        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
    }
}
