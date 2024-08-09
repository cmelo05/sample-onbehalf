using Microsoft.AspNetCore.Mvc;
using NewRepositoryAPI.Models;
using NewRepositoryAPI.Repositories;

namespace NewRepositoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly ILogger<StatusController> _logger;
        private IRepository _repository;

        public StatusController(ILogger<StatusController> logger, IRepository repository)
        {
            this._logger = logger;
            this._repository = repository;
        }

        [HttpGet]
        public async Task<string> GetStatus(int id)
        {
            if (id <= 0)
            {
                this._logger.LogError("StatusController.GetRun: Invalid id - {id}", id);
                this.HttpContext.Response.StatusCode = 400;
                return null;
            }

            var run = await this._repository.GetRunAsync(id);

            return run.Status;
        }

        [HttpPatch]
        public async Task<WorkflowRun> UpdateStatus(int id, string status)
        {
            if (id <= 0)
            {
                this._logger.LogError("StatusController.UpdateStatus: Invalid id - {id}", id);
                this.HttpContext.Response.StatusCode = 400;
                return null;
            }

            if (string.IsNullOrEmpty(status))
            {
                this._logger.LogError("StatusController.UpdateStatus: No {0}", nameof(status));
                this.HttpContext.Response.StatusCode = 400;
                return null;
            }

            var runToUpdate = await this._repository.GetRunAsync(id);
            runToUpdate.Status = status;

            var updatedRun = await this._repository.UpdateRunAsync(runToUpdate);

            return updatedRun;
        }
    }
}
