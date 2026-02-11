using Blog.Api.DTOs;
using FluentValidation;

namespace Blog.Api.Validators;

public class LoginValidator : AbstractValidator<LoginRequest>
{
    public LoginValidator()
    {
        RuleFor (x => x.Username)
            .NotEmpty().WithMessage("Kullanıcı adı alanı boş olamaz.");
        RuleFor (x => x.PasswordHash)
            .NotEmpty().WithMessage("Şifre alanı boş olamaz.")
            .MinimumLength(6).WithMessage("Şifre minimum 6 karakterden oluşmalıdır.");
    }
}
