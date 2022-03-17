using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Dtos;
using WebApplication3.Entities;
using WebApplication3.Repositories;

namespace WebApplication3.Controllers;

[ApiController]
[Route("tag")]
public class TagController : ControllerBase
{
    private readonly ITagRepository _tagRepository;
    private readonly IMapper _mapper;
    private readonly IArticleRepository _articleRepository;

    public TagController(
        ITagRepository tagRepository,
        IMapper mapper,
        IArticleRepository articleRepository
    )
    {
        _tagRepository = tagRepository;
        _mapper = mapper;
        _articleRepository = articleRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _mapper
            .Map<List<TagDto>>(_tagRepository.GetAll());
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetOne([FromRoute] int id)
    {
        var result = _mapper.Map<TagDto>(_tagRepository.GetOne(id));
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteOne([FromRoute] int id)
    {
        _tagRepository.DeleteOne(id);
        return Ok();
    }

    [HttpPost]
    public IActionResult InsertOne([FromQuery] string name)
    {
        _tagRepository.InsertOne(name);
        return Ok();
    }

    [HttpGet("{id:int}/articles")]
    public IActionResult GetArticlesWithATag([FromRoute] int id)
    {
        var result = _mapper
            .Map<List<ArticleDto>>(
                _articleRepository
                    .GetArticlesOfATag(id)
            );
        return Ok(result);
    }
}