using WebApplication3.Entities;

namespace WebApplication3.Repositories;

public interface ITagRepository
{
    bool Exists(int tagId);
    Tag GetOne(int tagId);
    IList<Tag> GetAll();
    IList<Tag> GetTagsOfAnArticle(int articleId);
    void DeleteOne(int tagId);
    Tag InsertOne(string name);
}