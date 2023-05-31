using backend.Models;

namespace backend.Controllers;

public static class PlaneController
{
    private static string[]?[]? _boeing737Layout =
    {
        new []{"B", "B", "B", "B", "E", "E", "E"},
        new []{"B", "B", "B", "B", "E", "E", "E"},
        null,
        new []{"B", "B", "B", "B", "E", "E", "E"},
        new []{"B", "B", "B", "B", "E", "E", "E"},
    };
    private static string[]?[]? _airbus330Layout = new string[][]
    {
        new []{"B", "B", "B", "B", "E", "E", "E"},
        new []{"B", "B", "B", "B", "E", "E", "E"},
        new []{"B", "B", "B", "B", "E", "E", "E"},
        null,
        new []{"B", "B", "B", "B", "E", "E", "E"},
        new []{"B", "B", "B", "B", "E", "E", "E"},
        new []{"B", "B", "B", "B", "E", "E", "E"},
    };
    private static string[]?[]? _boeing787Layout = new string[][]
    {
        new []{"B", "B", "B", "B", "E", "E", "E", "E", "E", "E"},
        new []{"B", "B", "B", "B", "E", "E", "E", "E", "E", "E"},
        new []{"B", "B", "B", "B", "E", "E", "E", "E", "E", "E"},
        null,
        new []{"B", "B", "B", "B", "E", "E", "E", "E", "E", "E"},
        new []{"B", "B", "B", "B", "E", "E", "E", "E", "E", "E"},
        new []{"B", "B", "B", "B", "E", "E", "E", "E", "E", "E"},
    };
    public static List<Plane> Planes { get; } = new()
    {
        new Plane("Boeing 737", SeatsConvertStringsToObjects(_boeing737Layout), "A small Boeing 737"),
        new Plane("Airbus 330", SeatsConvertStringsToObjects(_airbus330Layout), "An small Airbus 330"),
        new Plane("Boeing 787", SeatsConvertStringsToObjects(_boeing787Layout), "A big Boeing 787"),
    };

    public static List<List<Seat?>?> SeatsConvertStringsToObjects(string[]?[]? seatLayout)
    {
        List<List<Seat?>?> seatLayoutObjects = new List<List<Seat?>?> {};
        
        for (int rowInt = 0; rowInt < seatLayout.Length; rowInt++)
        {
            if (seatLayout[rowInt] == null)
            {
                seatLayoutObjects.Add(null);
                continue;
            }
            seatLayoutObjects.Add(new List<Seat?>());
            for (int seatInt = 0; seatInt < seatLayout[rowInt].Length; seatInt++)
            {
                switch (seatLayout[rowInt][seatInt])
                {
                    case "B":
                        seatLayoutObjects[rowInt].Add(new Seat("Business", null)) ;
                        break;
                    case "E":
                        seatLayoutObjects[rowInt].Add( new Seat("Economy", null));
                        break;
                    case null:
                        seatLayoutObjects[rowInt].Add(null);
                        break;
                }
            }
        }

        return seatLayoutObjects;
    }

    public static Plane Create(string model, List<List<Seat?>?> seatsLayout, string info = "")
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
