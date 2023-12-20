using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using O9d.AspNet.FluentValidation;
using PMS.Data;
using PMS.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using PMS.Auth.Model;
using System.Text;
using PMS;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using PMS.Auth;

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithExposedHeaders("Pagination");
        });
});

builder.Services.AddDbContext<PMSDbContext>();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddTransient<JwtTokenService>();
builder.Services.AddScoped<AuthDbSeeder>();

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
app.AddAuthApi();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

using var scope = app.Services.CreateScope();
var dbSeeder = scope.ServiceProvider.GetRequiredService<AuthDbSeeder>();

await dbSeeder.SeedAsync();

app.Run();








