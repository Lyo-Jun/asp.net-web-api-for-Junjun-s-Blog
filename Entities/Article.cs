using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Article
{
    [Key] public int ID { get; set; }

    public string Name { get; set; }
    [Unicode] public string Markdown { get; set; }
    public bool IsPublished { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime CreatedTime { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime LastModifiedTime { get; set; }

    public string Description { get; set; }
    public Category Category { get; set; }
    public List<Tag> Tags { get; set; } = new List<Tag>();
}