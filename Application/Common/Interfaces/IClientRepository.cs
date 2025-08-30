using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IClientRepository
    {
        #region CRUD
        Task<Client?> GetById(Guid id);
        Task<List<Client>> GetAll();
        Task<List<Client>> GetAllWithPagination(int page, int pageSize);
        Task<Client> Add(Client client);
        Task Update(Client client);
        Task Delete(Guid id);
        #endregion

        #region Search
        Task<List<Client>> SearchByName(string name);
        Task<Client?> GetByEmail(string email);
        Task<List<Client>> GetByCompany(string company);
        #endregion

        #region Status
        Task Activate(Guid id);
        Task Deactivate(Guid id);
        #endregion

        #region Utilities
        Task<bool> Exists(Guid id);
        Task<int> Count();
        #endregion
    }
}
