using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Application.DTOs;
using TodoApi.Application.Interfaces;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly ITodoService _todoService;
    
    public TodosController(ITodoService todoService)
    {
        _todoService = todoService;
        
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result=await _todoService.GetAllTodosAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result= await _todoService.GetByIdAsync(id);
        if (result is null)
        {
            return NotFound(new {Message="Not found"});
        }
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTodoDto input)
    {
        try
        {
            await _todoService.CreateTodoAsync(input);
            return Ok(new { Message = "Successfully created" });

        }
        catch (ValidationException ex)
        {
            return BadRequest(new {Errors=ex.Message});
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "Server error: " + ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateTodoDto input)
    {
        try
        {
            await _todoService.UpdateTodoAsync(id, input);
            return NoContent();
        }
        catch (ValidationException ex)
        {
            return BadRequest(new { Errors = ex.Message });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _todoService.DeleteTodoAsync(id);
        return NoContent();
    }
    
}
