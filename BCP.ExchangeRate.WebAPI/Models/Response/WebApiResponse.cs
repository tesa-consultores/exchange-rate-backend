using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BCP.ExchangeRate.WebAPI.Models.Response
{
    public class WebApiResponse<T>
    {
        public bool Success { get; set; }
        public Response<T> Response { get; set; }
        public List<Error> Errors { get; set; }
    }

    public class Response<T>
    {
        public List<T> Data { get; set; }
    }

    public class Error
    {
        public int Code { get; set; }
        public string Message { get; set; }
    }

    /*
    {
      "success": false,
      "response": {
        "data": []
      },
      "errors": [
        {
          "code": 500,
          "message": "server 500 Error"
        }
      ]
    } 
     */
}
