using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class userController : ControllerBase
    {
        public userController(IMediator mediator) : base(mediator)
        {
        }
    }
}
