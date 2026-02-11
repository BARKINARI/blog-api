namespace Blog.Api.DTOs;

public record RegisterRequest(
    string Username,
    string PasswordHash
);

public record LoginRequest(
    string Username,
    string PasswordHash
);

public record TokenResponse(
    string Token,
    DateTime Expiration
);
