using Microsoft.EntityFrameworkCore;
using TodoApi.Application.Interfaces;
using TodoApi.Infrastructure.Data;
using TodoApi.Models;

namespace TodoApi.Infrastructure.Repositories;

public class TodoRepository : ITodoRepository
{
    private readonly AppDbContext _context;

    public TodoRepository(AppDbContext context)
    {
        _context = context;
    }

    public  async Task<List<TodoItem>> GetAllAsync()
    {
        return await _context.Todos.ToListAsync();
    }

    public async Task<TodoItem?> GetByIdAsync(int id)
    {
        return await _context.Todos.FindAsync(id);
    }

    public  async Task AddAsync(TodoItem todoItem)
    {
        await _context.Todos.AddAsync(todoItem);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TodoItem todoItem)
    {
        _context.Todos.Update(todoItem);
        await  _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await _context.Todos.Where(todo => todo.Id == id)
            .ExecuteDeleteAsync();
    }
}