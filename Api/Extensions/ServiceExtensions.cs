using Api.Services;
using Api.IServices;
using Autofac;

namespace Api.Extensions.ServiceExtensions;

public class AutofacModuleRegister : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ArticleService>().As<IArticleService>();
    }
}