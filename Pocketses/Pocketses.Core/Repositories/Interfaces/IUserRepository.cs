using Pocketses.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pocketses.Core.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetOrCreateGoogleUser(string googleSubject);
    }
}
