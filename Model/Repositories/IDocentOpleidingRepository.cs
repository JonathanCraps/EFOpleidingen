using Model.Entities;

namespace Model.Repositories;
public interface IDocentOpleidingRepository
{
    public Task<ICollection<DocentOpleiding>> GetAllDocentOpleidingenAsync();
}
