using Model.Entities;
using System.Collections;

namespace Model.Repositories;
public class SQLDocentRepository : IDocentRepository
{
    public readonly EFOpleidingenContext context;

    public SQLDocentRepository(EFOpleidingenContext context) => this.context = context;
    public async Task<ICollection<Docent>> GetAllDocentenOrderedById()
    {
        return await context.Docenten.OrderBy(docent => docent.Id).ToListAsync();
    }
    public async Task<ICollection<Docent>> GetAllDocentenOrderedByNameAsync()
    {
        return await context.Docenten.OrderBy(docent => docent.Familienaam).ThenBy(docent => docent.Voornaam).ToListAsync();
    }
    public async Task<ICollection<Docent>> GetDocentenByMinWeddeAsync(decimal minWedde)
    {
        return await context.Docenten
            .Where(docent => docent.Wedde >= minWedde)
            .OrderBy(docent => docent.Wedde)
            .ToListAsync();
    }
    public async Task<Docent?> GetDocentById(int id)
    {
        return await context.Docenten.FindAsync(id);
    }
    public async Task AddDocentAsync(Docent docent)
    {
        await context.Docenten.AddAsync(docent);
        await context.SaveChangesAsync();
    }

}
