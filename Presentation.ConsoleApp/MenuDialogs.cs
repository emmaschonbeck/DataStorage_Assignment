

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
            Console.Clear();
            Console.WriteLine("Main Menu");
            Console.WriteLine("1. Manage Projects");
            Console.WriteLine("2. Manage Customers");
            Console.WriteLine("3. Exit");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await ShowProjectMenu();
                    break;

                case "2":
                    await ShowCustomerMenu();
                    break;

                case "3":
                    return;

                default:
                    Console.WriteLine("Invalid choice, please try again");
                    break;
            }
        }
    }

    private async Task ShowProjectMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Manage Projects");
            Console.WriteLine("1. Create Projects");
            Console.WriteLine("2. List Projects");
            Console.WriteLine("3. Update Projects");
            Console.WriteLine("4. Delete Projects");
            Console.WriteLine("5. Return to Main Menu");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await CreateNewProject();
                    break;
                case "2":
                    await GetAllProjects();
                    break;
                case "3":
                    await UpdateProject();
                    break;
                case "4":
                    await DeleteProject();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice, please try again!");
                    break;
            }
        }
    }

    private async Task ShowCustomerMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Manage Customers:");
            Console.WriteLine("1. Create Customer");
            Console.WriteLine("2. List Customers");
            Console.WriteLine("3. Return to Main Menu");
            Console.Write("Select an option: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await CreateNewCustomer();
                    break;
                case "2":
                    await GetAllCustomers();
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Invalid choice, try again!");
                    break;
            }
        }
    }

    private async Task CreateNewProject()
    {
        var customers = await _customerService.GetAllCustomersAsync();

        Console.WriteLine("Select a customer:");
        var customerList = customers.ToList();
        for (int i = 0; i < customerList.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {customerList[i]?.CustomerName} (ID: {customerList[i]?.Id})");
        }

        Console.Write("Enter the customer number: ");
        int customerIndex;
        while (!int.TryParse(Console.ReadLine(), out customerIndex) || customerIndex < 1 || customerIndex > customerList.Count)
        {
            Console.WriteLine("Invalid selection. Please enter a valid customer number.");
        }

        int customerId = customerList[customerIndex - 1]?.Id ?? 0;

        Console.Write("Enter project title: ");
        var title = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Title is required");
            return;
        }

        Console.Write("Enter project description (optional): ");
        var description = Console.ReadLine();

        DateTime startDate;
        Console.Write("Enter project start date (yyyy-MM-dd): ");
        while (!DateTime.TryParse(Console.ReadLine(), out startDate))
        {
            Console.WriteLine("Invalid date format. Please enter a valid date.");
        }

        DateTime endDate;
        Console.Write("Enter project end date (yyyy-MM-dd): ");
        while (!DateTime.TryParse(Console.ReadLine(), out endDate) || endDate < startDate)
        {
            Console.WriteLine("Invalid date format or end date is earlier than start date. Please enter a valid end date.");
        }

        var projectForm = new ProjectRegistrationForm
        {
            Title = title,
            Description = description,
            StartDate = startDate,
            EndDate = endDate,
            CustomerId = customerId
        };

        var result = await _projectService.CreateProjectAsync(projectForm);
        Console.WriteLine(result ? "Project created successfully!" : "Project creation failed.");
        Console.ReadLine();
    }

    private async Task GetAllProjects()
    {
        var projects = await _projectService.GetAllProjectsAsync();
        foreach (var project in projects)
        {
            Console.WriteLine($"ID: {project.Id}, Project Number: {project.ProjectNumber}, Title: {project.Title}");
        }
        Console.ReadLine();
    }

    private async Task UpdateProject()
    {
        Console.Write("Enter project ID to update: ");
        var id = int.Parse(Console.ReadLine());

        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null)
        {
            Console.WriteLine("Project not found.");
            return;
        }

        Console.Write($"New Title ({project.Title}): ");
        var title = Console.ReadLine();
        project.Title = string.IsNullOrWhiteSpace(title) ? project.Title : title;

        var result = await _projectService.UpdateAsync(project);
        Console.WriteLine(result ? "Project updated successfully!" : "Project update failed.");
        Console.ReadLine();
    }

    private async Task DeleteProject()
    {
        Console.Write("Enter project ID to delete: ");
        var id = int.Parse(Console.ReadLine());

        var result = await _projectService.RemoveAsync(id);
        Console.WriteLine(result ? "Project deleted successfully!" : "Project deletion failed.");
        Console.ReadLine();
    }

    private async Task CreateNewCustomer()
    {
        Console.Write("Enter customer name: ");
        var name = Console.ReadLine();

        var customerForm = new CustomerRegistrationForm { CustomerName = name };
        var result = await _customerService.CreateCustomerAsync(customerForm);
        Console.WriteLine(result ? "Customer created successfully!" : "Customer creation failed.");
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
}

// fortsätta lägga in alla dialoger osv, som vi gjorde i c# kursen.
