using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class userController : StandardController
    {
        public userController(IMediator mediator) : base(mediator)
        {
        }
    }
}
