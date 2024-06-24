using Andreani.Arq.AMQStreams.Dtos;
using Andreani.Arq.AMQStreams.Interface;
using Andreani.Scheme.Onboarding;
using apiOnBording.Application.Common.Interfaces;
using apiOnBording.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace apiOnBording.Infrastructure.Services
{
    public class KafkaService(IPublisher publisher) : IKafkaService
    {
        public async Task<ResultPublisher<Pedido>> Publicar(TransaccionPedido pedido, string id)
        {
            var evento = new Pedido
            {
                id = pedido.Id.ToString(),
                cicloDelPedido = pedido.CicloDelPedido,
                codigoDeContratoInterno = Convert.ToInt64(pedido.CodigoDeContratoInterno),
                cuentaCorriente = Convert.ToInt64(pedido.CuentaCorriente),
                estadoDelPedido = "CREADO",
                numeroDePedido = 0,
                cuando = pedido.Cuando.ToString(),
            };
            return await publisher.To(evento, id);
        }

    }
}