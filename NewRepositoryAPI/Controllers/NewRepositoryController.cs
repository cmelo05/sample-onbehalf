using Microsoft.AspNetCore.Mvc;
using NewRepositoryAPI.Models;
using NewRepositoryAPI.Services;

namespace NewRepositoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewRepositoryController : ControllerBase
    {
        private readonly ILogger<NewRepositoryController> _logger;
        private IBackendService _backendService;
        public NewRepositoryController(ILogger<NewRepositoryController> logger, IBackendService backendService)
        {
            this._logger = logger;
            this._backendService = backendService;
        }

        [HttpPost]
        public async Task<Run> CreateRepository()
        {
            var authorizationHeader = this.HttpContext.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                this.HttpContext.Response.StatusCode = 401;
                return null;
            }

            var run = await this._backendService.CreateAsync(authorizationHeader);

            return run;
        }
    }
}
