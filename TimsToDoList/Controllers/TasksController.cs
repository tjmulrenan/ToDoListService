using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimsToDoList.Services;

namespace TimsToDoList.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("ReactPolicy")]
    public class TasksController : ControllerBase
    {
        private readonly TaskService _taskService;
        public TasksController(TaskService taskService)
        {
            _taskService = taskService;
        }

        // GET api/tasks
        [HttpGet]
        public IEnumerable<Models.Task> Get()
        {
            return (IEnumerable<Models.Task>)_taskService.GetAll;
        }

        // GET api/tasks/1
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(_taskService.GetById(id));
        }

        // POST api/tasks
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Models.Task task)
        {
            return CreatedAtAction("Get", new { id = task.Id }, _taskService.Create(task));
        }

        // POST api/tasks/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Post (int id, [FromBody] Models.Task task)
        {
            _taskService.Update(id, task);
            return NoContent();
        }

        // DELETE api/tasks/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (int id)
        {
            _taskService.Delete(id);
            return NoContent();
        }
    }
}
