using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeadLine.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DiscussionController : ControllerBase
    { 


    }
}