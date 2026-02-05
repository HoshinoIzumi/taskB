using Microsoft.AspNetCore.Mvc;
using TaskB.API.Models;
using TaskB.API.Services;

namespace TaskB.API.Controllers;

[ApiController]
[Route("[controller]")] // Maps to /Tasks
public class TasksController(ITaskService taskService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskDto>>> GetAll([FromQuery(Name = "_sort")] string? sort, [FromQuery(Name = "_order")] string? order)
    {
        var tasks = await taskService.GetAllAsync(sort, order);
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<ActionResult<TaskDto>> Create([FromBody] CreateTaskDto createDto)
    {
        var createdTask = await taskService.CreateAsync(createDto);
        return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskDto>> GetById(string id)
    {
        var task = await taskService.GetByIdAsync(id);
        if (task == null) return NotFound();
        return Ok(task);
    }

    [HttpPatch("{id}")]
    public async Task<ActionResult<TaskDto>> Update(string id, [FromBody] UpdateTaskDto updateDto)
    {
        var updatedTask = await taskService.UpdateAsync(id, updateDto);
        if (updatedTask == null) return NotFound();
        return Ok(updatedTask);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var success = await taskService.DeleteAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}
