using BizStream.Migrations.Abstractions;
using BizStream.Migrations.Attributes;
using BizStream.Migrations.Options;
using BizStream.Migrations.Repositories;

using Spectre.Console;

namespace BizStream.Migrations.Migrators;

[Migrator(MigrationOptions.Articles)]
public class ArticlesMigrator : IMigrator
{
    private readonly ArticleRepository articleRepository;
    private readonly ArticleFolderRepository folderRepository;

    public ArticlesMigrator(
        ArticleRepository articleRepository,
        ArticleFolderRepository folderRepository)
    {
        this.articleRepository = articleRepository;
        this.folderRepository = folderRepository;
    }

    public void Migrate()
    {
        var folders = folderRepository.RetrieveAll();
        var articles = articleRepository.RetrieveAll();

        AnsiConsole.WriteLine($"Retrieved Article models from old site. Inserting into the new site...");

        folderRepository.InsertAll(folders);
        articleRepository.InsertAll(articles);
    }
}
