using System.Linq.Expressions;
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


    public static List<TModel> Search(Dictionary<string, object> filters)
    {
        var models = All();
        if (!models.Any()) return models;

        var query = models.AsQueryable();
        var param = Expression.Parameter(typeof(TModel), "model");
        BinaryExpression? binaryFilter = null;

        foreach (var filter in filters)
        {
            var memberExpression = Expression.PropertyOrField(param, filter.Key);
            var memberExpressionString = Expression.Call(memberExpression, typeof(object).GetMethod("ToString")!);
            var constantExpression = Expression.Constant(filter.Value.ToString()?.ToLower());
            var binaryExpression = Expression.Equal(
                Expression.Call(
                    memberExpressionString, 
                    "Contains",
                    null, constantExpression),
                Expression.Constant(true));
            binaryFilter = binaryFilter != null ? Expression.And(binaryFilter, binaryExpression) : binaryExpression;
        }

        if (binaryFilter == null) return query.ToList();
        var lambda = Expression.Lambda<Func<TModel, bool>>(binaryFilter, param);
        return query.Where(lambda).ToList();
    }

    public static string CreateUUID()
    {
        Guid guid = Guid.NewGuid();
        string UUID = guid.ToString();

        return UUID;
    }
}