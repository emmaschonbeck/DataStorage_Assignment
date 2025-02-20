using System;
using System.Threading.Tasks;
using Business.Models;
using Business.Services;

namespace Presentation.ConsoleApp;

public class CustomerMenu : IMenu
{
    private readonly CustomerService _customerService;

    public CustomerMenu(CustomerService customerService)
    {
        _customerService = customerService;
    }

    public async Task ShowMenuAsync()
    {
        await ShowCustomerMenu();
    }

    private async Task ShowCustomerMenu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Manage Customers:");
            Console.WriteLine("1. Create Customer");
            Console.WriteLine("2. List Customers");
            Console.WriteLine("3. Delete Customers");
            Console.WriteLine("4. Return to Main Menu");
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
                    await DeleteCustomer();
                    break;

                case "4":
                    return;

                default:
                    Console.WriteLine("Invalid choice, try again!");
                    break;
            }
        }
    }

    private async Task CreateNewCustomer()
    {
        Console.Write("Enter customer name: ");
        var name = Console.ReadLine();

        var customerForm = new CustomerRegistrationForm { CustomerName = name };
        var result = await _customerService.CreateCustomerAsync(customerForm);
        Console.WriteLine(result ? "Customer created successfully!" : "Customer creation failed.");

        Console.WriteLine("Press any key to return to the Customer Menu");
        Console.ReadKey();
    }

    private async Task GetAllCustomers()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        foreach (var customer in customers)
        {
            Console.WriteLine($"ID: {customer.Id}, Name: {customer.CustomerName}");
        }
        Console.WriteLine("Press any key to return to the Customer Menu");
        Console.ReadKey();
    }

    private async Task DeleteCustomer()
    {
        Console.Write("Enter customer ID to delete: ");
        if (int.TryParse(Console.ReadLine(), out int customerId))
        {
            var result = await _customerService.RemoveAsync(customerId);
            Console.WriteLine(result ? "Customer deleted successfully!" : "Customer deletion failed.");
        }
        else
        {
            Console.WriteLine("Invalid customer ID format. Please enter a valid ID.");
        }

        Console.WriteLine("Press any key to go back to the Customer Menu");
        Console.ReadKey();
    }
}
