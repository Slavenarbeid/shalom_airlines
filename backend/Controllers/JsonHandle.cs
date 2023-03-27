namespace backend.Controllers;
using Newtonsoft.Json;

public class JsonHandle<TItem>
{
    public string JsonFileName;

    public JsonHandle(string jsonFileName)
    {
        JsonFileName = @"../../../../backend/Data" + jsonFileName;
    }

    public void SaveToJson(TItem item)
    {
        List<TItem> listOfObjects = new List<TItem>();
        if(File.Exists(JsonFileName)){
            using (StreamReader reader = new StreamReader(JsonFileName))
            {
                string file2Json = reader.ReadToEnd();
                listOfObjects = JsonConvert.DeserializeObject<List<TItem>>(file2Json);
            }
        }
        
        listOfObjects.Add(item);
        
        using (StreamWriter writer = new StreamWriter(JsonFileName))
        {
            string list2Json = JsonConvert.SerializeObject(listOfObjects);
            writer.Write(list2Json);
        }
    }
}