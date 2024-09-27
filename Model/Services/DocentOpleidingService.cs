using Model.Entities;
using Model.Repositories;

namespace Model.Services;
public class DocentOpleidingService
{
    private IDocentOpleidingRepository _repository;

    public DocentOpleidingService(IDocentOpleidingRepository repository) => this._repository = repository;

    public async Task<ICollection<DocentOpleiding>> GetAllDocentOpleidingenAsync() => await _repository.GetAllDocentOpleidingenAsync();
    public async Task<ICollection<DocentOpleiding>> GetAllDocentOpleidingenByDocentIdAsync(int gegevenId) => await _repository.GetAllDocentOpleidingenByDocentIdAsync(gegevenId);
}
