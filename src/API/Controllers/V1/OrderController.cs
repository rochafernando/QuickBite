using Application.Commands.Order;
using Application.Queries.Order;
using Application.Responses;
using Application.Responses.Order;
using Application.Responses.Product;
using Asp.Versioning;
using Domain.Interfaces.CQS;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/orders")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class OrderController : ControllerBase
    {
        private readonly ICommandHandler<CreateOrderCommand, OrderResponse> _createHandler;
        private readonly IQueryHandler<FindOrderByUidQuery, OrderResponse> _findByUidHandler;
        private readonly ICommandHandler<DeleteOrderCommand> _deleteHandler;
        private readonly ICommandHandler<UpdateOrderCommand, OrderResponse> _updateHandler;
        private readonly IQueryHandler<IEnumerable<OrderResponse>> _getAllHandler;

        public OrderController(
            ICommandHandler<CreateOrderCommand, OrderResponse> createHandler,
            IQueryHandler<FindOrderByUidQuery, OrderResponse> findByUidHandler,
            ICommandHandler<DeleteOrderCommand> deleteHandler,
            ICommandHandler<UpdateOrderCommand, OrderResponse> updateHandler,
            IQueryHandler<IEnumerable<OrderResponse>> getAllHandler)
        {
            _createHandler = createHandler;
            _findByUidHandler = findByUidHandler;
            _deleteHandler = deleteHandler;
            _updateHandler = updateHandler;
            _getAllHandler = getAllHandler;
        }

        /// <summary>
        /// Busca todos pedidos.
        /// </summary>
        /// <response code="200">Retorna lista de pedidos</response>
        /// <response code="500">Interanl Server Error</response>
        [HttpGet]
        [Tags("Pedido")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(IEnumerable<ProductResponse>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(ErrorResponse))]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _getAllHandler.HandleAsync());
        }

        /// <summary>
        /// Busca pedido pelo uid.
        /// </summary>
        /// <param name="findOrderByUidQuery"></param>
        /// <response code="200">Retorna pedido</response>
        /// <response code="204">No Content</response>
        /// <response code="500">Interanl Server Error</response>
        [HttpGet]
        [Route("{uid}")]
        [Tags("Pedido")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(OrderResponse))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(ErrorResponse))]
        public async Task<IActionResult> GetByUidAsync([FromRoute] FindOrderByUidQuery findOrderByUidQuery)
        {
            return Ok(await _findByUidHandler.HandleAsync(findOrderByUidQuery));
        }

        /// <summary>
        /// Cria um pedido.
        /// </summary>
        /// <param name="createOrderCommand"></param>
        /// <response code="201">Retorna pedido criado</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Interanl Server Error</response>
        [HttpPost]
        [Tags("Pedido")]
        [SwaggerResponse(StatusCodes.Status201Created, type: typeof(OrderResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(IEnumerable<ErrorResponse>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(ErrorResponse))]
        public async Task<IActionResult> PostAsync([FromBody] CreateOrderCommand createOrderCommand)
        {
            return Created(string.Empty, await _createHandler.HandleAsync(createOrderCommand));
        }

        /// <summary>
        /// Altera um pedido.
        /// </summary>
        /// <param name="updateOrderCommand"></param>
        /// <response code="200">Retorna pedido atualizado</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Interanl Server Error</response>
        [HttpPut]
        [Tags("Pedido")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(OrderResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(IEnumerable<ErrorResponse>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(ErrorResponse))]
        public async Task<IActionResult> PutAsync([FromBody] UpdateOrderCommand updateOrderCommand)
        {
            return Ok(await _updateHandler.HandleAsync(updateOrderCommand));
        }

        /// <summary>
        /// Remove um pedido.
        /// </summary>
        /// <param name="deleteOrderCommand"></param>
        /// <response code="204">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Interanl Server Error</response>
        [HttpDelete]
        [Route("{uid}")]
        [Tags("Pedido")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(IEnumerable<ErrorResponse>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(ErrorResponse))]
        public async Task<IActionResult> DeleteAsync([FromRoute] DeleteOrderCommand deleteOrderCommand)
        {
            await _deleteHandler.HandleAsync(deleteOrderCommand);
            return NoContent();
        }
    }
}
