

using Business.Models;
using Business.Services;

namespace Presentation.ConsoleApp;

public class CustomerActions(CustomerService customerService)
{
    private readonly CustomerService _customerService = customerService;

    public async Task CreateNewCustomer()
    {
        Console.Write("Enter customer name: ");
        var name = Console.ReadLine();

        var customerForm = new CustomerRegistrationForm { CustomerName = name };
        var result = await _customerService.CreateCustomerAsync(customerForm);
        Console.WriteLine(result ? "Customer created successfully!" : "Customer creation failed.");

        Console.WriteLine("Press any key to return to the Customer Menu");
        Console.ReadKey();
    }

    public async Task GetAllCustomers()
    {
        var customers = await _customerService.GetAllCustomersAsync();
        foreach (var customer in customers)
        {
            Console.WriteLine($"ID: {customer.Id}, Name: {customer.CustomerName}");
        }
        Console.WriteLine("Press any key to return to the Customer Menu");
        Console.ReadKey();
    }

    public async Task UpdateCustomer()
    {
        Console.Write("Enter customer ID to update: ");
        var input = Console.ReadLine();

        if (int.TryParse(input, out int customerId))
        {
            Console.Write("Enter new customer name: ");
            var newCustomerName = Console.ReadLine();

            var success = await _customerService.UpdateCustomerNameAsync(customerId, newCustomerName);
            Console.WriteLine(success ? "Customer updated successfully!" : "Customer update failed.");
        }
        else
        {
            Console.WriteLine("Invalid customer ID format.");
        }

        Console.WriteLine("Press any key to return to the Customer Menu");
        Console.ReadKey();
    }


    public async Task DeleteCustomer()
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
