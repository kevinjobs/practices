using Api.Context;
using Api.IRepositories;
using Api.Models;
using System.Linq.Expressions;

namespace Api.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ArticleContext _context;

        public ArticleRepository(ArticleContext context)
        {
            _context = context;
        }

        public void Add(Article model)
        {
            _context.Articles.Add(model);
            _context.SaveChanges();
        }

        public void Delete(Article model)
        {
            _context.Articles.Remove(model);
            _context.SaveChanges();
        }

        public void Update(Article model)
        {
            _context.Articles.Update(model);
            _context.SaveChanges();
        }

        public List<Article> Query(Expression<Func<Article, bool>> whereExpression)
        {
            return _context.Articles.Where(whereExpression).ToList();
        }
    }
}
