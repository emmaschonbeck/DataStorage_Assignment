using Business.Services;

namespace Presentation.ConsoleApp;

public class CustomerMenu : IMenu
{
    private readonly CustomerActions _customerActions;

    public CustomerMenu(CustomerActions customerActions)
    {
        _customerActions = customerActions;
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
            Console.WriteLine("3. Update Customers");
            Console.WriteLine("4. Delete Customers");
            Console.WriteLine("5. Return to Main Menu");
            Console.Write("Select an option: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    await _customerActions.CreateNewCustomer();
                    break;

                case "2":
                    await _customerActions.GetAllCustomers();
                    break;

                case "3":
                    await _customerActions.UpdateCustomer();
                    break;

                case "4":
                    await _customerActions.DeleteCustomer();
                    break;

                case "5":
                    return;

                default:
                    Console.WriteLine("Invalid choice, try again!");
                    break;
            }
        }
    }
}
