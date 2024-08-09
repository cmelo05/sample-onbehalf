using Microsoft.AspNetCore.Mvc;
using NewRepositoryAPI.Models;
using NewRepositoryAPI.Repositories;

namespace NewRepositoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogger<LogController> _logger;
        private IRepository _repository;

        public LogController(ILogger<LogController> logger, IRepository repository)
        {
            this._logger = logger;
            this._repository = repository;
        }

        [HttpGet]
        public async Task<Run> Get(int id)
        {
            if (id <= 0)
            {
                this._logger.LogError("LogController.Get: Invalid id - {id}", id);
                this.HttpContext.Response.StatusCode = 400;
                return null;
            }

            var run = await this._repository.GetRunAsync(id);

            return run;
        }
    }
}
