using System.Data.SqlClient;
using BizStream.Migrations.Options;
using Dapper;
using Microsoft.Extensions.Options;

namespace BizStream.Migrations.Abstractions;


/// <summary>
/// Repository used only when retrieving data from a static SQL query.
/// Define the SQL Connection String in appsettings.json.
/// </summary>
/// <typeparam name="TEntity">The model returned from the SQL call by Dapper.</typeparam>
public abstract class SqlRepository<TEntity> : IReadOnlyRepository<TEntity>, ISqlRepository
    where TEntity : class
{
    protected virtual string SqlQuery { get; set; }

    private readonly IOptions<ExportOptions> exportOptions;

    public SqlRepository( IOptions<ExportOptions> exportOptions )
    {
        this.exportOptions = exportOptions;
    }

    public IEnumerable<TEntity> RetrieveAll()
    {
        using (var connection = new SqlConnection( exportOptions.Value.ExportCMSConnectionString ))
        {
            var model = connection.Query<TEntity>( SqlQuery );

            return model;
        };
    }
}

