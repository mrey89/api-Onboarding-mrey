using FluentValidation;

namespace apiOnBording.Application.UseCase.V1.PedidoOperation.Commands.Create
{
    public class CreatePedidoValidation : AbstractValidator<CreatePedidoCommand>
    {

        public CreatePedidoValidation()
        {
            RuleFor(x => x.CuentaCorriente)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("CuentaCorriente es requerido")
                .WithMessage("CuentaCorriente debe ser un número entero válido.");
            RuleFor(x => x.CodigoDeContratoInterno)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("CodigoDeContratoInterno es requerido")
                .WithMessage("CuentaCorriente debe ser un número entero válido.");
        }
    }
}
