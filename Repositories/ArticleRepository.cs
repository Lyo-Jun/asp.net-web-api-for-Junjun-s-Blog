using System.Runtime.InteropServices.ComTypes;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Entities;

namespace WebApplication3.Repositories;

public class ArticleRepository : IArticleRepository
{
    private DBContext _context;
    public ArticleRepository(DBContext context) => _context = context;

    public IList<Article> GetAll()
    {
        return _context.Articles
            .AsNoTracking()
            .ToList();
    }

    public Article GetOne(int articleId)
    {
        return _context.Articles.Find(articleId);
    }

    public IList<Article> GetArticlesOfATag(int tagId)
    {
        var list = _context.Tags.Find(tagId);
        if (list == null)
            return new List<Article>();
        _context.Entry(list).Collection(t => t.Articles)
            .Load();
        return list.Articles;
    }

    public IList<Article> GetArticlesOfACategory(int catId)
    {
        var cat = _context.Categories
            .Include(c => c.Articles)
            .FirstOrDefault(c => c.ID == catId);
        return cat?.Articles ?? new List<Article>();
    }

    public Article InsertOne(Article article)
    {
        _context.Articles.Add(article);
        _context.SaveChanges();
        return article;
    }

    public Article UpdateOne(Article article)
    {
        _context.Articles.Update(article);
        _context.SaveChanges();
        return article;
    }


    public void DeleteOne(int id)
    {
        var x = _context.Articles.Find(id);
        if (x == null)
            return;
        _context.Articles.Remove(x);
        _context.SaveChanges();
    }

    public bool Exists(int articleId)
    {
        return _context.Articles.Any(a => a.ID == articleId);
    }

    public void RemoveAllTags(int id)
    {
        _context.Database
            .ExecuteSqlRaw(@"
        DELETE FROM ArticleTag
        WHERE ArticlesID={0}
        ", id);
    }
}