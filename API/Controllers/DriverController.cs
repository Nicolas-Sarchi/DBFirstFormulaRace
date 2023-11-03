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

public class DriverController : BaseApiController
{
private IUnitOfWork _unitOfWork;
private readonly IMapper _mapper;
 public DriverController(IUnitOfWork UnitOfWork, IMapper Mapper)
{
 _unitOfWork = UnitOfWork;
 _mapper = Mapper;
}
[ApiVersion("1.1")]
[HttpGet]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<IEnumerable<DriverDto>>> Get()
{
    var Driver = await _unitOfWork.Drivers.GetAllAsync();
    return _mapper.Map<List<DriverDto>>(Driver);
}

[HttpGet("{id}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<DriverDto>> Get(int id)
{
    var Driver = await _unitOfWork.Drivers.GetByIdAsync(id);
    return _mapper.Map<DriverDto>(Driver);
}
[ApiVersion("1.0")]
[HttpGet]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<Pager<DriverDto>>> Get([FromQuery]Params DriverParams)
{
var Driver = await _unitOfWork.Drivers.GetAllAsync(DriverParams.PageIndex,DriverParams.PageSize, DriverParams.Search, "Name" );
var listaDriversDto= _mapper.Map<List<DriverDto>>(Driver.registros);
return new Pager<DriverDto>(listaDriversDto, Driver.totalRegistros,DriverParams.PageIndex,DriverParams.PageSize,DriverParams.Search);
}

[HttpPost]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<Driver>> Post(DriverDto DriverDto)
{
    var Driver = _mapper.Map<Driver>(DriverDto);
    _unitOfWork.Drivers.Add(Driver);
    await _unitOfWork.SaveAsync();

    if (Driver == null)
    {
        return BadRequest();
    }
    DriverDto.Id = Driver.Id;
    return CreatedAtAction(nameof(Post), new { id = DriverDto.Id }, Driver);
}

[HttpPut("{id}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<DriverDto>> Put(int id, [FromBody]DriverDto DriverDto)
{
    if (DriverDto == null)
    {
        return NotFound();
    }
    var Driver = _mapper.Map<Driver>(DriverDto);
    _unitOfWork.Drivers.Update(Driver);
    await _unitOfWork.SaveAsync();
    return DriverDto;
}

[HttpDelete("{id}")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
public async Task<ActionResult<DriverDto>> Delete(int id)
{
    var Driver = await _unitOfWork.Drivers.GetByIdAsync(id);
    if (Driver == null)
    {
        return NotFound();
    }
    _unitOfWork.Drivers.Remove(Driver);
    await _unitOfWork.SaveAsync();
    return NoContent();
}
}
}