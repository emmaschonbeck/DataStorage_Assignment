using Business.Services;
using Data.Contexts;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presentation.ConsoleApp;

var services = new ServiceCollection()
    .AddDbContext<DataContext>(x => x.UseSqlServer())
    .AddScoped<CustomerRepository>()
    .AddScoped<ProductRepository>()
    .AddScoped<ProjectRepository>()
    .AddScoped<StatusTypeRepository>()
    .AddScoped<UserRepository>()

    .AddScoped<CustomerService>()
    .AddScoped<ProductService>()
    .AddScoped<ProjectService>()
    .AddScoped<StatusTypeService>()
    .AddScoped<UserService>()

    .AddScoped<MenuDialogs>()

    .BuildServiceProvider();

var menuDialogs = services.GetRequiredService<MenuDialogs>();
await menuDialogs.MenuOptions();
    