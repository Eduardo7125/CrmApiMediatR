using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Client?> GetById(Guid id) =>
            await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);

        public async Task<List<Client>> GetAll() =>
            await _context.Clients.ToListAsync();

        public async Task<List<Client>> GetAllWithPagination(int page, int pageSize) =>
            await _context.Clients
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

        public async Task<Client> Add(Client client)
        {
            await _context.Clients.AddAsync(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task Update(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var clientToDelete = await _context.Clients.FindAsync(id);
            if (clientToDelete != null)
            {
                _context.Clients.Remove(clientToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Client>> SearchByName(string name)
        {
            var lowerCaseName = name.ToLower();
            return await _context.Clients
                .Where(c => c.FirstName.ToLower().Contains(lowerCaseName) || c.LastName.ToLower().Contains(lowerCaseName))
                .ToListAsync();
        }

        public async Task<Client?> GetByEmail(string email) =>
            await _context.Clients.FirstOrDefaultAsync(c => c.Email.ToLower() == email.ToLower());

        public async Task<List<Client>> GetByCompany(string company) =>
            await _context.Clients.Where(c => c.Company.ToLower().Contains(company.ToLower())).ToListAsync();

        public async Task Activate(Guid id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                client.IsActive = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task Deactivate(Guid id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                client.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> Exists(Guid id) =>
            await _context.Clients.AnyAsync(c => c.Id == id);

        public async Task<int> Count() =>
            await _context.Clients.CountAsync();
    }
}