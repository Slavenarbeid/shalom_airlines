using backend.Models;

namespace backend.Controllers;

public static class PlaneController
{
    public static List<Plane> Planes { get; } = new ()
    { // in future this should be loaded by the planes.json
        new Plane("Boeing 737", 30, 10),
        new Plane("Airbus 330", 20, 5),
        new Plane("Boeing 787", 40, 15),
    };

    public static Plane Create(string model, int businessSeats, int firstClassSeats)
    {
        Plane plane = new Plane(model, businessSeats, firstClassSeats);

        JsonHandle<Plane> jsonHandle = new JsonHandle<Plane>("Planes");
        jsonHandle.AddToJson(plane);

        Planes.Add(plane);

        return plane;
    }

    public static void Delete(string modelToDelete)
    {
        Planes.Remove(Planes.Find(plane => plane.Model == modelToDelete));
    }
}