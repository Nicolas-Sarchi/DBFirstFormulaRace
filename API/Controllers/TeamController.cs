using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Globalization;
using System.Linq;
using API.Helpers;
namespace API.Controllers
{
[ApiVersion("1.0")]
[ApiVersion("1.1")]

public class TeamController : BaseApiController
{
private IUnitOfWork _unitOfWork;
private readonly IMapper _mapper;
 public TeamController(IUnitOfWork UnitOfWork, IMapper Mapper)
{
 _unitOfWork = UnitOfWork;
 _mapper = Mapper;
}
[ApiVersion("1.1")]
[HttpGet]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<IEnumerable<TeamDto>>> Get()
{
    var Team = await _unitOfWork.Teams.GetAllAsync();
    return _mapper.Map<List<TeamDto>>(Team);
}

[HttpGet("{id}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<TeamDto>> Get(int id)
{
    var Team = await _unitOfWork.Teams.GetByIdAsync(id);
    return _mapper.Map<TeamDto>(Team);
}
[ApiVersion("1.0")]
[HttpGet]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<Pager<TeamDto>>> Get([FromQuery]Params TeamParams)
{
var Team = await _unitOfWork.Teams.GetAllAsync(TeamParams.PageIndex,TeamParams.PageSize, TeamParams.Search, "Name" );
var listaTeamsDto= _mapper.Map<List<TeamDto>>(Team.registros);
return new Pager<TeamDto>(listaTeamsDto, Team.totalRegistros,TeamParams.PageIndex,TeamParams.PageSize,TeamParams.Search);
}

[HttpPost]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<Team>> Post(TeamDto TeamDto)
{
    var Team = _mapper.Map<Team>(TeamDto);
    _unitOfWork.Teams.Add(Team);
    await _unitOfWork.SaveAsync();

    if (Team == null)
    {
        return BadRequest();
    }
    TeamDto.Id = Team.Id;
    return CreatedAtAction(nameof(Post), new { id = TeamDto.Id }, Team);
}

[HttpPut("{id}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<TeamDto>> Put(int id, [FromBody]TeamDto TeamDto)
{
    if (TeamDto == null)
    {
        return NotFound();
    }
    var Team = _mapper.Map<Team>(TeamDto);
    _unitOfWork.Teams.Update(Team);
    await _unitOfWork.SaveAsync();
    return TeamDto;
}

[HttpDelete("{id}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<TeamDto>> Delete(int id)
{
    var Team = await _unitOfWork.Teams.GetByIdAsync(id);
    if (Team == null)
    {
        return NotFound();
    }
    _unitOfWork.Teams.Remove(Team);
    await _unitOfWork.SaveAsync();
    return NoContent();
}
}
}