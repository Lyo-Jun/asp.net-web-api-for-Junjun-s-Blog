namespace WebApplication3.Dtos;

public class ArticleDto
{
    public int ID { get; set; }

    public string Name { get; set; }

    public string Markdown { get; set; }
    public bool IsPublished { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime LastModifiedTime { get; set; }

    public string Description { get; set; }
}