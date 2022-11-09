using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocketses.Core.AppServices.Interfaces
{
    public interface IUserAppService
    {
        Task<string> SetUser(string token);
    }
}
