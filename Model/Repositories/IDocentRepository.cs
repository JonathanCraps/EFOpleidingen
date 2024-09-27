using Model.Entities;

namespace Model.Repositories;
public interface IDocentRepository
{
    public Task<ICollection<Docent>> GetAllDocentenOrderedById();
    public Task<ICollection<Docent>> GetAllDocentenOrderedByNameAsync();
    public Task<ICollection<Docent>> GetDocentenByMinWeddeAsync(decimal minWedde);
    public Task<Docent?> GetDocentById(int id);
    public Task AddDocentAsync(Docent docent);


}
