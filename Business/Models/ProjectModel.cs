

using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Models;

public class ProjectModel
{
    public int Id { get; set; }
    public string ProjectNumber { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string? Description { get; set; }

    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }

    [Column(TypeName = "date")]
    public DateTime EndDate { get; set; }
}
