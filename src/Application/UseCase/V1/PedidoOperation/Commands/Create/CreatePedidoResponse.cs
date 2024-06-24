using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apiOnBording.Application.UseCase.V1.PedidoOperation.Commands.Create
{
    public record struct CreatePedidoResponse(string urlGet, string message) { }
}
