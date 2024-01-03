using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/products")]
    [Produces("application/json")]
    public class ProductController : ControllerBase
    {
        
    }
}
