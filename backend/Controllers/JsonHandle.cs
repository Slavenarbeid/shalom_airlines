namespace backend.Controllers;

using Newtonsoft.Json;

public class JsonHandle<TItem>
{
    private readonly string _jsonFileName;

    private readonly string _debugDataPath = "../../../../backend/Data/";
    private readonly string _releaseDataPath = "Data/";

    public JsonHandle(string jsonFileName)
    {
#if DEBUG
        Directory.CreateDirectory(_debugDataPath);
        _jsonFileName = $"{_debugDataPath}{jsonFileName}.json";
#else
        Directory.CreateDirectory(_releaseDataPath);
        _jsonFileName = $"{_releaseDataPath}{jsonFileName}.json";
#endif
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

        var type = itemToUpdate.GetType();

        TItem listItemToUpdate;
        if (type.GetMethod("Equals") != null)
        {
            listItemToUpdate = listOfObjects.Find(obj => obj.Equals(newItem));
        }
        else
        {
            listItemToUpdate = listOfObjects.Find(obj => obj.ToString() == itemToUpdate.ToString());
        }

        if (listItemToUpdate == null) return;
        var index = listOfObjects.IndexOf(listItemToUpdate);
        if (index != -1)
            listOfObjects[index] = newItem;

        SaveJsonFile(listOfObjects);
    }


    public List<TItem> LoadJson()
    {
        List<TItem> listOfObjects = new List<TItem>();
        if (File.Exists(_jsonFileName))
        {
            using (StreamReader reader = new StreamReader(_jsonFileName))
            {
                string file2Json = reader.ReadToEnd();
                listOfObjects = JsonConvert.DeserializeObject<List<TItem>>(file2Json) ?? new List<TItem>();
            }
        }

        return listOfObjects;
    }

    public void SaveJsonFile(List<TItem> listOfObjects)
    {
        using (StreamWriter writer = new StreamWriter(_jsonFileName))
        {
            string list2Json = JsonConvert.SerializeObject(listOfObjects);
            writer.Write(list2Json);
        }
    }
}