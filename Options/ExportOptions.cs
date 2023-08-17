using System.ComponentModel.DataAnnotations;

namespace BizStream.Migrations.Options;
public class ExportOptions
{
    [Required]
    public required string ExportCMSConnectionString { get; set; }
}
