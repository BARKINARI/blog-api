using Blog.Api.DTOs;
using FluentValidation;

namespace Blog.Api.Validators;

public class CreateCommentValidator : AbstractValidator<CreateCommentRequest>
{
    public CreateCommentValidator()
    {
        RuleFor (x => x.Text)
            .NotEmpty().WithMessage("Yorum alanı boş olamaz.")
            .MaximumLength(1000).WithMessage("Yorum alanına maksimum 1000 karakter girilebilir.");
        RuleFor (x => x.Author)
            .NotEmpty().WithMessage("Yazar alanı boş olamaz.");
    }
}
