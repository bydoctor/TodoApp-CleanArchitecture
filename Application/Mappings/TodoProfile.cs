using AutoMapper;
using TodoApi.Application.DTOs;
using TodoApi.Models;

namespace TodoApi.Application.Mappings;

public class TodoProfile:Profile
{
    public TodoProfile()
    {
        CreateMap<TodoItem, TodoDto>().ReverseMap();
        CreateMap<CreateTodoDto, TodoItem>();
        CreateMap<UpdateTodoDto, TodoItem>();
    }
}