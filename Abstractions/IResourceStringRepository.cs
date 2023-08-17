using BizStream.Migrations.Models;

namespace BizStream.Migrations.Abstractions;
public interface IResourceStringRepository
{
    public void Insert( ResourceStringModel resourceString );
}
