using Andreani.Arq.Core.Interface;
using apiOnBording.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace apiOnBording.Application.Common.Interfaces;

public interface IQuerySqlServer : IReadOnlyQuery
{
    public Task<TransaccionPedido> GetPedidoByIdAsync(string Id);
}
