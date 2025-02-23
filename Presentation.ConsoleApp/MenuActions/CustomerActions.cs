

using Business.Models;
using Business.Services;

namespace Presentation.ConsoleApp.MenuActions;

public class CustomerActions(CustomerService customerService)
{
    private readonly CustomerService _customerService = customerService;

    public async Task CreateNewCustomer()
    {
        Console.Clear();
        Console.Write("Enter customer name - (Or enter Q to cancel): ");
        var name = Console.ReadLine();

        if (name?.ToUpper() == "Q")
        {
            return;
        }

        var customerForm = new CustomerRegistrationForm { CustomerName = name };
        var result = await _customerService.CreateCustomerAsync(customerForm);
        Console.WriteLine(result ? "Customer created successfully!" : "Customer creation failed.");

        Console.WriteLine("\nPress any key to return to the Customer Menu");
        Console.ReadKey();
    }

    public async Task GetAllCustomers()
    {
        Console.Clear();
        var customers = await _customerService.GetAllCustomersAsync();
        foreach (var customer in customers)
        {
            Console.WriteLine($"ID: {customer.Id}, Name: {customer.CustomerName}");
        }
        Console.WriteLine("\nPress any key to return to the Customer Menu");
        Console.ReadKey();
    }

    public async Task UpdateCustomer()
    {
        var customers = await _customerService.GetAllCustomersAsync();

        Console.Clear();
        Console.WriteLine("\nAvailable Customers:");

        foreach (var customer in customers)
        {
            Console.WriteLine($"ID: {customer.Id} | Customer Name: {customer.CustomerName}");
        }

        while (true)
        {
            Console.Write("\nEnter customer ID to update - (Or enter Q to cancel): ");
            var input = Console.ReadLine();

            if (input?.ToUpper() == "Q")
            {
                return;
            }

            if (int.TryParse(input, out int customerId))
            {
                var existingCustomer = customers.FirstOrDefault(c => c.Id == customerId);
                if (existingCustomer == null)
                {
                    Console.WriteLine("\nCustomer not found. Please try again.");
                    continue;
                }

                Console.WriteLine($"\nCurrent customer details:\nID: {existingCustomer.Id}\nName: {existingCustomer.CustomerName}");

                Console.Write("Enter new customer name: ");
                var newCustomerName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(newCustomerName))
                {
                    Console.WriteLine("\nCustomer name cannot be empty. Please try again.");
                    continue;
                }

                var success = await _customerService.UpdateCustomerNameAsync(customerId, newCustomerName);
                Console.WriteLine(success ? "\nCustomer updated successfully!" : "\nCustomer update failed.");
                break;
            }
            else
            {
                Console.WriteLine("\nInvalid customer ID format. Please enter a valid ID.");
            }
        }

        Console.WriteLine("\nPress any key to return to the Customer Menu");
        Console.ReadKey();
    }



    public async Task DeleteCustomer()
    {
        var customers = await _customerService.GetAllCustomersAsync();

        Console.Clear();
        Console.WriteLine("\nAvailable Customers:");

        foreach (var customer in customers)
        {
            Console.WriteLine($"ID: {customer.Id} | Customer Name: {customer.CustomerName}");
        }

        while (true)
        {
            Console.WriteLine("\nPlease note that if this customer is linked to a project, the associated project will also be deleted.");
            Console.Write("Enter customer ID to delete - (Or enter Q to cancel): ");
            var input = Console.ReadLine();

            if (input?.ToUpper() == "Q")
            {
                return;
            }

            if (int.TryParse(input, out int customerId))
            {
                var result = await _customerService.RemoveAsync(customerId);
                if (result)
                {
                    Console.WriteLine("\nCustomer deleted successfully!");
                }
                else
                {
                    Console.WriteLine("Invalid ID, please try again.");
                    continue;
                }
                break;
            }
            else
            {
                Console.WriteLine("\nInvalid customer ID format. Please enter a valid ID.");
            }
        }

        Console.WriteLine("\nPress any key to go back to the Customer Menu");
        Console.ReadKey();
    }

}
