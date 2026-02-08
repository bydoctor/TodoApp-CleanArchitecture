using AutoMapper;
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
    private readonly IMapper _mapper;

    public TodoService(ITodoRepository todoRepository, IValidator<CreateTodoDto> createValidator, IValidator<UpdateTodoDto> updateValidator, IMapper mapper)
    {
        _todoRepository = todoRepository;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
        _mapper = mapper;
    }

    public async Task<List<TodoDto>> GetAllTodosAsync()
    {
      var entity=  await _todoRepository.GetAllAsync();
      
      var itemList=_mapper.Map<List<TodoDto>>(entity);

      return itemList;
    }

    public async Task<TodoDto?> GetByIdAsync(int id)
    {
        var entity= await _todoRepository.GetByIdAsync(id);

        return entity is null ? null : _mapper.Map<TodoDto>(entity);
    }

    public async Task CreateTodoAsync(CreateTodoDto newDto)
    {
        
        var validationResult=await _createValidator.ValidateAsync(newDto);
        if (!validationResult.IsValid)
        {
            var errorMessages = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));
            throw new ValidationException(errorMessages);
        }

        var entity = _mapper.Map<TodoItem>(newDto);
        entity.IsDone=false;
        
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

        
        _mapper.Map(input, existingItem);
        existingItem.UpdatedAt = DateTime.Now;
        
        await _todoRepository.UpdateAsync(existingItem);
    }

    public async Task DeleteTodoAsync(int id)
    {
        await _todoRepository.DeleteAsync(id);
    }
}