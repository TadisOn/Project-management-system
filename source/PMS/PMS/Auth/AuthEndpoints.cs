﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PMS.Auth.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PMS.Auth
{
    public static class AuthEndpoints
    {
        public static void AddAuthApi(this WebApplication app)
        {
            app.MapPost("api/register", async (UserManager<PMSRestUser> userManager, RegisterUserDto registerUserDto) =>
            {
                var user = await userManager.FindByNameAsync(registerUserDto.Username);
                if (user != null)
                {
                    return Results.UnprocessableEntity("Username already taken");
                }
                var newUser = new PMSRestUser
                {
                    Email = registerUserDto.Email,
                    FirstName = registerUserDto.FirstName,
                    LastName = registerUserDto.LastName,
                    UserName = registerUserDto.Username
                };

                var createUserResult = await userManager.CreateAsync(newUser, registerUserDto.Password);

                if(!createUserResult.Succeeded)
                {
                    return Results.UnprocessableEntity();
                }



                await userManager.AddToRoleAsync(newUser, PMSRoles.PMSUser);

                return Results.Created("api/login", new UserDto(newUser.Id, newUser.UserName,newUser.FirstName,newUser.LastName, newUser.Email));
            });

            app.MapDelete("api/deleteUsers", async (UserManager<PMSRestUser> userManager) =>
            {
                await userManager.Users.Where(user => user.UserName != "admin").ExecuteDeleteAsync();
                return Results.Ok(userManager.Users);
            }); 

            app.MapGet("/api/getUsers", async (UserManager<PMSRestUser> userManager) =>
            {
                var users = userManager.Users.ToList();
                return Results.Ok(users.Where(user => user.UserName != "admin").Select(user => new UserDto(user.Id, user.UserName,user.FirstName,user.LastName, user.Email)));
            });

            app.MapPost("api/login", async (UserManager<PMSRestUser> userManager, LoginUserDto loginUserDto, JwtTokenService jwtTokenService) =>
            {
                var user = await userManager.FindByNameAsync(loginUserDto.Username);
                if (user == null)
                {
                    return Results.UnprocessableEntity("Username or password was incorrect.");
                }
              
                var isPasswordValid = await userManager.CheckPasswordAsync(user, loginUserDto.Password);

                if (!isPasswordValid)
                    return Results.UnprocessableEntity("Username or password was incorrect");

                user.ForceRelogin = false;
                await userManager.UpdateAsync(user);

                var roles = await userManager.GetRolesAsync(user);

                var accessToken = jwtTokenService.CreateAccessToken(user.UserName, user.Id, roles);
                var refreshToken = jwtTokenService.CreateRefreshToken(user.Id);

                return Results.Ok(new SuccessfulLoginDto(accessToken, refreshToken));
            });

            app.MapPost("api/accessToken", async (UserManager<PMSRestUser> userManager, RefreshAccessTokenDto refreshAccessToken, JwtTokenService jwtTokenService) =>
            {
                if(!jwtTokenService.TryParseRefreshToken(refreshAccessToken.RefreshToken, out var claims))
                {
                    return Results.UnprocessableEntity();
                }

                var userId = claims.FindFirstValue(JwtRegisteredClaimNames.Sub);

                var user = await userManager.FindByIdAsync(userId);

                if(user == null)
                {
                    return Results.UnprocessableEntity("Invalid token");
                }

                if(user.ForceRelogin)
                {
                    return Results.UnprocessableEntity();
                }

                var roles = await userManager.GetRolesAsync(user);

                var accessToken = jwtTokenService.CreateAccessToken(user.UserName, user.Id, roles);
                var refreshToken = jwtTokenService.CreateRefreshToken(user.Id);

                return Results.Ok(new SuccessfulLoginDto(accessToken,refreshToken));

            });
        }

    }


    public record UserDto(string UserId, string Username,string FirstName, string LastName, string Email);
    public record RegisterUserDto(string Username,string Email,string FirstName, string LastName, string Password);

    public record LoginUserDto(string Username, string Password);
    public record SuccessfulLoginDto(string AccessToken, string RefreshToken);
    public record RefreshAccessTokenDto(string RefreshToken);

}
