using System.ComponentModel.DataAnnotations;

namespace BizStream.Migrations.Options;
public class ExportOptions
{
    [Required]
    public required string ExportCMSConnectionString { get; set; }

    //TODO: Add boolean fields for which content to migrate
}
