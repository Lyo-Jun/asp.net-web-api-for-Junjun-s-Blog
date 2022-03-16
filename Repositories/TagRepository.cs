using WebApplication3.Entities;

namespace WebApplication3.Repositories;

public class TagRepository : ITagRepository
{
    private DBContext _context;
    public TagRepository(DBContext context) => _context = context;

    public bool Exists(int tagId)
    {
        return _context.Tags.Any(t => t.ID == tagId);
    }

    public Tag GetOne(int tagId)
    {
        return _context.Tags.Find(tagId);
    }

    public IList<Tag> GetAll()
    {
        return _context.Tags.ToList();
    }

    public IList<Tag> GetTagsOfAnArticle(int articleId)
    {
        var list = _context.Articles.Find(articleId)?.Tags;
        return list ?? new List<Tag>();
    }

    public void DeleteOne(int tagId)
    {
        var x = _context.Tags.Find(tagId);
        _context.Tags.Remove(x);
        _context.SaveChanges();
    }

    public Tag InsertOne(string name)
    {
        var x = _context.Tags.FirstOrDefault(t => t.Name == name);
        if (x == null)
        {
            var tag = new Tag()
            {
                Name = name
            };
            _context.Tags.Add(tag);
            _context.SaveChanges();
            return tag;
        }
        else return x;
        
    }
}