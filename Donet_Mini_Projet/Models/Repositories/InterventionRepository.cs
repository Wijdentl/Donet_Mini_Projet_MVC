using Microsoft.EntityFrameworkCore;
using Donet_Mini_Projet.context;

namespace Donet_Mini_Projet.Models.Repositories
{
    public class InterventionRepository : IRepository<Intervention>
    {
        private readonly AppDbContext _context;

        public InterventionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Intervention>> GetAllAsync()
        {
            return await _context.Interventions.ToListAsync();
        }

        public async Task<Intervention> GetByIdAsync(int id)
        {
            return await _context.Interventions.FindAsync(id);
        }

        public async Task AddAsync(Intervention entity)
        {
            await _context.Interventions.AddAsync(entity);
        }

        public async Task UpdateAsync(Intervention entity)
        {
            _context.Interventions.Update(entity);
            await SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var intervention = await GetByIdAsync(id);
            if (intervention != null)
            {
                _context.Interventions.Remove(intervention);
                await SaveAsync();
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
