using FluentValidation;
using User.Application.Dtos.TransferenciasDto;

namespace User.Application.Validators.TransacaoValidations;

public class TransferenciaValidation : AbstractValidator<TransferenciaRequestDto>
{
    public TransferenciaValidation()
    {
        RuleFor(t => t.UsuarioRemetenteId)
            .NotEmpty()
            .WithMessage("O id do remetente não pode ser vazio");

        RuleFor(t => t.UsuarioDestinatarioId)
            .NotEmpty()
            .WithMessage("O id do destinatario não pode ser vazio");

        RuleFor(x => x.Valor)
            .GreaterThan(0)
            .WithMessage("Valor deve ser maior que zero.");

        RuleFor(x => x)
            .Must(x => x.UsuarioRemetenteId != x.UsuarioDestinatarioId)
            .WithMessage("Não é permitido transferir para o mesmo usuário.");
    }
}
