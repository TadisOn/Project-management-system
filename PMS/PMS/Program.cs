using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using O9d.AspNet.FluentValidation;
using PMS.Data;
using PMS.Data.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PMSDbContext>();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();


var app = builder.Build();

/*
/api/v1/projects GET List 200
/api/v1/projects/{id} GET One 200
/api/v1/projects POST Create 201
/api/v1/projects/{id} PUT/PATCH Modify 200
/api/v1/projects/{id} DELETE Remove 200/204
*/

//37:19

var projectsGroup = app.MapGroup("/api").WithValidationFilter();

projectsGroup.MapGet("projects", async (PMSDbContext dbContext, CancellationToken cancellationToken) => {

    return (await dbContext.Projects.ToListAsync(cancellationToken))
    .Select(o => new ProjectDto(o.Id,o.Name,o.Description,o.CreationDate));
});

projectsGroup.MapGet("projects/{projectId}", async (int projectId, PMSDbContext dbContext) => {

    var project = await dbContext.Projects.FirstOrDefaultAsync(t => t.Id == projectId);

    if (project == null)
        return Results.NotFound();

    return Results.Ok(new ProjectDto(project.Id, project.Name, project.Description, project.CreationDate));
});

projectsGroup.MapPost("projects", async ([Validate]CreateProjectDto createProjectDto, PMSDbContext dbContext) => {

    var project = new Project()
    {
        Name = createProjectDto.Name,
        Description = createProjectDto.Description,
        CreationDate = DateTime.UtcNow
    };

    dbContext.Projects.Add(project);
    await dbContext.SaveChangesAsync();

    return Results.Created($"/api/projects/{project.Id}", new ProjectDto(project.Id, project.Name, project.Description, project.CreationDate));
});

projectsGroup.MapPut("projects/{projectId}", async (int projectId,[Validate]UpdateProjectDto updateProjectDto,PMSDbContext dbContext) => {

    var project = await dbContext.Projects.FirstOrDefaultAsync(t => t.Id == projectId);

    if (project == null)
        return Results.NotFound();

    project.Description = updateProjectDto.Description;

    dbContext.Update(project);
    await dbContext.SaveChangesAsync();

    return Results.Ok(new ProjectDto(project.Id, project.Name, project.Description, project.CreationDate));


});

projectsGroup.MapDelete("projects/{projectId}", async (int projectId, PMSDbContext dbContext) => {

    var project = await dbContext.Projects.FirstOrDefaultAsync(t => t.Id == projectId);

    if (project == null)
        return Results.NotFound();

    dbContext.Remove(project);
    await dbContext.SaveChangesAsync();

    return Results.NoContent();

});

/*
/api/v1/projects/{id}/tasks GET List 200
/api/v1/projects/{id}/tasks/{id} GET One 200
/api/v1/projects{id}/tasks POST Create 201
/api/v1/projects/{id}/tasks/{id} PUT/PATCH Modify 200
/api/v1/projects/{id}/tasks/{id} DELETE Remove 200/204
*/

var tasksGroup = app.MapGroup("/api/projects/{projectId}").WithValidationFilter();

tasksGroup.MapGet("tasks", async (int projectId, PMSDbContext dbContext, CancellationToken cancellationToken) => {
    var project = await dbContext.Projects.FirstOrDefaultAsync(t => t.Id == projectId);

    if (project == null) return Results.NotFound();

    return Results.Ok((await dbContext.Tasks.ToListAsync(cancellationToken)).Select(o => new TaskDto(o.Id, o.Name, o.Description, o.CreationDate)));
   
});

tasksGroup.MapGet("tasks/{taskId}", async (int taskId, int projectId, PMSDbContext dbContext) => {

    var project = await dbContext.Projects.FirstOrDefaultAsync(t => t.Id == projectId);
    if (project == null)
        return Results.NotFound();

    var task = await dbContext.Tasks.FirstOrDefaultAsync(p => p.Id == taskId && p.Project.Id == projectId);
    if(task == null)
        return Results.NotFound();

    return Results.Ok(new TaskDto(task.Id, task.Name, task.Description, task.CreationDate));
});

tasksGroup.MapPost("tasks", async (int projectId,[Validate] CreateTaskDto createTaskDto, PMSDbContext dbContext) => {

    var project = await dbContext.Projects.FirstOrDefaultAsync(t => t.Id == projectId);
    if (project == null)
        return Results.NotFound();


    var task = new PMS.Data.Entities.Task()
    {
        Name = createTaskDto.Name,
        Description = createTaskDto.Description,
        CreationDate = DateTime.UtcNow,
        Project = project
    };

    dbContext.Tasks.Add(task);
    await dbContext.SaveChangesAsync();

    return Results.Created($"/api/projects/{projectId}/tasks/{task.Id}", new TaskDto(task.Id, task.Name, task.Description, task.CreationDate));
});

tasksGroup.MapPut("tasks/{taskId}", async (int taskId, int projectId, [Validate] UpdateTaskDto updateTaskDto, PMSDbContext dbContext) => {

    var project = await dbContext.Projects.FirstOrDefaultAsync(t => t.Id == projectId);

    if (project == null)
        return Results.NotFound();

    var task = await dbContext.Tasks.FirstOrDefaultAsync(p => p.Id == taskId && p.Project.Id == projectId);
    if (task == null)
        return Results.NotFound();

    task.Description = updateTaskDto.Description;

    dbContext.Update(task);
    await dbContext.SaveChangesAsync();

    return Results.Ok(new TaskDto(task.Id, task.Name, task.Description, task.CreationDate));
});

tasksGroup.MapDelete("tasks/{taskId}", async (int taskId,int projectId, PMSDbContext dbContext) => {

    var project = await dbContext.Projects.FirstOrDefaultAsync(t => t.Id == projectId);
    if (project == null)
        return Results.NotFound();

    var task = await dbContext.Tasks.FirstOrDefaultAsync(p => p.Id == taskId && p.Project.Id == projectId);
    if (task == null)
        return Results.NotFound();

    dbContext.Remove(task);
    await dbContext.SaveChangesAsync();

    return Results.NoContent();

});

/*
/api/v1/projects/{id}/tasks/{id}/workers GET List 200
/api/v1/projects/{id}/tasks/{id}/workers{id} GET One 200
/api/v1/projects{id}/tasks/{id}/workers POST Create 201
/api/v1/projects/{id}/tasks/{id}/workers/{id} PUT/PATCH Modify 200
/api/v1/projects/{id}/tasks/{id}/workers/{id} DELETE Remove 200/204
*/

var workersGroup = app.MapGroup("/api/projects/{projectId}/tasks/{taskId}").WithValidationFilter();

workersGroup.MapGet("workers", async (int taskId, int projectId, PMSDbContext dbContext, CancellationToken cancellationToken) => {

    var project = await dbContext.Projects.FirstOrDefaultAsync(t => t.Id == projectId);
    if (project == null) return Results.NotFound();

    var task = await dbContext.Tasks.FirstOrDefaultAsync(p => p.Id == taskId && p.Project.Id == projectId);
    if (task == null)
        return Results.NotFound();

    return Results.Ok((await dbContext.Workers.ToListAsync(cancellationToken)).Select(o => new WorkerDto(o.Id, o.FirstName, o.LastName, o.UserName)));

});

workersGroup.MapGet("workers/{workerId}", async (int workerId, int taskId, int projectId, PMSDbContext dbContext) => {

    var project = await dbContext.Projects.FirstOrDefaultAsync(t => t.Id == projectId);
    if (project == null)
        return Results.NotFound();

    var task = await dbContext.Tasks.FirstOrDefaultAsync(p => p.Id == taskId && p.Project.Id == projectId);
    if (task == null)
        return Results.NotFound();

    var worker = await dbContext.Workers.FirstOrDefaultAsync(p => p.Id == workerId && p.Task.Id == taskId);
    if (worker == null)
        return Results.NotFound();

    return Results.Ok(new WorkerDto(worker.Id, worker.FirstName, worker.LastName, worker.UserName));
});

workersGroup.MapPost("workers", async (int taskId, int projectId, [Validate] CreateWorkerDto createWorkerDto, PMSDbContext dbContext) => {

    var project = await dbContext.Projects.FirstOrDefaultAsync(t => t.Id == projectId);
    if (project == null)
        return Results.NotFound();

    var task = await dbContext.Tasks.FirstOrDefaultAsync(p => p.Id == taskId && p.Project.Id == projectId);
    if (task == null)
        return Results.NotFound();


    var worker = new Worker()
    {
        FirstName = createWorkerDto.FirstName,
        LastName = createWorkerDto.LastName,
        UserName = createWorkerDto.Username,
        Password = createWorkerDto.Password.GetHashCode().ToString(), //Later this will be changed.
        CreationDate = DateTime.UtcNow,
        Task = task

    };

    dbContext.Workers.Add(worker);
    await dbContext.SaveChangesAsync();

    return Results.Created($"/api/projects/{projectId}/tasks/{taskId}/workers/{worker.Id}", new WorkerDto(worker.Id, worker.FirstName, worker.LastName, worker.UserName ));
});

workersGroup.MapPut("workers/{workerId}", async (int workerId, int taskId, int projectId, [Validate] UpdateWorkerDto updateWorkerDto, PMSDbContext dbContext) => {

    var project = await dbContext.Projects.FirstOrDefaultAsync(t => t.Id == projectId);

    if (project == null)
        return Results.NotFound();

    var task = await dbContext.Tasks.FirstOrDefaultAsync(p => p.Id == taskId && p.Project.Id == projectId);
    if (task == null)
        return Results.NotFound();

    var worker = await dbContext.Workers.FirstOrDefaultAsync(p => p.Id == workerId && p.Task.Id == taskId);
    if (worker == null)
        return Results.NotFound();

    worker.UserName = updateWorkerDto.Username;
    worker.Password = updateWorkerDto.Password.GetHashCode().ToString(); //This will be changed later

    dbContext.Update(worker);
    await dbContext.SaveChangesAsync();

    return Results.Ok(new WorkerDto(worker.Id, worker.FirstName, worker.LastName, worker.UserName));
});

workersGroup.MapDelete("workers/{workerId}", async (int workerId,int taskId, int projectId, PMSDbContext dbContext) => {

    var project = await dbContext.Projects.FirstOrDefaultAsync(t => t.Id == projectId);
    if (project == null)
        return Results.NotFound();

    var task = await dbContext.Tasks.FirstOrDefaultAsync(p => p.Id == taskId && p.Project.Id == projectId);
    if (task == null)
        return Results.NotFound();

    var worker = await dbContext.Workers.FirstOrDefaultAsync(p => p.Id == workerId && p.Task.Id == taskId);
    if (worker == null)
        return Results.NotFound();

    dbContext.Remove(worker);
    await dbContext.SaveChangesAsync();

    return Results.NoContent();

});


app.Run();


public record CreateProjectDto(string Name, string Description);
public record UpdateProjectDto(string Description);


public record CreateTaskDto(string Name, string Description);
public record UpdateTaskDto(string Description);

public record CreateWorkerDto(string FirstName, string LastName, string Username, string Password);
public record UpdateWorkerDto(string Username, string Password);

public class CreateProjectDtoValidator : AbstractValidator<CreateProjectDto>
{ 
    public CreateProjectDtoValidator()
    {
        RuleFor(dto => dto.Name).NotEmpty().NotNull().Length(2, 100);
        RuleFor(dto => dto.Description).NotEmpty().NotNull().Length(10, 300);
    }
}

public class UpdateProjectDtoValidator : AbstractValidator<UpdateProjectDto>
{
    public UpdateProjectDtoValidator()
    {
        RuleFor(dto => dto.Description).NotEmpty().NotNull().Length(10, 300);
    }
}

public class CreateTaskDtoValidator : AbstractValidator<CreateTaskDto>
{
    public CreateTaskDtoValidator()
    {
        RuleFor(dto => dto.Name).NotEmpty().NotNull().Length(2, 100);
        RuleFor(dto => dto.Description).NotEmpty().NotNull().Length(10, 300);
    }
}

public class UpdateTaskDtoValidator : AbstractValidator<UpdateTaskDto>
{
    public UpdateTaskDtoValidator()
    {
        RuleFor(dto => dto.Description).NotEmpty().NotNull().Length(10, 300);
    }
}

public class CreateWorkerDtoValidator : AbstractValidator<CreateWorkerDto>
{
    public CreateWorkerDtoValidator()
    {
        RuleFor(dto => dto.FirstName).NotEmpty().NotNull();
        RuleFor(dto => dto.LastName).NotEmpty().NotNull();
        RuleFor(dto => dto.Username).NotEmpty().NotNull().Length(3,10);
        RuleFor(dto => dto.Password).NotEmpty().NotNull().Length(6, 15);
    }
}

public class UpdateWorkerDtoValidator : AbstractValidator<UpdateWorkerDto>
{
    public UpdateWorkerDtoValidator()
    {
        RuleFor(dto => dto.Username).NotEmpty().NotNull().Length(3, 10);
        RuleFor(dto => dto.Password).NotEmpty().NotNull().Length(6, 15);
    }
}
