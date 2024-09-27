using Model.Entities;
namespace Model.Repositories;
public class SQLDocentOpleidingRepository : IDocentOpleidingRepository
{
    public readonly EFOpleidingenContext _context;
    public SQLDocentOpleidingRepository(EFOpleidingenContext _context) => this._context = _context;

    public async Task<ICollection<DocentOpleiding>> GetAllDocentOpleidingenAsync()
    {
        return await _context.DocentOpleidingen
            .Include(docent => docent.docent)
            .Include(opleiding => opleiding.opleiding)
            .ToListAsync();
    }
    public async Task<ICollection<DocentOpleiding>> GetAllDocentOpleidingenByDocentIdAsync(int gegevenId)
    {
        return await _context.DocentOpleidingen
            .Include(docent => docent.docent)
            .Include(opleiding => opleiding.opleiding)
            .Where(docentOpleiding => docentOpleiding.DocentId == gegevenId)
            .ToListAsync();
    }
}