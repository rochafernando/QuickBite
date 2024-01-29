using Application.Commands.Product;
using Application.Queries.Product;
using Application.Responses;
using Application.Responses.Product;
using Asp.Versioning;
using Domain.Interfaces.CQS;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/products")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ProductController : ControllerBase
    {
        private readonly ICommandHandler<CreateProductCommand, ProductResponse> _createHandler;
        private readonly ICommandHandler<UpdateProductCommand, ProductResponse> _updateHandler;
        private readonly ICommandHandler<DeleteProductCommand> _deleteHandler;
        private readonly IQueryHandler<IEnumerable<ProductResponse>> _getAllHandler;
        private readonly IQueryHandler<FindProductByUidQuery, ProductResponse> _findByUidHandler;

        public ProductController(
            ICommandHandler<CreateProductCommand, ProductResponse> createHandler,
            ICommandHandler<UpdateProductCommand, ProductResponse> updateHandler,
            ICommandHandler<DeleteProductCommand> deleteHandler,
            IQueryHandler<IEnumerable<ProductResponse>> getAllHandler,
            IQueryHandler<FindProductByUidQuery, ProductResponse> findByUidHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _deleteHandler = deleteHandler;
            _getAllHandler = getAllHandler;
            _findByUidHandler = findByUidHandler;
        }

        /// <summary>
        /// Busca todos produtos.
        /// </summary>
        /// <response code="200">Retorna lista de produtos</response>
        /// <response code="500">Interanl Server Error</response>
        [HttpGet]
        [Tags("Produto")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(IEnumerable<ProductResponse>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(ErrorResponse))]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _getAllHandler.HandleAsync());
        }

        /// <summary>
        /// Busca produto pelo uid.
        /// </summary>
        /// <param name="findProductByUidQuery"></param>
        /// <response code="200">Retorna produto</response>
        /// <response code="204">No Content</response>
        /// <response code="500">Interanl Server Error</response>
        [HttpGet]
        [Route("{uid}")]
        [Tags("Produto")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(ProductResponse))]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(ErrorResponse))]
        public async Task<IActionResult> GetByUidAsync([FromRoute] FindProductByUidQuery findProductByUidQuery)
        {
            return Ok(await _findByUidHandler.HandleAsync(findProductByUidQuery));
        }

        /// <summary>
        /// Cria um produto.
        /// </summary>
        /// <param name="createProductCommand"></param>
        /// <response code="201">Retorna produto criado</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Interanl Server Error</response>
        [HttpPost]
        [Tags("Produto")]
        [SwaggerResponse(StatusCodes.Status201Created, type: typeof(ProductResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(IEnumerable<ErrorResponse>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(ErrorResponse))]
        public async Task<IActionResult> PostProductAsync([FromBody] CreateProductCommand createProductCommand)
        {
            return Created(string.Empty, await _createHandler.HandleAsync(createProductCommand));
        }

        /// <summary>
        /// Altera um produto.
        /// </summary>
        /// <param name="updateProductCommand"></param>
        /// <response code="200">Retorna produto atualizado</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Interanl Server Error</response>
        [HttpPut]
        [Tags("Produto")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(ProductResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(IEnumerable<ErrorResponse>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(ErrorResponse))]
        public async Task<IActionResult> PutProductAsync([FromBody] UpdateProductCommand updateProductCommand)
        {
            return Ok(await _updateHandler.HandleAsync(updateProductCommand));
        }

        /// <summary>
        /// Remove um produto.
        /// </summary>
        /// <param name="deleteProductCommand"></param>
        /// <response code="204">No Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Interanl Server Error</response>
        [HttpDelete]
        [Route("{uid}")]
        [Tags("Produto")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(IEnumerable<ErrorResponse>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(ErrorResponse))]
        public async Task<IActionResult> DeleteProductAsync([FromRoute]DeleteProductCommand deleteProductCommand)
        {
            await _deleteHandler.HandleAsync(deleteProductCommand);
            
            return NoContent();
        }
    }
}
