

using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context)
{
}
