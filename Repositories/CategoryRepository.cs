using WebApplication3.Entities;

namespace WebApplication3.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private DBContext _context;
    public CategoryRepository(DBContext context) => _context = context;

    public bool Exists(int catId)
    {
        return _context.Categories.Any(c => c.ID == catId);
    }

    public Category GetOne(int catId)
    {
        return _context.Categories.Find(catId);
    }

    public Category GetCategoryByArticle(int articleId)
    {
        return _context.Articles.Find(articleId)?.Category;
    }

    public IList<Category> GetAll()
    {
        return _context.Categories.ToList();
    }

    public void DeleteOne(int catId)
    {
        var x = _context.Categories.Find(catId);
        if (x != null)
        {
            _context.Categories.Remove(x);
            _context.SaveChanges();
        }
    }

    public Category InsertOne(Category category)
    {
        var x = _context.Categories.FirstOrDefault(c =>
            c.Name.ToLower() == category.Name);
        if (x != null)
            return x;
        _context.Categories.Add(category);
        _context.SaveChanges();
        return category;
    }

    public Category UpdateOne(Category category, IList<int>? articleIds)
    {
        var list = _context.Articles.Where(a => articleIds.Contains(a.ID))
            .ToList();
        category.Articles = list;
        _context.Categories.Update(category);
        _context.SaveChanges();
        return category;
    }
}