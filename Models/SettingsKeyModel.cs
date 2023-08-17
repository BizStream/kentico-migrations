using CMS.DataEngine;

namespace BizStream.Migrations.Models;
public class SettingsKeyModel
{
    required public string KeyName { get; set; }
    required public string KeyValue { get; set; }
    required public string KeyDisplayName { get; set; }
    required public string KeyType { get; set; }
    required public string KeyDescription { get; set; }
    required public string KeyExplanationText { get; set; }
    required public string CategoryName { get; set; }
}
