using Business.Factories;
using Business.Models;
using Data.Entities;
using Data.Repositories;

namespace Business.Services;

public class ProjectService(ProjectRepository projectRepository)
{
    private readonly ProjectRepository _projectRepository = projectRepository;

    public async Task<bool> CreateProjectAsync(ProjectRegistrationForm form)
    {
        var existingProject = await _projectRepository.GetAsync(p => p.Title == form.Title);
        if (existingProject != null)
        {
            return false;
        }

        // Generera ProjectNumber
        var projectNumber = await GenerateProjectNumber();

        // Använd den parameteriserade konstruktorn för att skapa ett nytt ProjectEntity
        var projectEntity = new ProjectEntity(projectNumber)
        {
            Title = form.Title,
            Description = form.Description,
            StartDate = form.StartDate,
            EndDate = form.EndDate,
            CustomerId = form.CustomerId,
            StatusId = form.StatusId
        };

        await _projectRepository.AddAsync(projectEntity);
        return true;
    }

    public async Task<ProjectModel?> GetProjectByIdAsync(int id)
    {
        var projectEntity = await _projectRepository.GetAsync(p => p.Id == id);
        if (projectEntity == null) return null;

        // Visa ProjectNumber (inte ändra det)
        Console.WriteLine($"Project Number: {projectEntity.ProjectNumber}");

        return ProjectFactory.Create(projectEntity);
    }


    public async Task<IEnumerable<ProjectModel>> GetAllProjectsAsync()
    {
        var projectEntities = await _projectRepository.GetAsync();
        return projectEntities.Select(ProjectFactory.Create);
    }


    public async Task<bool> UpdateAsync(ProjectModel project)
    {
        var existingProject = await _projectRepository.GetAsync(p => p.Id == project.Id);
        if (existingProject == null)
        {
            return false;
        }

        ProjectFactory.UpdateEntity(existingProject, project);
        await _projectRepository.UpdateAsync(existingProject);
        return true;
    }


    public async Task<bool> RemoveAsync(int id)
    {
        var projectEntity = await _projectRepository.GetAsync(p => p.Id == id);
        if (projectEntity == null)
        {
            return false;
        }

        await _projectRepository.RemoveAsync(projectEntity);
        return true;
    }

    private async Task<string> GenerateProjectNumber()
    {
        var projectCount = await _projectRepository.CountAsync();
        return $"P-{100 + projectCount + 1}"; // Ex: P-101, P-102 osv.
    }
}

