using Contexts.Users.Application;
using Contexts.Users.Application.Command;
using Contexts.Users.Application.DTOs;
using Contexts.Users.Application.Queries;
using Contexts.Users.Domain;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userContext;
    private readonly IMediator _mediator;

    public UsersController(IUserRepository userContext, IMediator mediator)
    {
        _userContext = userContext;
        _mediator = mediator;
    }

    /// <summary>
    /// Obtiene la información de todos los usuarios 
    /// </summary>
    /// <response code="200">Lista de usuarios</response>
    [HttpGet]
    public async Task<ActionResult<List<User>>> Get() => Ok(await _mediator.Send(new GetUsersQuery()));
    
    /// <summary>
    /// Obtiene la información de un usuario a partir de us Id
    /// </summary>
    /// <param name="id" example="3fa85f64-5717-4562-b3fc-2c963f66afa6">El id del usuario en formato uui</param>
    /// <response code="200">Usuario encontrado</response>
    /// <response code="404">Usuario no encontrado</response>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult<User>> Get(string id)
    {
        var item = await _mediator.Send(new GetUserQuery(){ Id = id});

        if (item == null)
            return NotFound();

        return Ok(item);
    }
    
    /// <summary>
    /// Crea un nuevo usuario
    /// </summary>
    /// <remarks>
    /// Los datos del nuevo usuario se envían en el cuerpo de la solicitud
    /// </remarks>
    /// <param name="item"></param>
    /// <returns>El nuevo usuario creado</returns>
    /// <response code="201">Usuario creado</response>
    /// <response code="400">Error en la solicitud</response>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<User>> Post([FromBody] CreateUserCommand createUserCommand)
    {
        if (createUserCommand.Name.Length == 0 || createUserCommand.Email.Length == 0)
            return BadRequest();
        
        var newUser = await _mediator.Send(createUserCommand);
        if (newUser == null)
            return BadRequest();

        return CreatedAtAction(
            nameof(Get),
            new { id = newUser.Id },
            newUser);
    }
    
    /// <summary>
    /// Actualiza un usuario a partir de su Id
    /// </summary>
    /// <remarks>
    /// Los datos del usuario a actualizar se envían en el cuerpo de la solicitud
    /// </remarks>
    /// <param name="id" example="3fa85f64-5717-4562-b3fc-2c963f66afa6">El id del usuario en formato uui</param>    /// <returns>Codigo que indica el resultado de la acción</returns>
    /// <response code="204">Usuario actualizado</response>
    /// <response code="400">Error en la solicitud</response>
    /// <response code="404">Usuario no encontrado</response>
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> PutTodoItem(string id, UpdateUserCommand updateUserCommand)
    {
        if (id != updateUserCommand.Id.ToString())
            return BadRequest();
        
        if (await _mediator.Send(updateUserCommand))
            return NoContent();

        return NotFound();
    }

    /// <summary>
    /// Elimina un usuario a partir de su Id
    /// </summary>
    /// <param name="id" example="3fa85f64-5717-4562-b3fc-2c963f66afa6">El id del usuario en formato uui</param>
    /// <returns>Codigo que indica el resultado de la acción</returns>
    /// <response code="204">Usuario eliminado</response>
    /// <response code="404">Usuario no encontrado</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(string id)
    {
        if(await _mediator.Send(new DeleteUserCommand() { Id = id }))
            return NoContent();
        return NotFound();
    }
}