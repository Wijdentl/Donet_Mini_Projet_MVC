using Microsoft.EntityFrameworkCore;
using Donet_Mini_Projet.context;

namespace Donet_Mini_Projet.Models.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client> GetByIdAsync(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task AddAsync(Client entity)
        {
            await _context.Clients.AddAsync(entity);
        }

        public async Task UpdateAsync(Client entity)
        {
            _context.Clients.Update(entity);
            await SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var client = await GetByIdAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await SaveAsync();
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
