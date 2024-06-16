using Microsoft.AspNetCore.Mvc;
using PrimerApi.Dto;
using PrimerApi.Interfaces.Services;
using PrimerApi.Query;
using PrimerApi.Response;

namespace PrimerApi.Controllers;

[ApiController]
public class AvionController : ControllerBase
{
    private readonly IAvionService _avionService;
    public AvionController(IAvionService avionService)
    {
        _avionService = avionService;
    }

    [HttpPost("/crearAvion")]
    public Task<ApiResponse<AvionDto>> Post([FromBody] nuevoAvion avion)
    {
        return _avionService.PostAvion(avion);
    }

    [HttpGet("/getAll")]
    public Task<ApiResponse<List<AvionDto>>> GetAll() { 
        return _avionService.GetAll();
    }
    [HttpGet("/getById/{id}")]
    public Task<ApiResponse<AvionDto>> GetById(string id)
    {
        return _avionService.GetById(id);
    }

    [HttpPut("/update/{id}")]
    public Task<ApiResponse<AvionDto>> updateAvion(string id, [FromBody] nuevoAvion avion)
    {
        return _avionService.UpdateAvion(id, avion);
    }
}

