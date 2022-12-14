using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace Notes.WebApi.Controlleres
{
    [ApiController]
    [Route("api/[controller]/[action]]")]
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
        internal Guid UserId =>
            !User.Identity.IsAuthenticated ? Guid.Empty : Guid.Parse("9D5165A2-9B9F-4C36-8A66-E58680804DEB");
        //internal Guid UserId => 
        //    !User.Identity.IsAuthenticated ? Guid.Empty : Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
    }
}
