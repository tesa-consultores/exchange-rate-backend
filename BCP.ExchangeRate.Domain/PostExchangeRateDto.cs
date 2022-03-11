using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCP.ExchangeRate.Domain
{
    public class PostExchangeRateDto
    {
        public string OriginCurrency { get; set; }
        public string DestinationCurrency { get; set; }
        public decimal ExchangeRate { get; set; }
    }
}
