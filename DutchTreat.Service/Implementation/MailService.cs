using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DutchTreat.Service.Implementation
{
    public class MailService : IMailService
    {
        private readonly ILogger<MailService> _logger;
        public MailService(ILogger<MailService> logger)
        {
            _logger = logger;
        }
        public void SendMail(string to, string subject, string body){
            _logger.LogInformation($"To: {to} Subject:{subject} Body: {body}");
        }
    }
}
