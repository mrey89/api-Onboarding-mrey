using Andreani.Arq.Pipeline.Clases;
using apiOnBording.Application.Common.Interfaces;
using apiOnBording.Domain.Common;
using apiOnBording.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace apiOnBording.Application.UseCase.V1.PedidoOperation.Commands.Create
{

    public class CreatePedidoCommand : IRequest<Response<CreatePedidoResponse>>
    {
        public required int CuentaCorriente { get; set; }

        public required int CodigoDeContratoInterno { get; set; }
    }

    public class CreatePedidoCommandHandler(ICommandSqlServer repository, IValidator<CreatePedidoCommand> validator, IKafkaService publicador, ILogger<CreatePedidoCommandHandler> logger) : IRequestHandler<CreatePedidoCommand, Response<CreatePedidoResponse>>
    {
        private Guid idGuid;

        public async Task<Response<CreatePedidoResponse>> Handle(CreatePedidoCommand request, CancellationToken cancellationToken)
        {

            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return new Response<CreatePedidoResponse>
                {
                    Content = new CreatePedidoResponse { },
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                };
            }

            idGuid = Guid.NewGuid();
            var entity = new TransaccionPedido
            {
                Id = idGuid,
                NumeroDePedido = null,
                CicloDelPedido = idGuid.ToString(),
                CuentaCorriente = request.CuentaCorriente,
                EstadoDelPedido = "CREADO",
                CodigoDeContratoInterno = request.CodigoDeContratoInterno,
                Cuando = DateTime.Now,
            };

            repository.Insert(entity);
            await repository.SaveChangeAsync();

            logger.LogDebug("Pedido con Id:" + entity.Id + "Insertado correctamente");

            var publishResult = await publicador.Publicar(entity, entity.Id.ToString());
            var response = new Response<CreatePedidoResponse>();
            if (publishResult.HasErrors)
            {
                response.AddNotification("500", nameof(TransaccionPedido), string.Format(ErrorMessage.INTERNAL_ERROR_RECORD, nameof(TransaccionPedido), idGuid.ToString()));
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return response;
            }

            return new Response<CreatePedidoResponse>
            {
                Content = new CreatePedidoResponse
                {
                    urlGet = "~/api/pedido/" + entity.Id.ToString(),
                    message = "Pedido Creado"
                },
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
    }
}
