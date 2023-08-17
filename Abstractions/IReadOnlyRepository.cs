namespace BizStream.Migrations.Abstractions;

// TODO: Update this to not be ReadOnly repository, and to have another generic Kentico type representing what we're inserting
// Then, add a new InsertAll(IEnumerable<TKenticoType>) method that uses that type or something
// Have an Insert() method as well for individual items, and have it return the item it's inserting 
// Model the insert methods like "SettingsCategoryRepository"'s insert method

/// <summary>
/// Repository interface used only when retrieving data from a source. 
/// </summary>
/// <typeparam name="TEntity">A model from the "./Models" folder, typically.</typeparam>
public interface IReadOnlyRepository<TEntity>
    where TEntity : class
{
    public IEnumerable<TEntity> RetrieveAll();
}
