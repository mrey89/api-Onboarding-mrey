using Andreani.Arq.Pipeline.Clases;
using apiOnBording.Application.Common.Interfaces;
using apiOnBording.Domain.Common;
using apiOnBording.Domain.Dtos;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace apiOnBording.Application.UseCase.V1.PedidoOperation.Queries.GetById
{
    public class GetPedidoById : IRequest<Response<TransaccionPedido>>
    {
        public required string Id { get; set; }
    }

    public class GetPedidoByIdHandler(IQuerySqlServer query) : IRequestHandler<GetPedidoById, Response<TransaccionPedido>>
    {
        public async Task<Response<TransaccionPedido>> Handle(GetPedidoById request, CancellationToken cancellationToken)
        {
            var pedido = await query.GetPedidoByIdAsync(request.Id);

            var response = new Response<TransaccionPedido>();
            if (pedido == null)
            {
                response.AddNotification("404", nameof(request.Id), string.Format(ErrorMessage.NOT_FOUND_RECORD, nameof(TransaccionPedido), request.Id));
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }

            response.Content = new TransaccionPedido()
            {
                Id = pedido.Id,
                NumeroDePedido = pedido.NumeroDePedido,
                CicloDelPedido = pedido.CicloDelPedido,
                CodigoDeContratoInterno = pedido.CodigoDeContratoInterno,
                CuentaCorriente = pedido.CuentaCorriente,
                Cuando = pedido.Cuando,
                EstadoDelPedido = pedido.EstadoDelPedido
            };
            response.StatusCode = System.Net.HttpStatusCode.OK;
            return response;
        }
    }
}

