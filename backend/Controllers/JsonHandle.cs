namespace backend.Controllers;

using Newtonsoft.Json;

public class JsonHandle<TItem>
{
    private readonly string _jsonFileName;

    public JsonHandle(string jsonFileName)
    {
        _jsonFileName = @"../../../../backend/Data/" + jsonFileName + ".json";
    }

    public void AddToJson(TItem item)
    {
        List<TItem> listOfObjects = LoadJson();

        listOfObjects.Add(item);

        SaveJsonFile(listOfObjects);
    }

    public bool RemoveFromJson(TItem item)
    {
        List<TItem> listOfObjects = LoadJson();
        TItem itemToRemove = listOfObjects.FirstOrDefault(obj => obj.ToString() == item.ToString());
        bool success = listOfObjects.Remove(itemToRemove!);

        SaveJsonFile(listOfObjects);

        return success;
    }

    public void UpdateJson(TItem itemToUpdate, TItem newItem)
    {
        List<TItem> listOfObjects = LoadJson();

        TItem listItemToUpdate = listOfObjects.Find(obj => obj.ToString() == itemToUpdate.ToString());
        if (listItemToUpdate == null) return;
        var index = listOfObjects.IndexOf(listItemToUpdate);
        if (index != -1)
            listOfObjects[index] = newItem;

        SaveJsonFile(listOfObjects);
    }


    public List<TItem> LoadJson()
    {
        List<TItem> listOfObjects = new List<TItem>();
        if (!File.Exists(_jsonFileName)) return listOfObjects;

        using StreamReader reader = new StreamReader(_jsonFileName);
        string file2Json = reader.ReadToEnd();
        return JsonConvert.DeserializeObject<List<TItem>>(file2Json) ?? new List<TItem>();
    }

    public void SaveJsonFile(List<TItem> listOfObjects)
    {
        using StreamWriter writer = new StreamWriter(_jsonFileName);
        string list2Json = JsonConvert.SerializeObject(listOfObjects);
        writer.Write(list2Json);
    }
}