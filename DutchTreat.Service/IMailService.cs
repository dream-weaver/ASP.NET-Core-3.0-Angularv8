using System;
using System.Collections.Generic;
using System.Text;

namespace DutchTreat.Service
{
    public interface IMailService
    {
        public void SendMail(string to, string subject, string body);
    }
}
