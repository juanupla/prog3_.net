using PrimerApi.Dto;
using PrimerApi.Models;
using PrimerApi.Query;
using PrimerApi.Response;
using System.Net;

namespace PrimerApi.Interfaces.Services
{
    public interface IAvionService
    {
        Task<ApiResponse<List<AvionDto>>> GetAll();
        Task<ApiResponse<AvionDto>> GetById(string id);
        Task<ApiResponse<AvionDto>> PostAvion(nuevoAvion nuevoAvion);
        Task<ApiResponse<AvionDto>> UpdateAvion(string id, nuevoAvion nuevoAvion);
        Task<ApiResponse<HttpStatusCode>> DeleteAvion(string id);
    }
}
