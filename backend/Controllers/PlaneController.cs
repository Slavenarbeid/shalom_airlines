using backend.Models;

namespace backend.Controllers;

public static class PlaneController
{
    // {
    //     "first class", {
    //          "A", {
    //              {1, bool (istaken)},
    //              {2, bool (istaken)},
    //          }
    //      }    
    //     "business class", {
    //          "A", {
    //              {1, "seat test1"},
    //              {2, "seat test2"},
    //          }
    //      }
    // }
    
    // bool will become reservation?
    private static Dictionary<string, Dictionary<string, Dictionary<int, bool>>> _boeing737Layout = new ()
    {
        {
            "First class", new Dictionary<string, Dictionary<int, bool>>()
        {
            {
                "A", new Dictionary<int, bool>() 
                {
                    {1, true}
                }
            }
        }}
    };


    private static Dictionary<int, string> _airbus330Layout = new ();
    private static Dictionary<int, string> _boeing787Layout = new ();

    public static List<Plane> Planes { get; } = new ()
    { 
        new Plane("Boeing 737", _boeing737Layout, "A small Boeing 737"),
        // new Plane("Airbus 330", _airbus330Layout, "An small Airbus 330"),
        // new Plane("Boeing 787", _boeing787Layout, "A big Boeing 787"),
    };

    public static Plane Create(string model, Dictionary<string, Dictionary<string, Dictionary<int, bool>>> seatsLayout, string info = "")
    {
        Plane plane = new Plane(model, seatsLayout, info);

        JsonHandle<Plane> jsonHandle = new JsonHandle<Plane>("Planes");
        jsonHandle.SaveToJson(plane);

        Planes.Add(plane);

        return plane;
    }

    public static Plane? Update(string modelToUpdate, string info)
    {
        Plane planeToEdit = Planes.Find(plane => plane.Model == modelToUpdate);
        if (planeToEdit == null) return null;
        planeToEdit.Info = info;

        return planeToEdit;
    }

    public static void Delete(string modelToDelete)
    {
        Planes.Remove(Planes.Find(plane => plane.Model == modelToDelete));
    }
}