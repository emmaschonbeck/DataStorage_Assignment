
using System;
using Business.Models;
using Business.Services;

namespace Presentation.ConsoleApp;

public class MainMenu : IMenu
{
    private readonly ProjectMenu _projectMenu;
    private readonly CustomerMenu _customerMenu;

    public MainMenu(ProjectMenu projectMenu, CustomerMenu customerMenu)
    {
        _projectMenu = projectMenu;
        _customerMenu = customerMenu;
    }

    public async Task ShowMenuAsync()
    {
        while (true)
        {
            try
            {
                Console.Clear();
                Console.WriteLine("==== Main Menu ====");
                Console.WriteLine("1. Manage Projects");
                Console.WriteLine("2. Manage Customers");
                Console.WriteLine("3. Exit");

                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        await _projectMenu.ShowMenuAsync();
                        break;
                    case "2":
                        await _customerMenu.ShowMenuAsync();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("\nInvalid choice, please try again");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nError: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to return to the menu...");
            Console.ReadKey();
        }
    }

}

// fortsätta lägga in alla dialoger osv, som vi gjorde i c# kursen.
