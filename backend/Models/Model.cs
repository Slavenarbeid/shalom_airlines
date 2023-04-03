using backend.Controllers;
using PluralizeService.Core;

namespace backend.Models;

public abstract class Model<TModel>
{
    public static List<TModel> All()
    {
        var pluralName = PluralizationProvider.Pluralize(typeof(TModel).Name);
        JsonHandle<TModel> jsonHandle = new(pluralName);
        return jsonHandle.LoadJson();
    }
}