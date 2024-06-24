using Andreani.Arq.Pipeline.Clases;
using Andreani.Arq.WebHost.Controllers;
using Microsoft.AspNetCore.Mvc;
using apiOnBording.Domain.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using apiOnBording.Application.UseCase.V1.PedidoOperation.Queries.GetById;
using System;
using apiOnBording.Application.UseCase.V1.PedidoOperation.Commands.Create;


namespace apiOnBording.Controllers.V1;

[ApiController]
[Route("api/[controller]")]
public class PedidoController : ApiControllerBase
{
  /// <summary>
  /// Post Alta pedido Creado
  /// </summary>
  /// <remarks>Alta de pedido</remarks>
  /// <returns>UrlGet pedido</returns>
  [HttpPost]
  [ProducesResponseType(typeof(CreatePedidoResponse), StatusCodes.Status201Created)]
  [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
  public async Task<IActionResult> Post(CreatePedidoCommand body) => this.Result(await Mediator.Send(body));

  /// <summary>
  /// Get Consulta pedido
  /// </summary>
  /// <remarks>Consulta de pedidos</remarks>
  /// <returns>Entidad pedido</returns>
  [HttpGet("{id}")]
  [ProducesResponseType(typeof(TransaccionPedido), StatusCodes.Status200OK)]
  [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status404NotFound)]
  [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(string id) => this.Result(await Mediator.Send(new GetPedidoById() { Id = id}));
}


