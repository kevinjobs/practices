using Api.Models;

namespace Api.IServices;

public interface IArticleService
{
    public List<Article> FindAll();

    public Article? FindById(int id);

    public Article? Add(Article article);

    public void Update(Article article);

    public void DeleteById(int id);
}