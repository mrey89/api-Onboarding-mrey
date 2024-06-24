using FluentValidation;

namespace apiOnBording.Application.UseCase.V1.PedidoOperation.Queries.GetById
{
    public class GetPedidoByIdValidation : AbstractValidator<GetPedidoById>
  {
    public GetPedidoByIdValidation()
    {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                .WithMessage("El Id no puede estar vacio");
    }
  }
}

