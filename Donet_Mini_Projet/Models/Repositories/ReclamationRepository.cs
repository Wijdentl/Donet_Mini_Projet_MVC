using Microsoft.EntityFrameworkCore;
using Donet_Mini_Projet.context;
using static Donet_Mini_Projet.Models.Reclamation;

namespace Donet_Mini_Projet.Models.Repositories
{
    public class ReclamationRepository : IRepository<Reclamation>
    {
        private readonly AppDbContext _context;

        public ReclamationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reclamation>> GetAllAsync()
        {
            return await _context.Reclamations.ToListAsync();
        }

        public async Task<Reclamation> GetByIdAsync(int id)
        {
            return await _context.Reclamations.FindAsync(id);
        }

        public async Task AddAsync(Reclamation entity)
        {
            await _context.Reclamations.AddAsync(entity);
        }

        public async Task UpdateAsync(Reclamation entity)
        {
            _context.Reclamations.Update(entity);
            await SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var reclamation = await GetByIdAsync(id);
            if (reclamation != null)
            {
                _context.Reclamations.Remove(reclamation);
                await SaveAsync();
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
