

using Data.Contexts;

namespace Data.Repositories;

public class ProjectRepository(DataContext contexts)
{
    private readonly DataContext _contexts = contexts;
}
