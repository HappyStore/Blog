using Microsoft.AspNetCore.Mvc;

namespace Blog.Web.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public abstract class ApiV1ControllerBase : ControllerBase
    {
    }
}