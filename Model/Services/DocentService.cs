using Model.Entities;
using Model.Repositories;
using System.Collections;

namespace Model.Services;
public class DocentService
{
   private IDocentRepository docentRepository;
   
   public DocentService(IDocentRepository docentRepository) => this.docentRepository = docentRepository;
    public async Task<ICollection<Docent>> GetAllDocentenAsync() => await docentRepository.GetAllDocentenAsync();
   public async Task AddDocentAsync(Docent docent) => await docentRepository.AddDocentAsync(docent);

}
