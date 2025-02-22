using Business.Factories;
using Business.Models;
using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;

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

        var projectNumber = await GenerateProjectNumber();

        var projectEntity = new ProjectEntity(projectNumber)
        {
            Title = form.Title,
            Description = form.Description,
            StartDate = form.StartDate,
            EndDate = form.EndDate,
            CustomerId = form.CustomerId
        };

        await _projectRepository.AddAsync(projectEntity);
        return true;
    }

    public async Task<ProjectModel?> GetProjectByIdAsync(int id)
    {
        var projectEntity = await _projectRepository.GetAsync(p => p.Id == id);
        if (projectEntity == null) return null;

        Console.WriteLine($"Project Number: {projectEntity.ProjectNumber}");

        return ProjectFactory.Create(projectEntity);
    }


    public async Task<IEnumerable<ProjectModel>> GetAllProjectsAsync()
    {
        var projectEntities = await _projectRepository.GetAsync(include: q => q.Include(p => p.Customer));
        return projectEntities.Select(ProjectFactory.Create);
    }



    public async Task<bool> UpdateAsync(string projectNumber)
    {
        var projectEntity = await _projectRepository.GetAsync(p => p.ProjectNumber == projectNumber);
        if (projectEntity == null)
        {
            Console.WriteLine($"Project with Project Number {projectNumber} not found.");
            return false;
        }

        var originalTitle = projectEntity.Title;
        var originalDescription = projectEntity.Description;
        var originalStartDate = projectEntity.StartDate;
        var originalEndDate = projectEntity.EndDate;

        Console.WriteLine("Current project details:");
        Console.WriteLine($"Title: {originalTitle}");
        Console.WriteLine($"Description: {originalDescription ?? "No description available"}");
        Console.WriteLine($"Start Date: {originalStartDate:yyyy-MM-dd}");
        Console.WriteLine($"End Date: {originalEndDate:yyyy-MM-dd}");

        Console.Write("\nEnter new project title (required, leave blank to keep current): ");
        var newTitle = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(newTitle))
        {
            newTitle = originalTitle;
        }

        Console.Write("Enter new project description (optional, leave blank to keep current): ");
        var newDescription = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(newDescription))
        {
            newDescription = originalDescription;
        }

        Console.Write("Enter new project start date (yyyy-mm-dd, leave blank to keep current): ");
        var newStartDateInput = Console.ReadLine();
        DateTime newStartDate = originalStartDate;
        if (DateTime.TryParse(newStartDateInput, out var parsedStartDate))
        {
            newStartDate = parsedStartDate;
        }

        Console.Write("Enter new project end date (yyyy-mm-dd, leave blank to keep current): ");
        var newEndDateInput = Console.ReadLine();
        DateTime newEndDate = originalEndDate;
        if (DateTime.TryParse(newEndDateInput, out var parsedEndDate))
        {
            newEndDate = parsedEndDate;
        }

        Console.WriteLine("\nDo you want to save these changes? (y/n)");
        var confirm = Console.ReadLine();
        if (confirm?.ToLower() == "y")
        {

            projectEntity.Title = newTitle;
            projectEntity.Description = newDescription;
            projectEntity.StartDate = newStartDate;
            projectEntity.EndDate = newEndDate;

            await _projectRepository.UpdateAsync(projectEntity);
            return true;
        }
        else
        {
            Console.WriteLine("\nChanges were discarded.");
            return false;
        }
    }





    public async Task<bool> RemoveAsync(string projectNumber)
    {
        var projectEntity = await _projectRepository.GetAsync(p => p.ProjectNumber == projectNumber);
        if (projectEntity == null)
        {
            Console.WriteLine($"Project with Project Number {projectNumber} not found.");
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

