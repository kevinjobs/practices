using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.Context;

public class ArticleContext : DbContext
{
    public ArticleContext(DbContextOptions<ArticleContext> options)
        : base(options)
    {

    }

    public DbSet<Article> Articles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>().Property(p => p.Title).IsRequired();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(@"Server=.;Database=Article;Trusted_Connection=True;Connection Timeout=600;MultipleActiveResultSets=true;")
            .LogTo(Console.WriteLine, LogLevel.Information);
    }
}