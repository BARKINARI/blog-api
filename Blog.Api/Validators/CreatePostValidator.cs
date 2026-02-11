using Blog.Api.DTOs;
using FluentValidation;

namespace Blog.Api.Validators;

public class CreatePostValidator : AbstractValidator<CreatePostRequest>
{
    public CreatePostValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık alanı boş olamaz.")
            .MaximumLength(200).WithMessage("Başlık alanına maksimum 200 karakter girilebilir.");
            
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("İçerik alanı boş olamaz.")
            .MinimumLength(10).WithMessage("Başlık alanına minimum 10 karakter girilebilir.");


        RuleFor(x => x.Author)
            .NotEmpty().WithMessage("Yazar alanı boş olmaz.");
    }
}
