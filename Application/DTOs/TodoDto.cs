namespace TodoApi.Application.DTOs;

public record TodoDto(int Id,string Title,bool IsDone, DateTime? UpdatedAt);

public record CreateTodoDto(string Title);

public record UpdateTodoDto( string Title,bool IsDone);