using Microsoft.EntityFrameworkCore;
using PrimerApi.Data;
using PrimerApi.Interfaces;
using PrimerApi.Models;

namespace PrimerApi.Repos
{
    public class AvionRepository : IAvionRepository
    {
        private readonly ContextDb _contextDb;

        public AvionRepository(ContextDb contextDb)
        {
            _contextDb = contextDb;
        }

        public async Task<List<Avion>> GetAll()
        {
            var avion = await _contextDb.Aviones.Include(c => c.MarcaAvion).ToListAsync();
            return avion;
        }

        public async Task<Avion> GetById(string id)
        {
            Guid avionId;
            Guid.TryParse(id, out avionId);
            var avion = await _contextDb.Aviones.Where(c => c.Id.Equals(avionId)).Include(c => c.MarcaAvion).FirstOrDefaultAsync();

            if (avion != null)
            {
                return avion;
            }

            throw new Exception("Usuario no encontrado");
        }

        public async Task<Avion> PostAvion(Avion avion)
        {
            if (avion == null) {
                throw new Exception("avion nulo");
            }

            await _contextDb.AddAsync(avion);
            await _contextDb.SaveChangesAsync();

            return avion;
        }


    }
}
