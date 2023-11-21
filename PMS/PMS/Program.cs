using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using O9d.AspNet.FluentValidation;
using PMS.Data;
using PMS.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PMS.Auth.Model;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using PMS;

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PMSDbContext>();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddIdentity<PMSRestUser, IdentityRole>()
    .AddEntityFrameworkStores<PMSDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters.ValidAudience = builder.Configuration["Jwt:ValidAudience"];
    options.TokenValidationParameters.ValidIssuer = builder.Configuration["Jwt:ValidIssuer"];
    options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]));
});


builder.Services.AddAuthorization();


var app = builder.Build();

//-----------------------------------------------------------------------------------------------------------
var projectsGroup = app.MapGroup("/api").WithValidationFilter();
ProjectEndpoints.AddProjectApi(projectsGroup);

var tasksGroup = app.MapGroup("/api/projects/{projectId}").WithValidationFilter();
TaskEndpoints.AddTaskEndpoints(tasksGroup);

var workersGroup = app.MapGroup("/api/projects/{projectId}/tasks/{taskId}").WithValidationFilter();
WorkerEndpoints.AddWorkerEndpoints(workersGroup);
//------------------------------------------------------------------------------------------------------------

app.UseAuthentication();
app.UseAuthorization();

app.Run();








