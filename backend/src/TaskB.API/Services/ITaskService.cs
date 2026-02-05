using TaskB.API.Models;

namespace TaskB.API.Services;

public interface ITaskService
{
    Task<IEnumerable<TaskDto>> GetAllAsync(string? sort, string? order);
    Task<TaskDto?> GetByIdAsync(string id);
    Task<TaskDto> CreateAsync(CreateTaskDto createDto);
    Task<TaskDto?> UpdateAsync(string id, UpdateTaskDto updateDto);
    Task<bool> DeleteAsync(string id);
}
