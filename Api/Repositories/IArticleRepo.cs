using Api.Models;
using System.Linq.Expressions;

namespace Api.IRepositories;

public interface IArticleRepository
{
    void Add(Article model);
    void Delete(Article model);
    void Update(Article model);
    List<Article> Query(Expression<Func<Article, bool>> whereExpression);
}