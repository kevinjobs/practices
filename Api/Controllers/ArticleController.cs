using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {
        private ArticleService _service;

        public ArticleController (ArticleService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<Article>> GetAll()
        {
            return _service.FindAll();
        }

        [HttpGet("{id}")]
        public ActionResult<Article> Get(int id)
        {
            var article = _service.FindById(id);

            if(article == null)
            {
                return NotFound();
            }

            return article;
        }

        [HttpPost]
        public IActionResult Create(Article article)
        {
            _service.Add(article);
            return CreatedAtAction(nameof(Get), new { id = article.Id }, article);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Article article)
        {
            if (id != article.Id)
            {
                return BadRequest();
            }

            var existed = _service.FindById(id);
            if (existed is null)
            {
                return NotFound();
            }

            _service.Update(article);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var article = _service.FindById(id);
            if (article is null)
                return NotFound();

            _service.Delete(id);

            return NoContent();
        }
    }
}