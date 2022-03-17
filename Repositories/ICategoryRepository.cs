using WebApplication3.Entities;

namespace WebApplication3.Repositories;

public interface ICategoryRepository
{
    bool Exists(int catId);
    Category GetOne(int catId);
    Category GetCategoryByArticle(int articleId);
    IList<Category> GetAll();
    void DeleteOne(int catId);
    Category InsertOne(Category category);
    Category UpdateOne(Category category);
}