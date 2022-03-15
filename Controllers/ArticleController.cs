using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Entities;

namespace WebApplication3.Controllers;

[ApiController]
[Route("article")]
public class ArticleController : ControllerBase
{
    private DBContext _context;

    public ArticleController(DBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IList<Article> GetAll()
    {
        return _context.Articles
            .Include(a => a.Category)
            .ToList();
    }

    [HttpGet("{id:int}")]
    public ActionResult<Article> GetOne(int id)
    {
        var result = _context.Articles
            .Include(a => a.Category)
            .SingleOrDefault(a => a.ID == id);
        if (result is null)
            return NotFound();
        return Ok(result);
    }
    [HttpGet("test")]
    public IActionResult Test([FromQuery] int[] t)
    {
        Console.WriteLine(t);
        return Ok(t);
    }
}