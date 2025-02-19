﻿using Business.Services;
using Data.Contexts;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presentation.ConsoleApp;

var services = new ServiceCollection()
    .AddDbContext<DataContext>(x => x.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Emma\\Desktop\\Skola\\Datalagringskurs\\DataStorage_Assignment\\Data\\Databases\\local_database.mdf\";Integrated Security=True;Connect Timeout=30"))
    
    .AddScoped<CustomerRepository>()
    .AddScoped<ProjectRepository>()

    .AddScoped<CustomerService>()
    .AddScoped<ProjectService>()

    .AddScoped<MenuDialogs>()

    .BuildServiceProvider();

var menuDialogs = services.GetRequiredService<MenuDialogs>();
await menuDialogs.MenuOptions();
    