

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

    public async Task<bool> UpdateCustomerNameAsync(int customerId, string newCustomerName)
    {
        var existingCustomer = await _customerRepository.GetAsync(c => c.Id == customerId);
        if (existingCustomer == null)
        {
            Console.WriteLine($"Customer with ID {customerId} not found.");
            return false;
        }

        Console.WriteLine("Current customer details:");
        Console.WriteLine($"ID: {existingCustomer.Id}");
        Console.WriteLine($"Name: {existingCustomer.CustomerName}");

        if (!string.IsNullOrWhiteSpace(newCustomerName))
        {
            existingCustomer.CustomerName = newCustomerName;
        }

        await _customerRepository.UpdateAsync(existingCustomer);
        return true;
    }

    public async Task<bool> RemoveAsync(int customerId)
    {
        var customerEntity = await _customerRepository.GetAsync(c => c.Id == customerId);
        if (customerEntity == null)
        {
            return false;
        }

        await _customerRepository.RemoveAsync(customerEntity);
        return true;
    }
}
