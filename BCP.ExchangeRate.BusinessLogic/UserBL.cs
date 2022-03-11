using BCP.ExchangeRate.BusinessLogic.Interfaces;
using BCP.ExchangeRate.Domain;
using BCP.ExchangeRate.Repository;
using System.Threading.Tasks;

namespace BCP.ExchangeRate.BusinessLogic
{
    public class UserBL : IUserBL
    {
        public async Task<User> Authenticate(string username, string password)
        {
            User credentials = new User() {
                Username = "cvallejo",
                Password = "123qwe",
                FirstName = "Cesar",
                LastName = "Vallejo"                
            };

            if (username.Equals(credentials.Username) && password.Equals(credentials.Password))
                return credentials;

            return null;
        }
    }
}
