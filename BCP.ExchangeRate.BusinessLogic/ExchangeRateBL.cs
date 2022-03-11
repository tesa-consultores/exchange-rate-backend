using BCP.ExchangeRate.BusinessLogic.Interface;
using BCP.ExchangeRate.Domain;
using BCP.ExchangeRate.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BCP.ExchangeRate.BusinessLogic
{
    public class ExchangeRateBL : IExchangeRateBL
    {
        private readonly IExchangeRateRepository _exchangeRateRepository;

        public ExchangeRateBL(IExchangeRateRepository exchangeRateRepository)
        {
            _exchangeRateRepository = exchangeRateRepository;
        }

        public async Task<GetExchangeRateDto> Get(string originCurrency, string destinationCurrency, decimal amount)
        {
            try
            {
                PostExchangeRateDto entity = await _exchangeRateRepository.Get(originCurrency, destinationCurrency, amount);

                GetExchangeRateDto response = new GetExchangeRateDto()
                {
                    OriginCurrency = entity.OriginCurrency,
                    DestinationCurrency = entity.DestinationCurrency,
                    Amount = amount,
                    ExchangeRate = entity.ExchangeRate,
                    AmountChanged = amount * entity.ExchangeRate
                };

                return response;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Insert(PostExchangeRateDto entity)
        {
            try
            {
                await _exchangeRateRepository.Insert(entity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
