

using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class CustomerFactory
{
    public static CustomerEntity? Create(CustomerRegistrationForm form) => form == null ? null : new()
    {
        CustomerName = form.CustomerName,
    };

    public static Customer? Create(CustomerEntity entity) => entity == null ? null : new()
    {
        Id = entity.Id,
        CustomerName = entity.CustomerName,
    };

    /*
       Denna koden är genererad av ChatGPT - Koden definerar en metod som heter Create, den tar emot två parametrar, en Customer-instans och en befintlig
       CustomerEntity. Först kollar metoden ifall antingen Customer eller CustomerEntity är null, och ifall någon av dem är null, så returnerar metoden null.
       Ifall båda parametrarna är glitiga så uppdateras egenskapen CustomerName i existingEntity med värdet från Customer. Och till sist så returnerar metoden
       den uppdaterade existingEntity.
    */
    public static CustomerEntity? Create(Customer customer, CustomerEntity existingEntity)
    {
        if (customer == null || existingEntity == null)
            return null;

        existingEntity.CustomerName = customer.CustomerName;
        return existingEntity;
    }

    public static void UpdateEntity(CustomerEntity entity, Customer customer)
    {
        if (entity == null || customer == null)
            return;

        entity.CustomerName = customer.CustomerName;
    }
}
