namespace BizStream.Migrations.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class MigratorAttribute : Attribute
{
    public string MigratorName { get; private set; }

    public MigratorAttribute(string name)
        => MigratorName = name;
}
