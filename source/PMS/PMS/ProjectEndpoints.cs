using O9d.AspNet.FluentValidation;
using PMS.Data.Entities;
using PMS.Data;
using Microsoft.EntityFrameworkCore;
using PMS.Data.DTOs;
using PMS.Helpers;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using System.Text.Json;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.AspNetCore.Authorization;
using PMS.Auth.Model;

namespace PMS
{
    public static class ProjectEndpoints
    {
        public static void AddProjectApi(RouteGroupBuilder projectsGroup)
        {
            projectsGroup.MapGet("projects", [Authorize(Roles = PMSRoles.PMSUser)] async ([AsParameters] SearchParameters searchParams,PMSDbContext dbContext, CancellationToken cancellationToken, LinkGenerator linkGenerator, Microsoft.AspNetCore.Http.HttpContext httpContext) => {

                var queryable = dbContext.Projects.AsQueryable().OrderBy(o => o.CreationDate);
                var pagedList = await PagedList<Project>.CreateAsync(queryable, searchParams.PageNumber!.Value, searchParams.PageSize!.Value);

                var previousPageLink = pagedList.HasPrevious 
                    ? linkGenerator.GetUriByName(httpContext, "GetProjects", new { pageNumber = searchParams.PageNumber - 1, pageSize = searchParams.PageSize })
                        : null;

                var nextPageLink = pagedList.HasNext
                    ? linkGenerator.GetUriByName(httpContext,"GetProjects",new { pageNumber = searchParams.PageNumber +1, pageSize = searchParams.PageSize })
                        : null;

                var paginationMetadata = new PaginationMetadata(pagedList.TotalCount, pagedList.PageSize, pagedList.CurrentPage,pagedList.TotalPages, previousPageLink, nextPageLink);

                httpContext.Response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationMetadata));

                return pagedList.Select(project => new ProjectDto(project.Id,project.Name,project.Description,project.CreationDate));
            }).WithName("GetProjects");

            projectsGroup.MapGet("projects/{projectId}", [Authorize(Roles = PMSRoles.PMSUser)] async (int projectId, PMSDbContext dbContext) => {

                var project = await dbContext.Projects.FirstOrDefaultAsync(t => t.Id == projectId);

                if (project == null)
                    return Results.NotFound();

                return Results.Ok(new ProjectDto(project.Id, project.Name, project.Description, project.CreationDate));
            }).WithName("GetProject");

            projectsGroup.MapPost("projects", [Authorize(Roles = PMSRoles.Admin)] async ([Validate] CreateProjectDto createProjectDto, PMSDbContext dbContext, HttpContext httpContext, LinkGenerator linkGenerator) => {

                var project = new Project()
                {
                    Name = createProjectDto.Name,
                    Description = createProjectDto.Description,
                    CreationDate = DateTime.UtcNow,
                    UserId = httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub)
                };

                dbContext.Projects.Add(project);
                await dbContext.SaveChangesAsync();

                var links = CreateLinks(project.Id, httpContext, linkGenerator);
                var projectDto = new ProjectDto(project.Id, project.Name, project.Description, project.CreationDate);

                var resource = new ResourceDto<ProjectDto>(projectDto, links.ToArray());

                return Results.Created($"/api/projects/{project.Id}", resource);
            }).WithName("CreateProject");

            projectsGroup.MapPut("projects/{projectId}", [Authorize(Roles = PMSRoles.Admin)] async (int projectId, [Validate] UpdateProjectDto updateProjectDto, PMSDbContext dbContext, HttpContext httpContext) => {

                var project = await dbContext.Projects.FirstOrDefaultAsync(t => t.Id == projectId);

                if (project == null)
                    return Results.NotFound();

                if (!httpContext.User.IsInRole(PMSRoles.Admin) && httpContext.User.FindFirstValue(JwtRegisteredClaimNames.Sub) != project.UserId)
                {
                    return Results.Forbid();
                }

                project.Description = updateProjectDto.Description;

                dbContext.Update(project);
                await dbContext.SaveChangesAsync();

                return Results.Ok(new ProjectDto(project.Id, project.Name, project.Description, project.CreationDate));


            }).WithName("EditProject");

            projectsGroup.MapDelete("projects/{projectId}", [Authorize(Roles = PMSRoles.Admin)] async (int projectId, PMSDbContext dbContext) => {

                var project = await dbContext.Projects.FirstOrDefaultAsync(t => t.Id == projectId);

                if (project == null)
                    return Results.NotFound();

                dbContext.Remove(project);
                await dbContext.SaveChangesAsync();

                return Results.NoContent();

            }).WithName("DeleteProject");
        }
        static IEnumerable<LinkDto> CreateLinks(int projectId, HttpContext httpContext, LinkGenerator linkGenerator)
        {
            yield return new LinkDto(linkGenerator.GetUriByName(httpContext, "GetProject", new { projectId }), "self", "GET");
            yield return new LinkDto(linkGenerator.GetUriByName(httpContext, "EditProject", new { projectId }), "edit", "PUT");
            yield return new LinkDto(linkGenerator.GetUriByName(httpContext, "DeleteProject", new { projectId }), "delete", "DELETE");
        }

    }

  
}
