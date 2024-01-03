using API.Controllers.V1;
using Application.Queries;
using Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Endpoints.V1
{
    public class GetProductByIdEndpoint : ProductController
    {
        /// <summary>
        /// Obter produto pelo ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Produto pelo ID</returns>
        /// <response code="200">Retorna produto pelo ID</response>
        [HttpGet]
        [Route("{id}")]
        [Tags("Produtos")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(ProductResponse))]
        public async Task<IActionResult> GetById([FromRoute] FindProductByIdQuery findProductByIdQuery)
        {
            return Ok("V1");
        }
    }
}
