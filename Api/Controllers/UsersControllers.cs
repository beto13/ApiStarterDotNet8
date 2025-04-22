using Application.Dtos.User;
using Application.UseCases.Users.Commands.Create;
using Application.UseCases.Users.Commands.SoftDelete;
using Application.UseCases.Users.Queries.GetFilteredUsers;
using Application.UseCases.Users.Queries.GetFilteredUsersWithPost;
using Application.UseCases.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;
        public UserController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto dto)
        {
            var response = await mediator.Send(new CreateUserCommand(dto));
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await mediator.Send(new GetUserByIdQuery(id));
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("user-filter")]
        public async Task<IActionResult> GetUsersFilter([FromQuery] UserFilterDto userFilterDto, [FromQuery] int pageSize, [FromQuery] int pageNumber)
        {
            var response = await mediator.Send(new GetFilteredUsersQuery(userFilterDto, pageNumber, pageSize));

            return StatusCode((int)response.StatusCode, response);

        }

        [HttpGet("user-with-post-filter")]
        public async Task<IActionResult> GetUsersWithPostFilter([FromQuery] UserFilterDto userFilterDto, [FromQuery] int pageSize, [FromQuery] int pageNumber)
        {
            var response = await mediator.Send(new GetFilteredUsersWithPostsQuery(userFilterDto, pageNumber, pageSize));

            return StatusCode((int)response.StatusCode, response);
        }

        [HttpDelete()]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var response = await mediator.Send(new SoftDeleteUserCommand(id));

            return StatusCode((int)response.StatusCode, response);
        }
    }
}
