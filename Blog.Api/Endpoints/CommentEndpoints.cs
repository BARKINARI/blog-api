using Blog.Api.Data;
using Blog.Api.DTOs;
using Blog.Api.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Blog.Api.Endpoints;

public static class CommentEndpoints
{
    public static void MapCommentEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/posts/{postId:int}/comments").WithTags("Comments");

        // GET /api/posts/{postId:int}/comments
        group.MapGet("/", async (int postId, BlogDbContext db) =>
        {
            var postExists = await db.Posts.AnyAsync(p => p.Id == postId);
            if (!postExists)
            {
                return Results.NotFound(new { message = "Yazı bulunamadı." });
            }

            var comments = await db.Comments
                .Where(c => c.PostId == postId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return Results.Ok(comments.Select(c => c.ToResponse()));
        });

        // POST /api/posts/{postId:int}/comments
        group.MapPost("/", async (
            int postId,
            CreateCommentRequest req,
            IValidator<CreateCommentRequest> validator,
            BlogDbContext db) =>
        {
            var validation = await validator.ValidateAsync(req);
            if (!validation.IsValid)
            {
                return Results.BadRequest(validation.Errors.Select(e => e.ErrorMessage));
            }

            var postExists = await db.Posts.AnyAsync(p => p.Id == postId);
            if (!postExists)
            {
                return Results.NotFound(new { message = "Yazı bulunamadı." });
            }

            var comment = new Comment
            {
                Text = req.Text,
                Author = req.Author,
                PostId = postId,
                CreatedAt = DateTime.UtcNow
            };

            db.Comments.Add(comment);
            await db.SaveChangesAsync();

            return Results.Created($"/api/posts/{postId}/comments/{comment.Id}", comment.ToResponse());
        })
        .RequireAuthorization();

        // DELETE /api/posts/{postId:int}/comments/{commentId:int}
        group.MapDelete("/{commentId:int}", async (
            int postId, 
            int commentId,
            BlogDbContext db) =>
        {
            var comment = await db.Comments.FirstOrDefaultAsync(c => c.Id == commentId && c.PostId == postId);
            if (comment is null)
            {
                return Results.NotFound(new { message = "Yorum bulunamadı" });
            }

            db.Comments.Remove(comment);
            await db.SaveChangesAsync();

            return Results.Ok(new { message = "Yorum silindi." });
        })
        .RequireAuthorization(p => p.RequireRole("Admin"));
    }
}
