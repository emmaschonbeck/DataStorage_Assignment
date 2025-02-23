

using Business.Models;
using Data.Entities;

namespace Business.Factories;

public class ProjectFactory
{
    public static ProjectEntity Create(ProjectModel project)
    {
        return new ProjectEntity
        {
            Id = project.Id,
            ProjectNumber = project.ProjectNumber,
            Title = project.Title,
            Description = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate
        };
    }

    /*
       Denna koden är genererad av ChatGPT - Nedan metod gör en omvandling från en databaspost, från ProjectEntity,till en användbar modell, i ProjectModel, så att
       vi kan arbeta med den i vår app. Genom att extrahera och mappa alla dessa egenskaper, får vi en mer strukturerad och anpassad representation av projektdata.
       Metoden hanterar även vår kundinformation, detta gör det möjligt att på ett enkelt sätt associera varje projekt med dess tillhörande kund.
    */
    public static ProjectModel Create(ProjectEntity entity)
    {
        return new ProjectModel
        {
            Id = entity.Id,
            ProjectNumber = entity.ProjectNumber,
            Title = entity.Title,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            CustomerId = entity.CustomerId,
            Customer = entity.Customer != null ? new Customer
            {
                Id = entity.Customer.Id,
                CustomerName = entity.Customer.CustomerName
            } : null
        };
    }

    public static void UpdateEntity(ProjectEntity entity, ProjectModel model)
    {
        entity.Title = model.Title;
        entity.Description = model.Description;
        entity.StartDate = model.StartDate;
        entity.EndDate = model.EndDate;
    }
}
