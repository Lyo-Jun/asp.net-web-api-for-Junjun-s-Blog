using System.Runtime.InteropServices.ComTypes;
using WebApplication3.Entities;

namespace WebApplication3.Repositories;

public class ArticleRepository : IArticleRepository
{
    private DBContext _context;
    public ArticleRepository(DBContext context) => _context = context;

    public IList<Article> GetAll()
    {
        return _context.Articles.ToList();
    }

    public Article GetOne(int articleId)
    {
        return _context.Articles.Find(articleId);
    }

    public IList<Article> GetArticlesOfATag(int tagId)
    {
        var list = _context.Tags.Find(tagId)?.Articles;
        return list ?? new List<Article>();
    }

    public IList<Article> GetArticlesOfACategory(int catId)
    {
        var list = _context.Categories.Find(catId)
            ?.Articles;
        return list ?? new List<Article>();
    }

    public Article InsertOne(int catId, IList<int> tagIds, Article article)
    {
        article.Category = _context.Categories.Find(catId);
        article.Tags = _context.Tags.Where(t => tagIds.Contains(t.ID)).ToList();
        _context.Articles.Add(article);
        _context.SaveChanges();
        return article;
    }

    public Article UpdateOne(int catId, IList<int> tagIds, Article article)
    {
        article.Category = _context.Categories.Find(catId);
        article.Tags = _context.Tags.Where(t => tagIds.Contains(t.ID)).ToList();
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
}