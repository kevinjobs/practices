using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<Article>> GetAll()
        {
            return ArticleService.GetAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Article> Get(int id)
        {
            var article = ArticleService.Get(id);

            if(article == null)
            {
                return NotFound();
            }

            return article;
        }
    }
}