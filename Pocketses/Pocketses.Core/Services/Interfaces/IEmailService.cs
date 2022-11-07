using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocketses.Core.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendMail(string receiver, string subject, string body);
    }
}
