using Api.IServices;
using Api.Models;
using Api.IRepositories;
using System.Linq.Expressions;

namespace Api.Services;

public class ArticleService : IArticleService
{
    private readonly IArticleRepository _dal;

    public ArticleService(IArticleRepository dal)
    {
        _dal = dal;
    }

    public void Add(Article model)
    {
        _dal.Add(model);
    }

    public void Delete(Article model)
    {
        _dal.Delete(model);
    }

    public void Update(Article model)
    {
        _dal.Update(model);
    }

    public List<Article> Query(Expression<Func<Article, bool>> expression)
    {
        return _dal.Query(expression);
    }
}