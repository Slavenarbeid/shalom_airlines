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
        TItem itemToRemove = listOfObjects.FirstOrDefault(obj => obj.ToString() == item.ToString());
        bool succes = listOfObjects.Remove(itemToRemove);
        
        SaveJsonFile(listOfObjects);

        return succes;
    }
    
    public void UpdateJson(TItem itemToUpdate, TItem newItem)
    {
        List<TItem> listOfObjects = LoadJson();
        
        TItem listItemToUpdate = listOfObjects.Find(obj => obj.ToString() == itemToUpdate.ToString());
        if (listItemToUpdate == null) return;
        var index = listOfObjects.IndexOf(listItemToUpdate);
        if(index != -1)
            listOfObjects[index] = newItem;
        
        SaveJsonFile(listOfObjects);
    }
    

    public List<TItem> LoadJson()
    {
        List<TItem> listOfObjects = new List<TItem>();
        if(File.Exists(JsonFileName)){
            using (StreamReader reader = new StreamReader(JsonFileName))
            {
                string file2Json = reader.ReadToEnd();
                listOfObjects = JsonConvert.DeserializeObject<List<TItem>>(file2Json) ?? new List<TItem>();
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