using BCP.ExchangeRate.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.ExchangeRate.BusinessLogic.Interface
{
    public interface IExchangeRateBL
    {
        Task<GetExchangeRateDto> Get(string originCurrency, string destinationCurrency, decimal amount);
        Task Insert(PostExchangeRateDto entity);
    }
}
