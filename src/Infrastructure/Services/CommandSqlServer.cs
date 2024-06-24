using Andreani.Arq.Cqrs.Command;
using Andreani.Arq.Cqrs.Interfaces;
using apiOnBording.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;


namespace apiOnBording.Infrastructure.Services
{
    public class CommandSqlServer([FromKeyedServices("default")] ITransactionalConfiguration config) : TransactionalRepository(config), ICommandSqlServer
    {
    }
}
