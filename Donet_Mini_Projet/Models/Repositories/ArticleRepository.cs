using Donet_Mini_Projet.context;
using Microsoft.EntityFrameworkCore;
namespace Donet_Mini_Projet.Models.Repositories
{
    public class ArticleRepository : IRepository<Article>
    {
        private readonly AppDbContext _context;

        public ArticleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Article>> GetAllAsync()
        {
            return await _context.Articles.ToListAsync();
        }

        public async Task<Article> GetByIdAsync(int id)
        {
            return await _context.Articles.FindAsync(id);
        }

        public async Task AddAsync(Article entity)
        {
            await _context.Articles.AddAsync(entity);
        }

        public async Task UpdateAsync(Article entity)
        {
            _context.Articles.Update(entity);
            await SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var article = await GetByIdAsync(id);
            if (article != null)
            {
                _context.Articles.Remove(article);
                await SaveAsync();
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
