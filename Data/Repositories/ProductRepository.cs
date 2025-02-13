

using Data.Contexts;

namespace Data.Repositories;

public class ProductRepository(DataContext contexts)
{
    private readonly DataContext _contexts = contexts;
}
