using Api.Models;
using Api.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ArticleController : ControllerBase
{
    private readonly IArticleService _service;

    public ArticleController(IArticleService service)
    {
        _service = service;
    }

    ///<summary>
    /// get article by id
    ///</summary>
    ///<param name="id"></param>
    ///<returns></returns>
    [HttpGet("{id}", Name = "Get")]
    public List<Article> Get(int id)
    {
        return _service.Query(d => d.Id == id);
    }
}