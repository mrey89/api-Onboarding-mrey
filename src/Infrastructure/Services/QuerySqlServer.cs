using Andreani.Arq.Cqrs.Interfaces;
using Andreani.Arq.Cqrs.Queries;
using apiOnBording.Application.Common.Interfaces;
using apiOnBording.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using apiOnBording.Infrastructure.Persistence;
using System;

namespace apiOnBording.Infrastructure.Services
{
    public class QuerySqlServer : ReadOnlyQuery, IQuerySqlServer
    {
        private readonly PedidosDbContext _context;
        public QuerySqlServer([FromKeyedServices("default")] IReadOnlyQueryConfiguration config,
            [FromKeyedServices("default")] PedidosDbContext context) : base(config)
        {
            _context = context;
        }

        public async Task<TransaccionPedido> GetPedidoByIdAsync(string id)
        {
            return await _context.Pedidos.FirstOrDefaultAsync(p => p.Id.ToString().ToLower() == id.ToLower());
        }

    }
}

