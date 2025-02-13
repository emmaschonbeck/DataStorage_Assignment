

using Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class UserRepository(DataContext contexts)
{
    private readonly DataContext _contexts = contexts;

    // CREATE

    // READ

    // UPDATE

    // DELETE
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _contexts.Customers.FirstOrDefaultAsync(x => x.Id == id);
        if (entity != null)
        {
            _contexts.Customers.Remove(entity);
            await _contexts.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
