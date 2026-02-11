namespace Blog.Api.DTOs;

public record CreateCommentRequest(
    string Text,
    string Author
);

public record CommentResponse(
    int Id,
    string Text,
    string Author,
    DateTime CreatedAt
);
