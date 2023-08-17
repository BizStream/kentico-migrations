namespace BizStream.Migrations.Models;
public class ResourceStringModel
{
    required public string StringKey { get; set; }
    required public string TranslationText { get; set; }
    required public string CultureCode { get; set; }
    required public bool StringIsCustom { get; set; }
}