using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1
{

    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/products")]
    public class ProductController : ControllerBase
    {




        [HttpGet]
        public IActionResult Get()
        {
            return Ok("V1");
        }
    }
}
