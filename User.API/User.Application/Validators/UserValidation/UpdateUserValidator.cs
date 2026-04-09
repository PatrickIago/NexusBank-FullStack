using FluentValidation;
using User.Application.Dtos.UserDtos;
using User.Domain.Enuns;
namespace User.Application.Validators.UserValidation;

public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
{
    public UpdateUserValidator()
    {
        RuleFor(u => u.Id)
            .NotEmpty()
            .WithMessage("O id não pode estar vazio");

        RuleFor(u => u.Nome)
            .NotEmpty()
            .WithMessage("O nome não pode estar vazio");

        RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage("O email não pode estar vazio")
            .EmailAddress();

        RuleFor(u => u.Celular)
            .NotEmpty()
            .WithMessage("O celular não pode estar vazio");

        RuleFor(x => x.Documento)
            .NotEmpty()
            .Must((usuario, documento) =>
                  usuario.TipoPessoa == TipoPessoa.PessoaFisica
                      ? DocumentoValidation.DocumentoValidoCpf(documento)
                      : DocumentoValidation.DocumentoValidoCnpj(documento))
            .WithMessage("Documento inválido para o tipo de pessoa informado.");

        RuleFor(x => x.TipoPessoa)
           .IsInEnum()
           .WithMessage("Tipo de pessoa inválido. Use PessoaFisica ou PessoaJuridica.");
    }
}