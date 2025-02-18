

using Business.Models;
using Business.Services;

namespace Presentation.ConsoleApp;

public class MenuDialogs(CustomerService customerService, ProjectService projectService)
{
    private readonly CustomerService _customerService = customerService;
    private readonly ProjectService _projectService = projectService;

    public async Task MenuOptions()
    {
        while (true)
        {
            Console.WriteLine("Please select an option.");
            Console.WriteLine("1. Create New Customer");
            Console.WriteLine("2. Create New Project");
            Console.WriteLine("3. Get All Customers");
            Console.WriteLine("4. Get All Projects");
            Console.WriteLine("5. Get Customer");
            Console.WriteLine("6. Get Project");
            Console.WriteLine("7. Edit Project");
            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await CreateNewCustomer();
                    break;

                case "2":
                    await CreateNewProject();
                    break;

                case "3":
                    await GetAllCustomers();
                    break;

                case "4":
                    await GetAllProjects();
                    break;

                case "5":
                    await GetCustomerById();
                    break;

                case "6":
                    await GetProjectById();
                    break;

                default:
                    Console.WriteLine("Invalid choice, try again!");
                    break;
            }
        }
    }

    private async Task CreateNewCustomer()
    {
        Console.WriteLine("Enter customer name: ");
        var name = Console.ReadLine();
    }

    private async Task CreateNewProject()
    {
        Console.WriteLine("Enter project title: ");
        var title = Console.ReadLine();

        Console.WriteLine("Enter project description (optional): ");
        var description = Console.ReadLine();

        Console.WriteLine("Enter project start date (yyyy-MM-dd): ");
        var startDate = DateTime.Parse(Console.ReadLine());

        Console.WriteLine("Enter project end date (yyyy-MM-dd): ");
        var endDate = DateTime.Parse(Console.ReadLine());

        var projectForm = new ProjectRegistrationForm
        {
            Title = title,
            Description = description,
            StartDate = startDate,
            EndDate = endDate,
        };

        var result = await _projectService.CreateProjectAsync(projectForm);

        if (result)
        {
            Console.WriteLine("Project created successfully!");
        }
        else
        {
            Console.WriteLine("Project creation failed. Please try again!");
        }

        Console.ReadLine();
    }

    private async Task GetAllCustomers()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        foreach (var customer in customers)
        {
            Console.WriteLine($"ID: {customer.Id}, Name: {customer.CustomerName}");
        }

        Console.ReadLine();
    }

    private async Task GetAllProjects()
    {
        var projects = await _projectService.GetAllProjectsAsync();
        foreach (var project in projects)
        {
            Console.WriteLine($"ID: {project.Id}, Title: {project.Title}");
        }

        Console.ReadLine();
    }

    private async Task GetCustomerById()
    {
        Console.WriteLine("Enter customer ID: ");
        var id = int.Parse(Console.ReadLine());

        var customer = await _customerService.GetCustomerByIdAsync(id);

        if (customer != null)
        {
            Console.WriteLine($"Customer ID: {customer.Id}, Name: {customer.CustomerName}");
        }
        else
        {
            Console.WriteLine("Customer not found.");
        }

        Console.ReadLine();
    }

    private async Task GetProjectById()
    {
        Console.WriteLine("Enter project ID: ");
        var id = int.Parse(Console.ReadLine());

        var project = await _projectService.GetProjectByIdAsync(id);

        if (project != null)
        {
            Console.WriteLine($"Project ID: {project.Id}, Title: {project.Title}, Project Number: {project.ProjectNumber}");
        }
        else
        {
            Console.WriteLine("Project not found.");
        }

        Console.ReadLine();
    }
}

// fortsätta lägga in alla dialoger osv, som vi gjorde i c# kursen.
