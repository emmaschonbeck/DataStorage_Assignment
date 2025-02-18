

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

    public static ProjectModel Create(ProjectEntity entity)
    {
        return new ProjectModel
        {
            Id = entity.Id,
            ProjectNumber = entity.ProjectNumber,
            Title = entity.Title,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate
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
