using FluentValidation;
using TodoApi.Application.DTOs;
using TodoApi.Application.Interfaces;
using TodoApi.Models;

namespace TodoApi.Application.Services;

public class TodoService: ITodoService
{
    
    private readonly ITodoRepository _todoRepository;
    private readonly IValidator<CreateTodoDto> _createValidator;
    private readonly IValidator<UpdateTodoDto> _updateValidator;

    public TodoService(ITodoRepository todoRepository, IValidator<CreateTodoDto> createValidator, IValidator<UpdateTodoDto> updateValidator)
    {
        _todoRepository = todoRepository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<List<TodoDto>> GetAllTodosAsync()
    {
      var items=  await _todoRepository.GetAllAsync();
      
      return items.Select(item=> new TodoDto(item.Id, item.Title,item.IsDone,item.UpdatedAt)).ToList();
    }

    public async Task<TodoDto?> GetByIdAsync(int id)
    {
        var item= await _todoRepository.GetByIdAsync(id);

        return item is null ? null : new TodoDto(item.Id, item.Title, item.IsDone,item.UpdatedAt);
    }

    public async Task CreateTodoAsync(CreateTodoDto newDto)
    {
        
        var validationResult=await _createValidator.ValidateAsync(newDto);
        if (!validationResult.IsValid)
        {
            var errorMessages = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));
            throw new ValidationException(errorMessages);
        }

        var entity = new TodoItem()
        {
            Title = newDto.Title,
            IsDone = false

        };
        
        await  _todoRepository.AddAsync(entity);
    }

    public async Task UpdateTodoAsync(int id, UpdateTodoDto input)
    {
        var validationResult= await _updateValidator.ValidateAsync(input);
        if (!validationResult.IsValid)
        {
            var errorMessages = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));
            throw new ValidationException(errorMessages);
        }
        var existingItem=await _todoRepository.GetByIdAsync(id);
        
        if (existingItem is null)
            throw new ArgumentException($"Item with id {id} not found");
        
        
        existingItem.Title = input.Title;
        existingItem.IsDone = input.IsDone;
        existingItem.UpdatedAt = DateTime.Now;
        
        await _todoRepository.UpdateAsync(existingItem);
    }

    public async Task DeleteTodoAsync(int id)
    {
        await _todoRepository.DeleteAsync(id);
    }
}