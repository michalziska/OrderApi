using Microsoft.AspNetCore.Mvc;

namespace SystemOrder.Controllers
{
	[Route("api/[controller]")]
	[Produces("application/json")]
	[ApiController]
	public class BaseApiController : ControllerBase { }
}
