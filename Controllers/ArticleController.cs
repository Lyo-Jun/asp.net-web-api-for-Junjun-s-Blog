using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Entities;
using WebApplication3.Repositories;

namespace WebApplication3.Controllers;

[ApiController]
[Route("article")]
public class ArticleController : ControllerBase
{
    private readonly IArticleRepository _articleRepository;
    private readonly ITagRepository _tagRepository;


    public ArticleController(IArticleRepository articleRepository,
        ITagRepository tagRepository)
    {
        _articleRepository = articleRepository;
        _tagRepository = tagRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_articleRepository.GetAll());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetOne([FromRoute] int id)
    {
        return Ok(_articleRepository.GetOne(id));
    }

    [HttpGet]
    public IActionResult GetArticlesByCategory([FromQuery] int catId)
    {
        return Ok(_articleRepository.GetArticlesOfACategory(catId));
    }

    [HttpGet]
    public IActionResult GetArticlesByTag([FromQuery] int tagId)
    {
        return Ok(_articleRepository.GetArticlesOfATag(tagId));
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteByID([FromRoute] int id)
    {
        _articleRepository.DeleteOne(id);
        return Ok();
    }
    


    [HttpGet("test")]
    public IActionResult Test([FromQuery] int[] t)
    {
        Console.WriteLine(t);
        return Ok(t);
    }
}