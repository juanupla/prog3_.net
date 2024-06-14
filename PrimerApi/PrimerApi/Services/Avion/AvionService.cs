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

        Models.Avion avion = _mapper.Map<Avion>(nuevoAvion);
        Models.Avion avionResponse = await _avionRepository.PostAvion(avion);
        

        AvionDto avion1 = _mapper.Map<AvionDto>(avionResponse);
        return new ApiResponse<AvionDto> { 
            Data = avion1
        };

    }
}

