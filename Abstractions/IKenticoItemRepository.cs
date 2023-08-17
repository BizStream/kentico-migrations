using BizStream.Migrations.Models;
using CMS.DataEngine;

namespace BizStream.Migrations.Abstractions;
public interface IKenticoItemRepository<TModel, TKenticoItem>
    where TModel : KenticoItemModel
    where TKenticoItem : IInfo
{
    public void Insert( TModel model );
}
