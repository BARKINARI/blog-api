using System.ComponentModel.DataAnnotations;
using Blog.Api.Data;
using Blog.Api.DTOs;
using Blog.Api.Models;
using Blog.Api.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/auth").WithTags("Auth");

        // POST /api/auth/register
        group.MapPost("/register", async (
            RegisterRequest req,
            IValidator<RegisterRequest> validator,
            BlogDbContext db) =>
        {
            if (await db.Users.AnyAsync(u => u.Username == req.Username))
            {
                return Results.Conflict(new { message = "Bu kullanıcı adı zaten alınmış." });
            }

            var user = new User
            {
                Username = req.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(req.PasswordHash),
                Role = "User"
            };

            db.Users.Add(user);
            await db.SaveChangesAsync();

            return Results.Ok(new { message = "Kayıt başarılı." });
        });

        // POST /api/auth/login
    }
}
