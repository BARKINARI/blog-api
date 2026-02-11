namespace Blog.Api.DTOs;

public record CreatePostRequest(
    string Title,
    string Content,
    string Author
);

public record UpdatePostRequest(
    string Title,
    string Content
);

public record PostResponse(
    int Id,
    string Title,
    string Content,
    string Author,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    List<CommentResponse> Comments
);

public record PostSummaryResponse(
    int Id,
    string Title,
    string Author,
    DateTime CreatedAt,
    int CommentCount
);

