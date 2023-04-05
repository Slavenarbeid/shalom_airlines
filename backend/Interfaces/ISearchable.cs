using backend.Models;

namespace backend.Interfaces;

public interface ISearchable
{
    public IEnumerable<TModel> Search<TModel>() where TModel : Model<TModel>
    {
        TModel.All();
    }
}