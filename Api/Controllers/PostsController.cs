using Application.Common;
using Application.Dtos.Posts;
using Application.UseCases.Posts.Commands.Create;
using Application.UseCases.Posts.Commands.SoftDelete;
using Application.UseCases.Posts.Commands.Update;
using Application.UseCases.Posts.Queries.GetFilteredPosts;
using Application.UseCases.Posts.Queries.GetPostById;
using Application.UseCases.Posts.Queries.GetPostsByUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostsController : ControllerBase
    {
        private readonly IMediator mediator;

        public PostsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            var response = await mediator.Send(new GetPostByIdQuery(id));
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PostsCreateDto dto)
        {
            var response = await mediator.Send(new CreatePostCommand(dto));

            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("get-filter-post")]
        public async Task<IActionResult> GetFilterPost([FromQuery] PostFilterDto postFilterDto, [FromQuery] int pageSize, [FromQuery] int pageNumber)
        {
            var response = await mediator.Send(new GetFilteredPostsQuery(postFilterDto, pageNumber, pageSize));

            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("get-post-by-user")]
        public async Task<IActionResult> GetPostByUser([FromQuery] Guid userId)
        {
            var response = await mediator.Send(new GetPostsByUserQuery(userId));
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpDelete]
        public async Task<IActionResult> SoftDelete([FromQuery] Guid id)
        {
            var response = await mediator.Send(new SoftDeletePostCommand(id));
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPatch]
        public async Task<IActionResult> Update([FromQuery] Guid id,[FromQuery] string title)
        {
            var response = await mediator.Send(new UpdatePostCommand(new PostDto { Id = id, Title = title }));
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
