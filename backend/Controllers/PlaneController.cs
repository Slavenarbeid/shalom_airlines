using backend.Models;

namespace backend.Controllers;

public static class PlaneController
{

    public static List<Plane> Planes { get; } = new()
    {
        new Plane("Boeing 737", _boeing737Layout, "A small Boeing 737"),
        new Plane("Airbus 330", _airbus330Layout, "An small Airbus 330"),
        new Plane("Boeing 787", _boeing787Layout, "A big Boeing 787"),
    };

    public static Plane Create(string model, Dictionary<string, Dictionary<string, Dictionary<int, bool>>> seatsLayout, string info = "")
    {
        Plane plane = new Plane(model, seatsLayout, info);

        JsonHandle<Plane> jsonHandle = new JsonHandle<Plane>("Planes");
        jsonHandle.AddToJson(plane);
        
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