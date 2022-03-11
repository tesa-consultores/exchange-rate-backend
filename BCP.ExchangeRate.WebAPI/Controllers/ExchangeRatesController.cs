using BCP.ExchangeRate.BusinessLogic.Interface;
using BCP.ExchangeRate.Common;
using BCP.ExchangeRate.Domain;
using BCP.ExchangeRate.WebAPI.Models.Response;
using BCP.ExchangeRate.WebAPI.Validators;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BCP.ExchangeRate.WebAPI.Controllers
{
    [Route("api/{v:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class ExchangeRatesController : ControllerBase
    {
        private readonly IExchangeRateBL _exchangeRateBL;

        public ExchangeRatesController(IExchangeRateBL exchangeRateBL)
        {
            _exchangeRateBL = exchangeRateBL;
        }

        // GET: api/<ExchangeRatesController>
        [HttpGet]
        public async Task<ActionResult<WebApiResponse<GetExchangeRateDto>>> Get(string originCurrency, string destinationCurrency, decimal amount)
        {
            WebApiResponse<GetExchangeRateDto> response = new WebApiResponse<GetExchangeRateDto>();

            try
            {
                if (string.IsNullOrEmpty(originCurrency) || string.IsNullOrEmpty(destinationCurrency) || amount == 0)
                {
                    response.Success = false;
                    response.Errors = new List<Error>();

                    if (string.IsNullOrEmpty(originCurrency))
                        response.Errors.Add(new Error()
                        {
                            Code = StatusCodes.Status400BadRequest,
                            Message = $"La moneda origen no puede tener valor nulo o vacío."
                        });

                    if (string.IsNullOrEmpty(destinationCurrency))
                        response.Errors.Add(new Error()
                        {
                            Code = StatusCodes.Status400BadRequest,
                            Message = $"La moneda destino no puede tener valor nulo o vacío."
                        });

                    if (amount == 0)
                        response.Errors.Add(new Error()
                        {
                            Code = StatusCodes.Status400BadRequest,
                            Message = $"El monto no puede tener valor 0."
                        });

                    return StatusCode(400, response);
                }

                var entity = await _exchangeRateBL.Get(originCurrency, destinationCurrency, amount);

                if (entity == null)
                {
                    response.Success = false;
                    response.Errors = new List<Error>();
                    response.Errors.Add(new Error()
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = $"No existe un tipo de cambio configurado para la moneda origen {originCurrency} y moneda destino {destinationCurrency}"
                    });

                    return StatusCode(404, response);
                }

                response.Success = true;
                response.Response = new Response<GetExchangeRateDto>();
                response.Response.Data = new List<GetExchangeRateDto>();
                response.Response.Data.Add(entity);

                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Errors = new List<Error>();
                response.Errors.Add(new Error()
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                });

                return StatusCode(500, response);
            }
        }


        // POST api/<ExchangeRatesController>
        [HttpPost]
        public async Task<ActionResult<WebApiResponse<PostExchangeRateDto>>> Post([FromBody] PostExchangeRateDto entity)
        {
            WebApiResponse<PostExchangeRateDto> response = new WebApiResponse<PostExchangeRateDto>();

            var validator = new PostExchangeRateDtoValidator();
            ValidationResult result = validator.Validate(entity);

            try
            {
                if (!result.IsValid)
                {
                    response.Success = false;
                    response.Errors = new List<Error>();

                    Error error = null;

                    foreach (var item in result.Errors)
                    {
                        error = new Error();

                        error.Code = StatusCodes.Status400BadRequest;
                        error.Message = string.Format("{0}: {1}", item.ErrorCode, item.ErrorMessage);

                        response.Errors.Add(error);
                    }

                    return StatusCode(400, response);
                }

                await _exchangeRateBL.Insert(entity);

                if (entity == null)
                {
                    response.Success = false;
                    response.Errors = new List<Error>();
                    response.Errors.Add(new Error()
                    {
                        Code = StatusCodes.Status500InternalServerError,
                        Message = Constantes.MensajeErrorSistema
                    });

                    return StatusCode(500, response);
                }

                response.Success = true;
                response.Response = new Response<PostExchangeRateDto>();
                response.Response.Data = new List<PostExchangeRateDto>();
                response.Response.Data.Add(entity);

                return StatusCode(201, response);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message, ex);

                response.Success = false;
                response.Errors = new List<Error>();
                response.Errors.Add(new Error()
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                });

                return StatusCode(500, response);
            }
        }
    }
}
