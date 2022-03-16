using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Dtos;
using WebApplication3.Entities;
using WebApplication3.Repositories;

namespace WebApplication3.Controllers;

[ApiController]
[Route("article")]
public class ArticleController : ControllerBase
{
    private readonly IArticleRepository _articleRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IMapper _mapper;
    private readonly ICategoryRepository _categoryRepository;

    public ArticleController(IArticleRepository articleRepository,
        ITagRepository tagRepository,
        ICategoryRepository categoryRepository,
        IMapper mapper)
    {
        _articleRepository = articleRepository;
        _tagRepository = tagRepository;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_mapper.Map<List<ArticleDto>>(_articleRepository.GetAll()));
    }

    [HttpGet("{id:int}")]
    public IActionResult GetOne([FromRoute] int id)
    {
        return Ok(_mapper.Map<ArticleDto>(_articleRepository.GetOne(id)));
    }

    [HttpGet("{id:int}/tags")]
    public IActionResult GetTagsOfAnArticle([FromRoute] int id)
    {
        var result = _tagRepository.GetTagsOfAnArticle(id);
        return Ok(
            _mapper.Map<List<TagDto>>(result)
        );
    }

    [HttpGet("{id:int}/category")]
    public IActionResult GetCategoryByArticle([FromRoute] int id)
    {
        var result = _categoryRepository.GetCategoryByArticle(id);
        return Ok(
            _mapper.Map<CategoryDto>(result)
        );
    }


    [HttpDelete("{id:int}")]
    public IActionResult DeleteByID([FromRoute] int id)
    {
        _articleRepository.DeleteOne(id);
        return Ok();
    }

    [HttpPut("{id:ind}")]
    public IActionResult UpdateOne([FromRoute] int id
        , [FromBody] ArticleDto article,
        [FromQuery] int catId = -1,
        [FromQuery] int[]? tags = null)
    {
        var temp = _mapper.Map<Article>(article);
        if (catId >= 0)
            temp.Category = _categoryRepository.GetOne(catId);
        if (tags != null)
        {
            var list = tags.ToList();
            temp.Tags = _tagRepository.GetAll()
                .Where(t => list.Contains(t.ID))
                .ToList();
        }

        return Ok(
            _articleRepository.UpdateOne(temp)
        );
    }
    


    [HttpGet("test")]
    public IActionResult Test([FromQuery] int[] t)
    {
        Console.WriteLine(t);
        return Ok(t);
    }
}