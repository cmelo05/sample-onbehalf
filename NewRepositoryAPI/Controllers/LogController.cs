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
        public async Task<IList<string>> GetLogs(int id)
        {
            if (id <= 0)
            {
                this._logger.LogError("LogController.Get: Invalid id - {id}", id);
                this.HttpContext.Response.StatusCode = 400;
                return null;
            }

            var run = await this._repository.GetRunAsync(id);

            return run.Logs;
        }

        [HttpPatch]
        public async Task<WorkflowRun> Append(int id, string log)
        {
            if (id <= 0)
            {
                this._logger.LogError("LogController.Get: Invalid id - {id}", id);
                this.HttpContext.Response.StatusCode = 400;
                return null;
            }

            if (string.IsNullOrEmpty(log))
            {
                this._logger.LogError("LogController.Get: No {0}", nameof(log));
                this.HttpContext.Response.StatusCode = 400;
                return null;
            }

            var runToUpdate = await this._repository.GetRunAsync(id);
            runToUpdate.Logs.Add(log);

            var updatedRun = await this._repository.UpdateRunAsync(runToUpdate);

            return updatedRun;
        }
    }
}
