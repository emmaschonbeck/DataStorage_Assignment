

using Business.Models;
using Business.Services;

namespace Presentation.ConsoleApp.MenuActions;

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

        Console.Clear();
        Console.WriteLine("\nAvailable customers:");

        foreach (var customer in customers)
        {
            Console.WriteLine($"ID: {customer.Id} | Name: {customer.CustomerName}");
        }

        int customerId;

        while (true)
        {
            Console.Write("\nEnter the customer ID - (Or enter Q to cancel): ");
            var input = Console.ReadLine();

            if (input?.ToUpper() == "Q")
            {
                return;
            }

            if (!int.TryParse(input, out customerId) || !customers.Any(c => c.Id == customerId))
            {
                Console.WriteLine("Invalid customer ID. Please enter a valid ID from the list.");
                continue;
            }

            break;
        }

        Console.Write("Enter project title: ");
        var title = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(title))
        {
            Console.WriteLine("Title is required.");
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

        /*
           Denna koden är genererad av ChatGPT - Koden skapar ett nýtt objekt med information om ett projekt som inkluderar titel, beskrivning,
           start och slutdatum samt kund ID. Sedan anropas en metod för att skapa projektet i databasen via denna koden: _projectService.CreateProjectAsync(projectForm)
           Om projektet skapas utan några problem så kommer ett bekräftande meddelande, annars kommer ett felmeddelande upp.
        */
        var projectForm = new ProjectRegistrationForm
        {
            Title = title,
            Description = description,
            StartDate = startDate,
            EndDate = endDate,
            CustomerId = customerId
        };

        var result = await _projectService.CreateProjectAsync(projectForm);
        Console.WriteLine(result ? "\nProject created successfully!" : "\nProject creation failed.");

        Console.WriteLine("Press any key to return to the menu.");
        Console.ReadKey();
    }


    public async Task GetAllProjects()
    {
        var projects = await _projectService.GetAllProjectsAsync();

        Console.Clear();
        Console.WriteLine("==== List of Projects: ====\n");

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
        Console.WriteLine("Press any key to return to the menu.");
        Console.ReadKey();
    }


    public async Task UpdateProject()
    {
        var projects = await _projectService.GetAllProjectsAsync();

        Console.Clear();
        Console.WriteLine("\nAvailable projects:");

        foreach (var project in projects)
        {
            Console.WriteLine($"ID: {project.Id} | Project Number: {project.ProjectNumber} | Title: {project.Title}");
        }

        string input;

        while (true)
        {
            Console.Write("\nEnter Project Number to update (e.g.: p-102) - (Or enter Q to cancel): ");
            input = Console.ReadLine();
            Console.WriteLine("");

            if (input?.ToUpper() == "Q")
            {
                return;
            }

            if (!string.IsNullOrEmpty(input) && input.StartsWith("p-"))
            {
                var existingProject = projects.FirstOrDefault(p => p.ProjectNumber.Equals(input, StringComparison.OrdinalIgnoreCase));
                if (existingProject == null)
                {
                    Console.WriteLine("Project not found. Please enter a valid Project Number from the list.");
                    continue;
                }

                var result = await _projectService.UpdateAsync(input);
                Console.WriteLine(result ? "\nProject updated successfully!" : "Project update failed.");
                break;
            }
            else
            {
                Console.WriteLine("Invalid project ID format. Please enter a valid ID (e.g., p-102).");
            }
        }

        Console.WriteLine("Press any key to return to the Project Menu");
        Console.ReadKey();
    }


    public async Task DeleteProject()
    {
        var projects = await _projectService.GetAllProjectsAsync();

        Console.Clear();
        Console.WriteLine("\nAvailable projects:");

        foreach (var project in projects)
        {
            Console.WriteLine($"ID: {project.Id} | Project Number: {project.ProjectNumber} | Title: {project.Title}");
        }

        string input;

        while (true)
        {
            Console.Write("\nEnter project ID to delete (e.g.: p-101) - (Or enter Q to cancel): ");
            input = Console.ReadLine();

            if (input?.ToUpper() == "Q")
            {
                return;
            }

            if (!string.IsNullOrEmpty(input) && input.StartsWith("p-"))
            {
                var existingProject = projects.FirstOrDefault(p => p.ProjectNumber.Equals(input, StringComparison.OrdinalIgnoreCase));
                if (existingProject == null)
                {
                    Console.WriteLine("\nProject not found. Please enter a valid Project Number from the list.");
                    continue;
                }

                var result = await _projectService.RemoveAsync(input);
                Console.WriteLine(result ? "\nProject was deleted successfully!" : "\nProject deletion failed.");
                break;
            }
            else
            {
                Console.WriteLine("\nInvalid project ID format. Please enter a valid ID (e.g.: p-101).");
            }
        }

        Console.WriteLine("Press any key to return to the Project Menu");
        Console.ReadKey();
    }

}
