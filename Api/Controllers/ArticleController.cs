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

        [HttpPost]
        public IActionResult Create(Article article)
        {
            ArticleService.Add(article);
            return CreatedAtAction(nameof(Get), new { id = article.Id }, article);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Article article)
        {
            if (id != article.Id)
            {
                return BadRequest();
            }

            var existed = ArticleService.Get(id);
            if (existed is null)
            {
                return NotFound();
            }

            ArticleService.Update(article);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var article = ArticleService.Get(id);
            if (article is null)
                return NotFound();

            ArticleService.Delete(id);

            return NoContent();
        }
    }
}