

using Business.Factories;
using Business.Models;
using Data.Entities;
using Data.Repositories;

namespace Business.Services;

public class CustomerService(CustomerRepository customerRepository)
{
    private readonly CustomerRepository _customerRepository = customerRepository;


    public async Task CreateCustomerAsync(CustomerRegistrationForm form)
    {
        var CustomerEntity = CustomerFactory.Create(form);
        await _customerRepository.AddAsync(CustomerEntity!);

        // Lägg in funktionalitet att en användare ej kan skapas om den redan finns.
    }

    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        var customerEntity = await _customerRepository.GetByIdAsync(id);
        return CustomerFactory.Create(customerEntity!);

        // Klar tror jag
    }

    public async Task<IEnumerable<Customer?>> GetAllCustomersAsync()
    {
        var customerEntities = await _customerRepository.GetAsync();
        return customerEntities.Select(CustomerFactory.Create);

        // Klar tror jag
    }

    public async Task<Customer?> GetCustomerByCustomerNameAsync(string customerName)
    {
        var customerEntity = await _customerRepository.GetByCustomerNameAsync(customerName);
        return CustomerFactory.Create(customerEntity!);

        // Klar tror jag
    }

    public async Task<bool> UpdateAsync(Customer customer)
    {

    }

    public async Task<bool> RemoveAsync(int id)
    {

    }
}
