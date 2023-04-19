namespace backend.Models;

public class Plane : Model<Plane>
{
    public string Model { get; }

    public Dictionary<string, Dictionary<string, Dictionary<int, bool>>> SeatsLayout { get; }

    public string Info { get; set; }

    public Plane(string model, Dictionary<string, Dictionary<string, Dictionary<int, bool>>> seatsLayout, string info = "")
    {
        Model = model;
        SeatsLayout = seatsLayout;
        Info = info;
    }
}