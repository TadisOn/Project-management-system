using Microsoft.AspNetCore.Mvc.Routing;
using PMS.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PMSDbContext>();


var app = builder.Build();

/*
/api/v1/projects GET List 200
/api/v1/projects/{id} GET One 200
/api/v1/projects POST Create 201
/api/v1/projects/{id} PUT/PATCH Modify 200
/api/v1/projects/{id} DELETE Remove 200/204
*/

//37:19

var projectsGroup = app.MapGroup("/api");

projectsGroup.MapGet("projects", () => { 



});

projectsGroup.MapGet("projects/{projectId}", (int projectId) => {



});

projectsGroup.MapPost("projects", () => {



});

projectsGroup.MapPut("projects/{projectId}", (int projectId) => {



});

projectsGroup.MapDelete("projects/{projectId}", (int projectId) => {



});
app.Run();
