using WebApplication3.Dtos;
using WebApplication3.Entities;

namespace WebApplication3.Helper;

public static class ArticleExtensions
{
    public static Article ChangeSelf(this Article article, ArticleDto dto)
    {
        article.Name = dto.Name;
        article.Description = dto.Description;
        article.Markdown = dto.Markdown;
        article.IsPublished = dto.IsPublished;
        article.LastModifiedTime = DateTime.Now;
        return article;
    }
}