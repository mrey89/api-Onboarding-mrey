using Andreani.Arq.AMQStreams.Extensions;
using Andreani.Arq.Observability.Extensions;
using Andreani.Arq.WebHost.Extension;
using Andreani.Scheme.Onboarding;
using apiOnBording.Application.Boopstrap;
using apiOnBording.Application.UseCase.V1.PedidoOperation.Commands.Create;
using apiOnBording.Application.UseCase.V1.PedidoOperation.Queries.GetById;
using apiOnBording.Infrastructure.Boopstrap;
using apiOnBording.Infrastructure.Services;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

builder.ConfigureAndreaniWebHost(args);
builder.Services.AddScoped<IValidator<CreatePedidoCommand>, CreatePedidoValidation>();
builder.Services.AddScoped<IValidator<GetPedidoById>, GetPedidoByIdValidation>();
builder.Services.ConfigureAndreaniServices();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Host.AddObservability();
builder.Services.
    AddKafka(builder.Configuration).
    CreateOrUpdateTopic(3, "Onboarding-PedidoCreado-Mrey").
    ToProducer<Pedido>("Onboarding-PedidoCreado-Mrey").
    ToConsumer<EventSuscriber, Pedido>("Onboarding-PedidoAsignado-Mrey").
    Build();

var app = builder.Build();

app.UseObservability();
app.ConfigureAndreani();

app.Run();
