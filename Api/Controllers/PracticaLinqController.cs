using Application.Dtos.Comments;
using Application.UseCases.Comments.Commands.Create;
using Application.UseCases.PracticaLinq.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/Practica")]
    public class PracticaLinqController : ControllerBase
    {
        private readonly IMediator mediator;

        public PracticaLinqController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create()
        {
            var response = await mediator.Send(new PracticaLinqQuery());
            return Ok(response);
        }
    }
}
