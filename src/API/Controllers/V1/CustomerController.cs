using Application.Commands.Customer;
using Application.Queries.Customer;
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
        private readonly ICommandHandler<CreateCustomerCommand, CustomerResponse> _createHandler;
        private readonly ICommandHandler<UpdateCustomerCommand, CustomerResponse> _updateHandler;
        private readonly ICommandHandler<DeleteCustomerCommand> _deleteHandler;
        private readonly IQueryHandler<FindCustomerByUidQuery, CustomerResponse> _findByIdHandler;

        public CustomerController(
            ICommandHandler<CreateCustomerCommand, CustomerResponse> createHandler,
            ICommandHandler<UpdateCustomerCommand, CustomerResponse> updateHandler,
            ICommandHandler<DeleteCustomerCommand> deleteHandler,
            IQueryHandler<FindCustomerByUidQuery, CustomerResponse> findByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _deleteHandler = deleteHandler;
            _findByIdHandler = findByIdHandler;
        }


        /// <summary>
        /// Cria um cliente novo.
        /// </summary>
        /// <param name="createCustomerCommand"></param>
        /// <response code="201">Retorna cliente criado</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Interanl Server Error</response>
        [HttpPost]
        [Tags("Cliente")]
        [SwaggerResponse(StatusCodes.Status201Created, type: typeof(CustomerResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(IEnumerable<ErrorResponse>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(ErrorResponse))]
        public async Task<IActionResult> PostAsync([FromBody] CreateCustomerCommand createCustomerCommand)
        {
            return Created(string.Empty, await _createHandler.HandleAsync(createCustomerCommand));
        }

        /// <summary>
        /// Atualiza um cliente.
        /// </summary>
        /// <param name="updateClientCommand"></param>
        /// <response code="200">Retorna cliente atualizado</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Interanl Server Error</response>
        [HttpPut]
        [Tags("Cliente")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(CustomerResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(IEnumerable<ErrorResponse>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(ErrorResponse))]
        public async Task<IActionResult> PutAsync([FromBody] UpdateCustomerCommand updateClientCommand)
        {
            return Ok(await _updateHandler.HandleAsync(updateClientCommand));
        }

        /// <summary>
        /// Exclui um cliente.
        /// </summary>
        /// <param name="deleteClientCommand"></param>
        /// <response code="204">No Content</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Interanl Server Error</response>
        [HttpDelete]
        [Route("{uid}")]
        [Tags("Cliente")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(IEnumerable<ErrorResponse>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(ErrorResponse))]
        public async Task<IActionResult> DeleteAsync([FromRoute] DeleteCustomerCommand deleteClientCommand)
        {
            await _deleteHandler.HandleAsync(deleteClientCommand);
            return NoContent();
        }

        /// <summary>
        /// Busca um cliente pelo Uid.
        /// </summary>
        /// <param name="findClientByUidQuery"></param>
        /// <response code="200">Retorna o cliente pelo Id</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Interanl Server Error</response>
        [HttpGet]
        [Route("{uid}")]
        [Tags("Cliente")]
        [SwaggerResponse(StatusCodes.Status200OK, type: typeof(CustomerResponse))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, type: typeof(IEnumerable<ErrorResponse>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, type: typeof(ErrorResponse))]
        public async Task<IActionResult> GetByIdAsync([FromRoute] FindCustomerByUidQuery findClientByUidQuery)
        {
            return Ok(await _findByIdHandler.HandleAsync(findClientByUidQuery));
        }
    }
}
