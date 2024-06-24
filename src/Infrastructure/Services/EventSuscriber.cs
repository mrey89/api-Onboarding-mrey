using Andreani.Arq.AMQStreams.Class;
using Andreani.Arq.AMQStreams.Interface;
using Andreani.Scheme.Onboarding;
using apiOnBording.Application.Common.Interfaces;
using apiOnBording.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace apiOnBording.Infrastructure.Services
{
    public class EventSuscriber : ISubscriber
    {
        private readonly ILogger<EventSuscriber> _logger;
        private readonly IQuerySqlServer _querySqlServer;
        private readonly ICommandSqlServer _repository;

        public EventSuscriber(ICommandSqlServer repository, ILogger<EventSuscriber> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Consume(Pedido @event, ConsumerMetadata metadata)
        {

            _logger.LogInformation($"Evento con id :", @event.id, "Estado: ", @event.estadoDelPedido);

            try
            {
                var pedidoMapping = new TransaccionPedido
                {
                    Id = Guid.Parse(@event.id),
                    CicloDelPedido = @event.cicloDelPedido,
                    CodigoDeContratoInterno = Convert.ToInt16(@event.codigoDeContratoInterno),
                    Cuando = DateTime.Parse(@event.cuando),
                    CuentaCorriente = (int)@event.cuentaCorriente,
                    EstadoDelPedido = @event.estadoDelPedido,
                    NumeroDePedido = @event.numeroDePedido,
                };
                _repository.Update(pedidoMapping);
                await _repository.SaveChangeAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }

            _logger.LogInformation("Trasaccion actualizada: ", @event.id);
        }
    }
}