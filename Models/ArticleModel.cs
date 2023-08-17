namespace BizStream.Migrations.Models;

public class ArticleModel : TreeNodeModel
{
    required public string ArticleTeaserText { get; set; }
    required public string ArticleTeaserImage { get; set; }
    required public string ArticleText { get; set; }
}

