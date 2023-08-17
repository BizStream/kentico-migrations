namespace BizStream.Migrations.Options;
public static class MigrationOptions
{
    public const string ResourceStrings = "Resource Strings";
    public const string Settings = "Settings";
    public const string Articles = "Articles";

    public static IEnumerable<string> All
        => typeof( MigrationOptions )
            .GetFields()
            .Where( f => f.FieldType == typeof( string ) )
            .Select( f => (string)f.GetValue( null ) ?? string.Empty )
            .Where( f => !string.IsNullOrWhiteSpace( f ) )
            .ToList() ?? Enumerable.Empty<string>();
}
