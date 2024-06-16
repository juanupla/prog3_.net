using PrimerApi.Models;

namespace PrimerApi.Interfaces
{
    public interface IAvionRepository
    {
        Task<List<Avion>> GetAll();
        Task<Avion> GetById(string id);
        Task<Avion> PostAvion(Avion avion);
        Task<bool> DeleteAvion(string id);
        Task<Avion> UpdateAvion(string id, Avion avionUpdated);

    }
}
