using Model.Entities;
using System.Collections;

namespace Model.Repositories;
public class SQLDocentRepository : IDocentRepository
{
     public readonly EFOpleidingenContext context;

    public SQLDocentRepository(EFOpleidingenContext context) => this.context = context;
    public async Task<ICollection<Docent>> GetAllDocentenAsync()
    {
        return await context.Docenten.ToListAsync();
    }
}
