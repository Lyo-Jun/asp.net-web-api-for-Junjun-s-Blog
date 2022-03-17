using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Dtos;
using WebApplication3.Entities;
using WebApplication3.Repositories;

namespace WebApplication3.Controllers;

[ApiController]
[Route("category")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;
    private readonly IArticleRepository _articleRepository;

    public CategoryController(ICategoryRepository categoryRepository,
        IMapper mapper,
        IArticleRepository articleRepository)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _articleRepository = articleRepository;
    }


    [HttpGet]
    public IActionResult GetAll()
    {
        var Categories = _categoryRepository.GetAll();
        var dtos = _mapper.Map<List<CategoryDto>>(Categories);
        return Ok(dtos);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetOne([FromRoute] int id)
    {
        var cat = _categoryRepository.GetOne(id);
        var result = _mapper.Map<CategoryDto>(cat);
        return Ok(result);
    }

    [HttpGet("{id:int}/articles")]
    public IActionResult GetArticlesByCat([FromRoute] int id)
    {
        var articles = _articleRepository.GetArticlesOfACategory(id);
        var result = _mapper.Map<List<ArticleDto>>(articles);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteOne([FromRoute] int id)
    {
        _categoryRepository.DeleteOne(id);
        return Ok();
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateOne(
        [FromRoute] int id,
        [FromBody] CategoryDto category,
        [FromQuery] int[]? articles
    )
    {
        if (id != category.ID)
            return BadRequest();
        var result = _mapper.Map<Category>(category);
        if (articles != null)
        {
            var list = articles.ToList();
            result.Articles = _articleRepository.GetAll()
                .Where(a => list.Contains(a.ID))
                .ToList();
        }

        var updated = _categoryRepository.UpdateOne(result);
        var dto = _mapper.Map<CategoryDto>(updated);
        return Ok(dto);
    }

    [HttpPost]
    public IActionResult CreateOne([FromBody] CategoryDto category)
    {
        var result = _mapper.Map<Category>(category);
        var inserted = _mapper.Map<CategoryDto>(_categoryRepository.InsertOne(result));

        return Ok(inserted);
    }
}