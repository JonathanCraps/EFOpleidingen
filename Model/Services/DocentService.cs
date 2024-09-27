using Model.Entities;
using Model.Repositories;
using System.Collections;

namespace Model.Services;
public class DocentService
{
   private IDocentRepository docentRepository;
   
   public DocentService(IDocentRepository docentRepository) => this.docentRepository = docentRepository;
    public async Task<ICollection<Docent>> GetAllDocentenOrderedById() => await docentRepository.GetAllDocentenOrderedById();
    public async Task<ICollection<Docent>> GetAllDocentenOrderedByNameAsync() => await docentRepository.GetAllDocentenOrderedByNameAsync();
    public async Task<ICollection<Docent>> GetDocentenByMinWeddeAsync(decimal minWedde) => await docentRepository.GetDocentenByMinWeddeAsync(minWedde);
    public async Task<Docent?> GetDocentById(int id) => await docentRepository.GetDocentById(id);
    public async Task AddDocentAsync(Docent docent) => await docentRepository.AddDocentAsync(docent);


}
