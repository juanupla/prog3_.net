using AutoMapper;
using PrimerApi.Dto;
using PrimerApi.Interfaces;
using PrimerApi.Interfaces.Services;
using PrimerApi.Models;
using PrimerApi.Query;
using PrimerApi.Response;
using System.Net;

namespace PrimerApi.Services;

public class AvionService : IAvionService
{
    private readonly IAvionRepository _avionRepository;
    private readonly IMapper _mapper;
    public AvionService(IAvionRepository avionRepository, IMapper mapper)
    {
        _avionRepository = avionRepository;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<AvionDto>>> GetAll()
    {
        var response = new ApiResponse<List<AvionDto>>();
        var aviones = await _avionRepository.GetAll();

        response.Data = _mapper.Map<List<AvionDto>>(aviones);
        return response;



    }

    public async Task<ApiResponse<AvionDto>> GetById(string id)
    {
        var response = new ApiResponse<AvionDto>();
        var avion = await _avionRepository.GetById(id);
        if (avion != null) // Verificar si avion es diferente de null
        {
            response.Data = _mapper.Map<AvionDto>(avion);
            return response;
        }
        else
        {
            response.SetError("id no coincide", HttpStatusCode.BadRequest);
            return response;
        }
    }

    public async Task<ApiResponse<AvionDto>> PostAvion(nuevoAvion nuevoAvion)
    {
        if (nuevoAvion == null)
        {
            throw new Exception("abion null");
        }

        Avion avion = _mapper.Map<Avion>(nuevoAvion);
        Avion avionResponse = await _avionRepository.PostAvion(avion);

        
        if (avionResponse == null)
        {
            var resp = new ApiResponse<AvionDto>();
            resp.SetError("Error interno", HttpStatusCode.InternalServerError);
            return resp;
        }

        AvionDto avion1 = _mapper.Map<AvionDto>(avionResponse);

        return new ApiResponse<AvionDto> { Data = avion1 };
    }



    public async Task<ApiResponse<AvionDto>> UpdateAvion(string id, nuevoAvion nuevoAvion)
    {
        if (nuevoAvion == null)
        {

            throw new Exception("Avion nulo");
        }

        Guid idAvion;
        if (!Guid.TryParse(id, out idAvion))
        {
            throw new Exception("id incorrecto");
        }

        Avion av1 = _mapper.Map<Avion>(nuevoAvion);

        Avion avion = await _avionRepository.UpdateAvion(id, av1);

        return new ApiResponse<AvionDto> { Data = _mapper.Map<AvionDto>(avion) };
    }

    public async Task<ApiResponse<HttpStatusCode>> DeleteAvion(string id)
    {
         bool result = await _avionRepository.DeleteAvion(id);

        if (result)
        {
            return new ApiResponse<HttpStatusCode> { Data = HttpStatusCode.OK };
        }
        else {
            throw new Exception("Avion no encontrado");
        }
    }
}

