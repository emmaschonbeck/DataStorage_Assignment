

using Business.Models;
using Business.Services;

namespace Presentation.ConsoleApp;

public class ProjectActions
{
    private readonly ProjectService _projectService;
    private readonly CustomerService _customerService;

    public ProjectActions(ProjectService projectService, CustomerService customerService)
    {
        _projectService = projectService;
        _customerService = customerService;
    }

    public async Task CreateNewProject()
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

        Console.WriteLine("Press any key to return to the Project Menu");
        Console.ReadKey();
    }

    public async Task GetAllProjects()
    {
        var projects = await _projectService.GetAllProjectsAsync();
        Console.Clear();
        Console.WriteLine("List of Projects:\n");

        foreach (var project in projects)
        {
            Console.WriteLine($"ID: {project.Id}");
            Console.WriteLine($"Project Number: {project.ProjectNumber}");
            Console.WriteLine($"Title: {project.Title}");
            Console.WriteLine($"Description: {project.Description ?? "No description available"}");
            Console.WriteLine($"Start Date: {project.StartDate:yyyy-MM-dd}");
            Console.WriteLine($"End Date: {project.EndDate:yyyy-MM-dd}");
            Console.WriteLine($"Customer: {project.Customer?.CustomerName}");
            Console.WriteLine(new string('-', 40));
        }

        Console.WriteLine("Press any key to return to the Project Menu");
        Console.ReadKey();
    }


    public async Task UpdateProject()
    {
        Console.Write("Enter project ID to update (e.g., p-102): ");
        var input = Console.ReadLine();

        if (!string.IsNullOrEmpty(input) && input.StartsWith("p-"))
        {
            var result = await _projectService.UpdateAsync(input);
            Console.WriteLine(result ? "Project updated successfully!" : "Project update failed.");
        }
        else
        {
            Console.WriteLine("Invalid project ID format. Please enter a valid ID (e.g., p-102).");
        }

        Console.WriteLine("Press any key to return to the Project Menu");
        Console.ReadKey();
    }

    public async Task DeleteProject()
    {
        Console.Write("Enter project ID to delete (e.g., p-101): ");
        var input = Console.ReadLine();

        if (!string.IsNullOrEmpty(input) && input.StartsWith("p-"))
        {
            var result = await _projectService.RemoveAsync(input);
            Console.WriteLine(result ? "Project deleted successfully!" : "Project deletion failed.");
        }
        else
        {
            Console.WriteLine("Invalid project ID format. Please enter a valid ID (e.g., p-101).");
        }

        Console.WriteLine("Press any key to return to the Project Menu");
        Console.ReadKey();
    }
}
