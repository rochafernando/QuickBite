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
    [Route("api/v{version:apiVersion}/products-categories")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class ProductCategoryController : ControllerBase
    {
        private readonly ICommandHandler<CreateProductCategoryCommand, ProductCategoryResponse> _createProductCategoryHandler;
        private readonly ICommandHandler<UpdateProductCategoryCommand, ProductCategoryResponse> _updateProductCategoryHandler;
        private readonly ICommandHandler<DeleteProductCategoryCommand> _deleteProductCategoryHandler;
        private readonly IQueryHandler<IEnumerable<ProductCategoryResponse>> _getAllProductCategoryHandler;

        public ProductCategoryController(
            ICommandHandler<CreateProductCategoryCommand, ProductCategoryResponse> createProductCategoryHandler,
            ICommandHandler<UpdateProductCategoryCommand, ProductCategoryResponse> updateProductCategoryHandler,
            ICommandHandler<DeleteProductCategoryCommand> deleteProductCategoryHandler,
            IQueryHandler<IEnumerable<ProductCategoryResponse>> getAllProductCategoryHandler)
        {
            _createProductCategoryHandler = createProductCategoryHandler;
            _updateProductCategoryHandler = updateProductCategoryHandler;
            _deleteProductCategoryHandler = deleteProductCategoryHandler;
            _getAllProductCategoryHandler = getAllProductCategoryHandler;
        }


        /// <summary>
        /// Busca lista de categorias.
        /// </summary>
        /// <response code="200">Retorna lista de categorias</response>
        /// <response code="500">Interanl Server Error</response>
        [HttpGet]
        [Tags("Categoria do Produto")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(List<ProductCategoryResponse>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(ErrorResponse))]
        public async Task<IActionResult> GetCategoryAsync()
        {
            return Ok(await _getAllProductCategoryHandler.HandleAsync());
        }

        /// <summary>
        /// Cria uma categoria.
        /// </summary>
        /// <param name="createProductCategoryCommand"></param>
        /// <response code="201">Retorna categoria criada</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Interanl Server Error</response>
        [HttpPost]
        [Tags("Categoria do Produto")]
        [SwaggerResponse(StatusCodes.Status201Created, type: typeof(ProductCategoryResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(IEnumerable<ErrorResponse>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(ErrorResponse))]
        public async Task<IActionResult> PostCategoryAsync([FromBody] CreateProductCategoryCommand createProductCategoryCommand)
        {
            return Created(string.Empty, await _createProductCategoryHandler.HandleAsync(createProductCategoryCommand));
        }

        /// <summary>
        /// Altera uma categoria.
        /// </summary>
        /// <param name="updateProductCategoryCommand"></param>
        /// <response code="200">Retorna categoria alterada</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Interanl Server Error</response>
        [HttpPut]
        [Tags("Categoria do Produto")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(ProductCategoryResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(IEnumerable<ErrorResponse>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(ErrorResponse))]
        public async Task<IActionResult> PutCategoryAsync([FromBody] UpdateProductCategoryCommand updateProductCategoryCommand)
        {
            return Ok(await _updateProductCategoryHandler.HandleAsync(updateProductCategoryCommand));
        }

        /// <summary>
        /// Remove uma categoria.
        /// </summary>
        /// <param name="deleteCategoryCommand"></param>
        /// <response code="204">Success</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Interanl Server Error</response>
        [HttpDelete]
        [Route("{uid}")]
        [Tags("Categoria do Produto")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(IEnumerable<ErrorResponse>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(ErrorResponse))]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] DeleteProductCategoryCommand deleteCategoryCommand)
        {
            await _deleteProductCategoryHandler.HandleAsync(deleteCategoryCommand);

            return NoContent();
        }
    }
}
