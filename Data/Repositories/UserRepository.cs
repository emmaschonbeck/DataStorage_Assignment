

using Data.Contexts;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class UserRepository(DataContext context) : BaseRepository<UserEntity>(context)
{
}
