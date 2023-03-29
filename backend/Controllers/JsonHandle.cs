namespace backend.Controllers;
using Newtonsoft.Json;

public class JsonHandle<TItem>
{
    public string JsonFileName;

    public JsonHandle(string jsonFileName)
    {
        JsonFileName = @"../../../../backend/Data/" + jsonFileName + ".json";
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
        
        bool succes = listOfObjects.Remove(item);
        
        SaveJsonFile(listOfObjects);

        return succes;
    }

    public List<TItem> LoadJson()
    {
        List<TItem> listOfObjects = new List<TItem>();
        if(File.Exists(JsonFileName)){
            using (StreamReader reader = new StreamReader(JsonFileName))
            {
                string file2Json = reader.ReadToEnd();
                listOfObjects = JsonConvert.DeserializeObject<List<TItem>>(file2Json);
            }
        }
        return listOfObjects;
    }

    public void SaveJsonFile(List<TItem> listOfObjects)
    {
        using (StreamWriter writer = new StreamWriter(JsonFileName))
        {
            string list2Json = JsonConvert.SerializeObject(listOfObjects);
            writer.Write(list2Json);
        }
    }
}