using BCP.ExchangeRate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.ExchangeRate.BusinessLogic.Interfaces
{
    public interface IUserBL
    {
        Task<User> Authenticate(string username, string password);
    }
}
