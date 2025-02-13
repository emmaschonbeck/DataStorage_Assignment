

using Data.Contexts;

namespace Data.Repositories;

public class CustomerRepository(DataContext contexts)
{
    private readonly DataContext _contexts = contexts;
}
