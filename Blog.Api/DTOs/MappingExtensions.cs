using Blog.Api.Models;

namespace Blog.Api.DTOs;

public static class MappingExtensions
{
    public static PostResponse ToResponse(this Post post) => new(
        post.Id,
        post.Title,
        post.Content,
        post.Author,
        post.CreatedAt,
        post.UpdatedAt,
        post.Comments.Select(c => c.ToResponse()).ToList()
    );

    public static PostSummaryResponse ToSummary(this Post post) => new(
        post.Id,
        post.Title,
        post.Author,
        post.CreatedAt,
        post.Comments.Count      
    );

    public static CommentResponse ToResponse(this Comment comment) => new(
        comment.Id,
        comment.Text,
        comment.Author,
        comment.CreatedAt
    );
}
