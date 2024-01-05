using Application.Commands.Customer;
using Application.Responses;
using Application.Responses.Customer;
using Asp.Versioning;
using Domain.Interfaces.CQS;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/clients")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class CustomerController : ControllerBase
    {
        private readonly ICommandHandler<CreateCustomerCommand, CustomerResponse> _handler;

        public CustomerController(ICommandHandler<CreateCustomerCommand, CustomerResponse> handler)
        {
            _handler = handler;
        }


        /// <summary>
        /// Cria um cliente novo.
        /// </summary>
        /// <param name="createClientCommand"></param>
        /// <response code="201">Retorna cliente criado</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Interanl Server Error</response>
        [HttpPost]
        [Tags("Cliente")]
        [SwaggerResponse(StatusCodes.Status201Created, type: typeof(CustomerResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(IEnumerable<ErrorResponse>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(ErrorResponse))]
        public async Task<IActionResult> PostAsync([FromBody] CreateCustomerCommand createClientCommand)
        {
            return Created(string.Empty, await _handler.HandleAsync(createClientCommand));
        }

        /// <summary>
        /// Atualiza um cliente.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateClientCommand"></param>
        /// <response code="200">Retorna cliente atualizado</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Interanl Server Error</response>
        [HttpPut]
        [Route("{id}")]
        [Tags("Cliente")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(CustomerResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(IEnumerable<ErrorResponse>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(ErrorResponse))]
        public async Task<IActionResult> PutAsync([FromRoute]Guid id, [FromBody] UpdateCustomerCommand updateClientCommand)
        {
            return Ok("V1");
        }

        /// <summary>
        /// Exclui um cliente.
        /// </summary>
        /// <param name="deleteClientCommand"></param>
        /// <response code="204">No Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Interanl Server Error</response>
        [HttpDelete]
        [Route("{id}")]
        [Tags("Cliente")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(IEnumerable<ErrorResponse>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(ErrorResponse))]
        public async Task<IActionResult> DeleteAsync([FromRoute] DeleteCustomerCommand deleteClientCommand)
        {
            return Ok("V1");
        }

        /// <summary>
        /// Busca um cliente pelo Id.
        /// </summary>
        /// <param name="findClientByIdCommand"></param>
        /// <response code="200">Retorna o cliente pelo Id</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Interanl Server Error</response>
        [HttpGet]
        [Route("{id}")]
        [Tags("Cliente")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(CustomerResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(IEnumerable<ErrorResponse>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(ErrorResponse))]
        public async Task<IActionResult> GetByIdAsync([FromRoute] FindCustomerByIdCommand findClientByIdCommand)
        {
            return Ok("V1");
        }
    }
}
