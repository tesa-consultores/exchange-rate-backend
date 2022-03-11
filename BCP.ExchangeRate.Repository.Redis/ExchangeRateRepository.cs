using BCP.ExchangeRate.Domain;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BCP.ExchangeRate.Repository.Redis
{
    public class ExchangeRateRepository : IExchangeRateRepository
    {

        public ExchangeRateRepository()
        {

        }

        public async Task<PostExchangeRateDto> Get(string originCurrency, string destinationCurrency, decimal amount)
        {
            try
            {
                PostExchangeRateDto entity = new PostExchangeRateDto();

                var redisDB = RedisDB.Connection.GetDatabase();
                var exchangeRate = await redisDB.StringGetAsync($"{originCurrency}-{destinationCurrency}");
                entity = JsonConvert.DeserializeObject<PostExchangeRateDto>(exchangeRate);

                return entity;
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
                var redisDB = RedisDB.Connection.GetDatabase();
                await redisDB.StringSetAsync($"{entity.OriginCurrency}-{entity.DestinationCurrency}", JsonConvert.SerializeObject(entity));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
