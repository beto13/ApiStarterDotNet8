using Application.UseCases.Users.Commands.Create;
using Application.UseCases.Users.Commands.SoftDelete;
using Application.UseCases.Users.Queries.GetFilteredUsers;
using Application.UseCases.Users.Queries.GetUserById;
using Domain.Dtos.User;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto dto)
        {
            var response = await _mediator.Send(new CreateUserCommand(dto));
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetUserByIdQuery(id));

            return Ok(result);
        }

        [HttpGet("user-filter")]
        public async Task<IActionResult> GetUsersFilter([FromQuery] UserFilterDto userFilterDto, [FromQuery] int pageSize, [FromQuery] int pageNumber)
        {
            var result = await _mediator.Send(new GetFilteredUsersQuery(userFilterDto, pageNumber, pageSize));

            return Ok(result);
        }

        //[HttpGet("user-with-post-filter")]
        //public async Task<IActionResult> GetUsersWithPostFilter([FromQuery] UserFilterDto userFilterDto, [FromQuery] int pageSize, [FromQuery] int pageNumber)
        //{
        //    var result = await _mediator.Send(new GetFilteredUsersQuery(userFilterDto, pageSize, pageNumber));

        //    return Ok(result);
        //}

        [HttpDelete()]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            var result = await _mediator.Send(new SoftDeleteUserCommand(id));

            return Ok(result);
        }

        //[HttpGet("getfilterpost")]
        //public async Task<IActionResult> Getfilterpost([FromQuery] PostFilterDto postFilterDto)
        //{
        //    var result = await _mediator.Send(new GetFilteredPostsQuery(postFilterDto));

        //    return Ok(result);
        //}
    }
}
