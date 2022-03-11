using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCP.ExchangeRate.WebAPI.Models.Response
{
    public class AuthResponse
    {
        public string Message { get; set; }
        public string Fullname { get; set; }
        public string Token { get; set; }
    }
}
