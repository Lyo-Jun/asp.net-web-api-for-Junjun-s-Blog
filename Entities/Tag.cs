using System.ComponentModel.DataAnnotations;

namespace WebApplication3.Entities;

public class Tag
{
    [Key] public int ID { get; set; }

    public string Name { get; set; }

    public IList<Article> Articles { get; set; } = new List<Article>();
    
}