namespace backend.Models;

public class Plane : Model<Plane>
{
    public string Model { get; }

    public List<List<Seat?>?> SeatsLayout { get; }

    public string Info { get; set; }

    public Plane(string model, List<List<Seat?>?> seatsLayout, string info = "")
    {
        Model = model;
        SeatsLayout = seatsLayout;
        Info = info;
    }
}