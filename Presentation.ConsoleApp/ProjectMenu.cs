using Business.Models;
using Business.Services;

namespace Presentation.ConsoleApp;

public class ProjectMenu : IMenu
{
    private readonly ProjectActions _projectActions;

    public ProjectMenu(ProjectActions projectActions)
    {
        _projectActions = projectActions;
    }

    public async Task ShowMenuAsync()
    {
        await ShowProjectMenu();
    }

    private async Task ShowProjectMenu()
    {
        while (true)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("==== Manage Projects ====");
                Console.WriteLine("1. Create Projects");
                Console.WriteLine("2. List Projects");
                Console.WriteLine("3. Update Projects");
                Console.WriteLine("4. Delete Projects");
                Console.WriteLine("5. Return to Main Menu");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        await _projectActions.CreateNewProject();
                        break;
                    case "2":
                        await _projectActions.GetAllProjects();
                        break;
                    case "3":
                        await _projectActions.UpdateProject();
                        break;
                    case "4":
                        await _projectActions.DeleteProject();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice, please try again!");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }
        }
    }
}
