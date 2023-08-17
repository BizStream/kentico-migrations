namespace BizStream.Migrations.Models;
public class SettingsCategoryModel
{
    required public string CategoryName { get; set; }
    required public string CategoryDisplayName { get; set; }
    required public string CategoryIconPath { get; set; } = string.Empty;
    required public bool CategoryIsGroup { get; set; }
    required public bool CategoryIsCustom { get; set; }
    required public int CategoryParentID { get; set; }
    required public int CategoryResourceID { get; set; }
}
