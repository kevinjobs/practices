using Api.Models;
using Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class ArticleService
    {
        private readonly ArticleContext _context;
        public ArticleService(ArticleContext context)
        {
            _context = context;
        }

        public List<Article> FindAll()
        {
            return _context.Articles
                .AsNoTracking()
                .ToList();
        }

        public Article? FindById(int id)
        {
            return _context.Articles
                //.Include()
                .AsNoTracking()
                .SingleOrDefault(p => p.Id == id);
        }

        public Article Add(Article article)
        {
            _context.Articles.Add(article);
            _context.SaveChanges();

            return article;
        }

        public void Delete(int id)
        {
            var articleToDelete = _context.Articles.Find(id);
            if (articleToDelete is null)
            {
                throw new NullReferenceException("No this article");
            }

            _context.Articles.Remove(articleToDelete);
            _context.SaveChanges();
        }

        public void Update(Article article)
        {
            //
        }
    }
}