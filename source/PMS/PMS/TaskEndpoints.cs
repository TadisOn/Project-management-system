using O9d.AspNet.FluentValidation;
using PMS.Data.DTOs;
using PMS.Data.Entities;
using PMS.Data;
using Microsoft.EntityFrameworkCore;
using PMS.Helpers;
using System.Text.Json;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.AspNetCore.Authorization;
using PMS.Auth.Model;
using System.Data;
using Microsoft.AspNetCore.Http;

namespace PMS
{
    public static class TaskEndpoints
    {

        public static void AddTaskEndpoints(RouteGroupBuilder tasksGroup)
        {
            tasksGroup.MapGet("tasks", [Authorize(Roles = PMSRoles.PMSUser)] async ([AsParameters] SearchParameters searchParams,int projectId, PMSDbContext dbContext, CancellationToken cancellationToken, LinkGenerator linkGenerator, HttpContext httpContext) => {
                var project = await dbContext.Projects.FirstOrDefaultAsync(t => t.Id == projectId);

                if (project == null) return Results.NotFound();

                var queryable = dbContext.Tasks.AsQueryable().OrderBy(o=> o.CreationDate);
                var pagedList = await PagedList<Data.Entities.Task>.CreateAsync(queryable, searchParams.PageNumber!.Value, searchParams.PageSize!.Value);

                var previousPageLink = pagedList.HasPrevious
                    ? linkGenerator.GetUriByName(httpContext,"GetTasks", new { pageNumber = searchParams.PageNumber - 1, pageSize = searchParams.PageSize })
                    : null;

                var nextPageLink = pagedList.HasNext
                    ? linkGenerator.GetUriByName(httpContext, "GetTasks", new { pageNumber = searchParams.PageNumber + 1, pageSize = searchParams.PageSize })
                    : null;

                var paginationMetadata = new PaginationMetadata(pagedList.TotalCount, pagedList.PageSize, pagedList.CurrentPage, pagedList.TotalPages, previousPageLink, nextPageLink);

                httpContext.Response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationMetadata));

                return Results.Ok((await dbContext.Tasks.ToListAsync(cancellationToken)).Select(o => new TaskDto(o.Id, o.Name, o.Description, o.CreationDate)));

            }).WithName("GetTasks");

            tasksGroup.MapGet("tasks/{taskId}", [Authorize(Roles = PMSRoles.PMSUser)] async (int taskId, int projectId, PMSDbContext dbContext) => {

                var project = await dbContext.Projects.FirstOrDefaultAsync(t => t.Id == projectId);
                if (project == null)
                    return Results.NotFound();

                var task = await dbContext.Tasks.FirstOrDefaultAsync(p => p.Id == taskId && p.Project.Id == projectId);
                if (task == null)
                    return Results.NotFound();

                return Results.Ok(new TaskDto(task.Id, task.Name, task.Description, task.CreationDate));
            }).WithName("GetTask");

            tasksGroup.MapPost("tasks", [Authorize(Roles = PMSRoles.PMSUser)] async (int projectId, [Validate] CreateTaskDto createTaskDto, PMSDbContext dbContext, HttpContext httpContext, LinkGenerator linkGenerator) => {

                var project = await dbContext.Projects.FirstOrDefaultAsync(t => t.Id == projectId);
                if (project == null)
                    return Results.NotFound();


                var task = new Data.Entities.Task()
                {
                    Name = createTaskDto.Name,
                    Description = createTaskDto.Description,
                    CreationDate = DateTime.UtcNow,
                    Project = project,
                    UserId = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub)
                };

                dbContext.Tasks.Add(task);
                await dbContext.SaveChangesAsync();

                var links = CreateLinks(task.Id, httpContext,linkGenerator);
                var taskDto = new TaskDto(task.Id, task.Name, task.Description, task.CreationDate);

                var resource = new ResourceDto<TaskDto>(taskDto, links.ToArray());

                return Results.Created($"/api/projects/{projectId}/tasks/{task.Id}", resource);
            }).WithName("CreateTask");

            tasksGroup.MapPut("tasks/{taskId}", [Authorize(Roles = PMSRoles.PMSUser)] async (int taskId, int projectId, [Validate] UpdateTaskDto updateTaskDto, PMSDbContext dbContext, HttpContext httpContext) => {

                var project = await dbContext.Projects.FirstOrDefaultAsync(t => t.Id == projectId);

                if (project == null)
                    return Results.NotFound();

                var task = await dbContext.Tasks.FirstOrDefaultAsync(p => p.Id == taskId && p.Project.Id == projectId);
                if (task == null)
                    return Results.NotFound();


                if (!httpContext.User.IsInRole(PMSRoles.Admin) && httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub) != project.UserId)
                {
                    return Results.Forbid();
                }

                task.Description = updateTaskDto.Description;


                dbContext.Update(task);
                await dbContext.SaveChangesAsync();

                return Results.Ok(new TaskDto(task.Id, task.Name, task.Description, task.CreationDate));
            }).WithName("EditTask");

            tasksGroup.MapDelete("tasks/{taskId}", [Authorize(Roles = PMSRoles.Admin)] async (int taskId, int projectId, PMSDbContext dbContext) => {

                var project = await dbContext.Projects.FirstOrDefaultAsync(t => t.Id == projectId);
                if (project == null)
                    return Results.NotFound();

                var task = await dbContext.Tasks.FirstOrDefaultAsync(p => p.Id == taskId && p.Project.Id == projectId);
                if (task == null)
                    return Results.NotFound();

                dbContext.Remove(task);
                await dbContext.SaveChangesAsync();

                return Results.NoContent();

            }).WithName("DeleteTask");
        }

        static IEnumerable<LinkDto> CreateLinks(int projectId, HttpContext httpContext, LinkGenerator linkGenerator)
        {
            yield return new LinkDto(linkGenerator.GetUriByName(httpContext, "GetTask", new { projectId }), "self", "GET");
            yield return new LinkDto(linkGenerator.GetUriByName(httpContext, "EditTask", new { projectId }), "edit", "PUT");
            yield return new LinkDto(linkGenerator.GetUriByName(httpContext, "DeleteTask", new { projectId }), "delete", "DELETE");
        }
    }
}
