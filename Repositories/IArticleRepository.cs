using WebApplication3.Entities;

namespace WebApplication3.Repositories;

public interface IArticleRepository
{
    IList<Article> GetAll();
    Article GetOne(int articleId);

    IList<Article> GetArticlesOfATag(int tagId);
    IList<Article> GetArticlesOfACategory(int catId);
    Article InsertOne( Article article);
    Article UpdateOne(Article article);
    void DeleteOne(int id);
    bool Exists(int articleId);
}