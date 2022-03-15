using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebApplication3.Entities;

public class Category
{
    [Key] public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Article> Articles { get; set; } = new List<Article>();
}