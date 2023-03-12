using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Data;

namespace Core
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandler> _logger;

        public ExceptionHandler(ILogger<ExceptionHandler> logger, RequestDelegate next)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                await HandleDbExceptionAsync(context, new Exception("The record has been updated by someone else, please resubmit."));
            }
            catch (DBConcurrencyException ex)
            {
                await HandleDbExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleValidationExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError($"Validation exception: Message: {ex.Message}", ex);
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = StatusCodes.Status500InternalServerError;
            await response.WriteAsync(JsonConvert.SerializeObject(new
            {
                messages = ex.Message,
            }));
        }

        private async Task HandleDbExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError($"Db exception: Message: {ex.Message}", ex);
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = StatusCodes.Status500InternalServerError;
            await response.WriteAsync(JsonConvert.SerializeObject(new
            {
                messages = ex.Message,
            }));
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogCritical($"Exception: Message: {ex.Message}", ex);
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = StatusCodes.Status500InternalServerError;
            await response.WriteAsync(JsonConvert.SerializeObject(new
            {
                messages = ex.Message,
            }));
        }
    }
}
