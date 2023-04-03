using backend.Controllers;

namespace backend.Models;

public abstract class Model<T>
{
    public static List<T> All()
    {
        var a = typeof(T).ToString();
        JsonHandle<T> jsonHandle = new(typeof(T).ToString());
        return jsonHandle.LoadJson();
    }
}