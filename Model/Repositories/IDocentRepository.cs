using Model.Entities;

namespace Model.Repositories;
public interface IDocentRepository
{
    public Task<ICollection<Docent>> GetAllDocentenAsync();
    public Task AddDocentAsync(Docent docent);
}
