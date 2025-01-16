using Donet_Mini_Projet.context;
using Microsoft.EntityFrameworkCore;
namespace Donet_Mini_Projet.Models.Repositories
{
    public class PieceDeRechangeRepository : IRepository<PieceDeRechange>
    {
        private readonly AppDbContext _context;

        public PieceDeRechangeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PieceDeRechange>> GetAllAsync()
        {
            return await _context.PieceDeRechanges.ToListAsync();
        }

        public async Task<PieceDeRechange> GetByIdAsync(int id)
        {
            return await _context.PieceDeRechanges.FindAsync(id);
        }

        public async Task AddAsync(PieceDeRechange entity)
        {
            await _context.PieceDeRechanges.AddAsync(entity);
        }

        public async Task UpdateAsync(PieceDeRechange entity)
        {
            _context.PieceDeRechanges.Update(entity);
            await SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var piece = await GetByIdAsync(id);
            if (piece != null)
            {
                _context.PieceDeRechanges.Remove(piece);
                await SaveAsync();
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
