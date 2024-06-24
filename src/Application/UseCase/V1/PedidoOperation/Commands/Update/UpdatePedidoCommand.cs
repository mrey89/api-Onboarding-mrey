using Andreani.Arq.Pipeline.Clases;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Threading;
using System;
using apiOnBording.Application.Common.Interfaces;
using apiOnBording.Domain.Common;
using apiOnBording.Domain.Dtos;

namespace onboarding_net.Application.UseCase.V1.PersonOperation.Commands.Update
{
    public class UpdatePedidoCommand : IRequest<Response<TransaccionPedido>>
    {
        public string Id { get; set; }
        public int NumeroDePedido { get; set; }
        public string EstadoDelPedido { get; set; }
    }

    public class UpdatePedidoCommandHandler : IRequestHandler<UpdatePedidoCommand, Response<TransaccionPedido>>
    {
        private readonly IQuerySqlServer _query;
        private readonly ICommandSqlServer _repository;
        private readonly ILogger<UpdatePedidoCommandHandler> _logger;

        public UpdatePedidoCommandHandler(IQuerySqlServer query, ICommandSqlServer repository, ILogger<UpdatePedidoCommandHandler> logger)
        {
            _query = query;
            _repository = repository;
            _logger = logger;
        }

        public async Task<Response<TransaccionPedido>> Handle(UpdatePedidoCommand request, CancellationToken cancellationToken)
        {
            var entity = await _query.GetPedidoByIdAsync(request.Id);

            var response = new Response<TransaccionPedido>();

            if (entity is null)
            {
                response.AddNotification("#3123", nameof(request.Id), string.Format(ErrorMessage.NOT_FOUND_RECORD, "Pedido", request.Id));
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }

            entity.NumeroDePedido = request.NumeroDePedido;
            entity.EstadoDelPedido = request.EstadoDelPedido;

            _repository.Update(entity);
            await _repository.SaveChangeAsync();

            response.Content = new TransaccionPedido();
            response.StatusCode = System.Net.HttpStatusCode.OK;

            _logger.LogInformation("Se actualizó el pedido correctamente en SQL");

            return response;
        }
    }
}
