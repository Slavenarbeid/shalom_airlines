using System.Data;

namespace backend.Models;

public class Plane : Model<Plane>
{
    public string Model { get; }
    public Dictionary<string, DataTable> SeatsLayout { get; }
    public string Info { get; set; }

    public Plane(string model, Dictionary<string, DataTable> seatsLayout, string info = "")
    {
        Model = model;
        SeatsLayout = seatsLayout;
        Info = info;
    }
}