

using Business.Factories;
using Business.Models;
using Data.Entities;
using Data.Repositories;

namespace Business.Services;

public class CustomerService(CustomerRepository customerRepository)
{
    private readonly CustomerRepository _customerRepository = customerRepository;

    public async Task<bool> CreateCustomerAsync(CustomerRegistrationForm form)
    {
        var existingCustomer = await _customerRepository.GetAsync(c => c.CustomerName == form.CustomerName);
        if (existingCustomer != null)
        {
            return false;
        }

        var customerEntity = CustomerFactory.Create(form);
        await _customerRepository.AddAsync(customerEntity!);
        return true;
    }

    public async Task<Customer?> GetCustomerByIdAsync(int id)
    {
        var customerEntity = await _customerRepository.GetAsync(c => c.Id == id);
        return CustomerFactory.Create(customerEntity!);
    }

    public async Task<IEnumerable<Customer?>> GetAllCustomersAsync()
    {
        var customerEntities = await _customerRepository.GetAsync();
        return customerEntities.Select(CustomerFactory.Create);
    }

    public async Task<Customer?> GetCustomerByCustomerNameAsync(string customerName)
    {
        var customerEntity = await _customerRepository.GetAsync(c => c.CustomerName == customerName);
        return customerEntity != null ? CustomerFactory.Create(customerEntity) : null;
    }

    public async Task<bool> UpdateAsync(Customer customer)
    {
        var existingCustomer = await _customerRepository.GetAsync(c => c.Id == customer.Id);
        if (existingCustomer == null)
        {
            return false;
        }

        CustomerFactory.UpdateEntity(existingCustomer, customer);

        await _customerRepository.UpdateAsync(existingCustomer);
        return true;
    }

    public async Task<bool> RemoveAsync(int customerId) // Ändra till int
    {
        var customerEntity = await _customerRepository.GetAsync(c => c.Id == customerId); // Kontrollera att 'Id' är av typen int
        if (customerEntity == null)
        {
            return false; // Kunden hittades inte
        }

        await _customerRepository.RemoveAsync(customerEntity);
        return true; // Kunden raderades framgångsrikt
    }
}
