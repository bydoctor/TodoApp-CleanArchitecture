using TodoApi.Application.DTOs;

namespace TodoApi.Application.Interfaces;

public interface ITodoService
{
    Task<List<TodoDto>> GetAllTodosAsync();
    Task<TodoDto?> GetByIdAsync(int id);
    Task CreateTodoAsync(CreateTodoDto newDto);
    Task UpdateTodoAsync(int id, UpdateTodoDto input);
    Task DeleteTodoAsync(int id);
}