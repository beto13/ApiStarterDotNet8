using Application.Dtos.Comments;
using Application.UseCases.Comments.Commands.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentController : ControllerBase
    {
        private readonly IMediator mediator;

        public CommentController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CommentCreateDto dto) 
        { 
            var response = await mediator.Send(new CreateCommentCommand(dto));
            return Ok(response);
        }
    }
}
