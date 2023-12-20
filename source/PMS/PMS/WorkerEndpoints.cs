using O9d.AspNet.FluentValidation;
using PMS.Data.DTOs;
using PMS.Data.Entities;
using PMS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using PMS.Helpers;
using System.Text.Json;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.AspNetCore.Authorization;
using PMS.Auth.Model;
using System.Data;
using System.Net.Http;

namespace PMS
{
    public static class WorkerEndpoints
    {
        public static void AddWorkerEndpoints(RouteGroupBuilder workersGroup)
        {
            workersGroup.MapGet("workers", [Authorize(Roles = PMSRoles.PMSUser)] async ([AsParameters] SearchParameters searchParams,int taskId, int projectId, PMSDbContext dbContext, CancellationToken cancellationToken, LinkGenerator linkGenerator, HttpContext httpContext) => {

                var project = await dbContext.Projects.FirstOrDefaultAsync(t => t.Id == projectId);
                if (project == null) return Results.NotFound();

                var task = await dbContext.Tasks.FirstOrDefaultAsync(p => p.Id == taskId && p.Project.Id == projectId);
                if (task == null)
                    return Results.NotFound();


                var queryable = dbContext.Workers.Where(worker=>worker.Task.Id == task.Id).AsQueryable().OrderBy(o => o.CreationDate);
                var pagedList = await PagedList<Worker>.CreateAsync(queryable, searchParams.PageNumber!.Value, searchParams.PageSize!.Value);
                var previousPageLink = pagedList.HasPrevious
                ? linkGenerator.GetUriByName(httpContext, "GetWorkers", new { pageNumber = searchParams.PageNumber - 1, pageSize = searchParams.PageSize })
                : null;
                var nextPageLink = pagedList.HasNext
                ? linkGenerator.GetUriByName(httpContext, "GetWorkers", new { pageNumber = searchParams.PageNumber + 1, pageSize = searchParams.PageSize })
                : null;

                var paginationMetadata = new PaginationMetadata(pagedList.TotalCount, pagedList.PageSize, pagedList.CurrentPage, pagedList.TotalPages, previousPageLink, nextPageLink);

                httpContext.Response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationMetadata));

                return Results.Ok((pagedList.Select(o => new WorkerDto(o.Id, o.FirstName, o.LastName, o.UserName))));

            }).WithName("GetWorkers");

            workersGroup.MapGet("workers/{workerId}", [Authorize(Roles = PMSRoles.Admin)] async (int workerId, int taskId, int projectId, PMSDbContext dbContext) => {

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
            }).WithName("GetWorker");

            workersGroup.MapPost("workers", [Authorize(Roles = PMSRoles.Admin)] async (int taskId, int projectId, [Validate] CreateWorkerDto createWorkerDto, PMSDbContext dbContext,HttpContext httpContext, LinkGenerator linkGenerator) => {

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
                    CreationDate = DateTime.UtcNow,
                    Task = task,
                    UserId = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub)

                };

                dbContext.Workers.Add(worker);
                await dbContext.SaveChangesAsync();

                var links = CreateLinks(worker.Id, httpContext, linkGenerator);
                var workerDto = new WorkerDto(worker.Id, worker.FirstName, worker.LastName, worker.UserName);
                var resource = new ResourceDto<WorkerDto>(workerDto, links.ToArray());


                return Results.Created($"/api/projects/{projectId}/tasks/{taskId}/workers/{worker.Id}", resource);
            }).WithName("CreateWorker");

            workersGroup.MapPut("workers/{workerId}", [Authorize(Roles = PMSRoles.Admin)] async (int workerId, int taskId, int projectId, [Validate] UpdateWorkerDto updateWorkerDto, PMSDbContext dbContext, HttpContext httpContext) => {

                var project = await dbContext.Projects.FirstOrDefaultAsync(t => t.Id == projectId);

                if (project == null)
                    return Results.NotFound();

                var task = await dbContext.Tasks.FirstOrDefaultAsync(p => p.Id == taskId && p.Project.Id == projectId);
                if (task == null)
                    return Results.NotFound();

                var worker = await dbContext.Workers.FirstOrDefaultAsync(p => p.Id == workerId && p.Task.Id == taskId);
                if (worker == null)
                    return Results.NotFound();
                if (!httpContext.User.IsInRole(PMSRoles.Admin) && httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub) != project.User.Id)
                {
                    return Results.Forbid();
                }

                worker.UserName = updateWorkerDto.Username;
                worker.FirstName = updateWorkerDto.FirstName;
                worker.LastName = updateWorkerDto.LastName;

                dbContext.Update(worker);
                await dbContext.SaveChangesAsync();

                return Results.Ok(new WorkerDto(worker.Id, worker.FirstName, worker.LastName, worker.UserName));
            }).WithName("EditWorker");

            workersGroup.MapDelete("workers/{workerId}", [Authorize(Roles = PMSRoles.PMSUser)] async (int workerId, int taskId, int projectId, PMSDbContext dbContext) => {

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

            }).WithName("DeleteWorker");
        }

        static IEnumerable<LinkDto> CreateLinks(int projectId, HttpContext httpContext, LinkGenerator linkGenerator)
        {
            yield return new LinkDto(linkGenerator.GetUriByName(httpContext, "GetWorker", new { projectId }), "self", "GET");
            yield return new LinkDto(linkGenerator.GetUriByName(httpContext, "EditWorker", new { projectId }), "edit", "PUT");
            yield return new LinkDto(linkGenerator.GetUriByName(httpContext, "DeleteWorker", new { projectId }), "delete", "DELETE");
        }

    }
}
