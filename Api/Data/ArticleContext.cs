using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.Data;

public class ArticleContext : DbContext
{
    public ArticleContext (DbContextOptions<ArticleContext> options)
        : base(options)
    {

    }

    public DbSet<Article> Articles => Set<Article>();
    // public DbSet<Topping> Toppings => Set<Topping>();
    // public DbSet<Sauce> Sauces => Set<Sauce>();
}