using Microsoft.AspNetCore.Mvc;
using NewRepositoryAPI.Repositories;
using NewRepositoryAPI.Services;

namespace NewRepositoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewRepositoryController : ControllerBase
    {
        private readonly ILogger<NewRepositoryController> _logger;
        private IBackendService _backendService;
        private IRepository _repository;
        public NewRepositoryController(ILogger<NewRepositoryController> logger, IBackendService backendService, IRepository repository)
        {
            this._logger = logger;
            this._backendService = backendService;
            this._repository = repository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRepository(string repositoryName)
        {
            var authorizationHeader = this.HttpContext.Request.Headers["Authorization"];

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                this._logger.LogError("NewRepositoryController.CreateRepository: No authorization header");
                return Unauthorized();
            }

            if (string.IsNullOrEmpty(repositoryName))
            {
                this._logger.LogError("NewRepositoryController.CreateRepository: No {0}", nameof(repositoryName));
                this.HttpContext.Response.StatusCode = 400;
                return BadRequest();
            }

            var workflowRun = await this._backendService.CreateAsync(authorizationHeader, repositoryName);

            return Ok(workflowRun);
        }
    }
}
