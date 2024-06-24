using System.Threading.Tasks;
using Andreani.Arq.AMQStreams.Dtos;
using Andreani.Scheme.Onboarding;
using apiOnBording.Domain.Entities;

namespace apiOnBording.Application.Common.Interfaces
{
    public interface IKafkaService
    {
        Task<ResultPublisher<Pedido>> Publicar(TransaccionPedido pedido, string id);
    }
}

