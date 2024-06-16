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

        public async Task<bool> DeleteAvion(string id)
        {
            Guid avionId;
            if (!Guid.TryParse(id, out avionId))
            {
                throw new Exception("ID no válido");
            }

            var avion = await _contextDb.Aviones.FindAsync(avionId);
            if (avion == null)
            {
                throw new Exception("Avión no encontrado");
            }

            _contextDb.Aviones.Remove(avion);
            await _contextDb.SaveChangesAsync();

            return true;
        }

        public async Task<Avion> UpdateAvion(string id, Avion avionUpdated)
        {
            if (avionUpdated == null)
            {
                throw new Exception("Avión null");
            }

            Guid avionId;
            if (!Guid.TryParse(id, out avionId))
            {
                throw new Exception("ID no válido");
            }

            var avion = await _contextDb.Aviones.FindAsync(avionId);
            if (avion == null)
            {
                throw new Exception("Avión no encontrado");
            }

            avion.IdMarca = avionUpdated.IdMarca;
            avion.CantidadPasajeros = avionUpdated.CantidadPasajeros;
            avion.Matricula = avionUpdated.Matricula;
            avion.FechaAlta = avionUpdated.FechaAlta;
            avion.Modelo = avionUpdated.Modelo;

            _contextDb.Aviones.Update(avion);
            await _contextDb.SaveChangesAsync();

            return avionUpdated;


        }
    }
}
