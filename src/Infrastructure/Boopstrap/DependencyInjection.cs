using Andreani.Arq.AMQStreams.Extensions;
using Andreani.Arq.Cqrs.Extension;
using Andreani.Scheme.Onboarding;
using apiOnBording.Application.Common.Interfaces;
using apiOnBording.Infrastructure.Persistence;
using apiOnBording.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace apiOnBording.Infrastructure.Boopstrap;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCQRS(configuration)
        .Configure<PedidosDbContext>();

        services.AddScoped<ICommandSqlServer, CommandSqlServer>();
        services.AddScoped<IQuerySqlServer, QuerySqlServer>();
        services.AddScoped<IKafkaService, KafkaService>();

        return services;
    }
}
