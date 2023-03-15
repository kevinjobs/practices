using Api.Models;

namespace Api.Services
{
    public static class ArticleService
    {
        static List<Article> Articles { get; }
        static int nextId = 3;
        static ArticleService()
        {
            Articles = new List<Article>
            {
                new Article
                {
                    Id = 1,
                    Title = "Test",
                    Description = "Test Article",
                    Content = "<p>This a test article</p>",
                    Category = "test",
                    Tags = new List<string>
                    {
                        "test",
                        "normal",
                        "single"
                    },
                    Author = "testauthor",
                    AuthorId = 1,
                    AuthorName = "test author name"
                },
                new Article
                {
                    Id = 2,
                    Title = "Test2",
                    Description = "Test Article",
                    Content = "<p>This a test article</p>",
                    Category = "test",
                    Tags = new List<string>
                    {
                        "test",
                        "normal",
                        "single"
                    },
                    Author = "testauthor",
                    AuthorId = 1,
                    AuthorName = "test author name"
                }
            };
        }

        public static List<Article> GetAll() => Articles;

        public static Article Get(int id) => Articles.FirstOrDefault(x => x.Id == id);

        public static void Add(Article article)
        {
            article.Id = nextId++;
            Articles.Add(article);
        }

        public static void Delete(int id)
        {
            var article = Get(id);
            if (article is null)
                return;
            Articles.Remove(article);
        }

        public static void Update(Article article)
        {
            var index = Articles.FindIndex(p => p.Id == article.Id);
            if (index == -1)
                return;
            Articles[index] = article;
        }
    }
}