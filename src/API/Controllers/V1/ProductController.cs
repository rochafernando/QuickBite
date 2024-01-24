using Application.Commands.Product;
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

        public ProductController(
            ICommandHandler<CreateProductCommand, ProductResponse> createHandler)
        {
            _createHandler = createHandler;
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
        public async Task<IActionResult> PostCategoryAsync([FromBody] CreateProductCommand createProductCommand)
        {
            return Created(string.Empty, await _createHandler.HandleAsync(createProductCommand));
        }
    }
}
