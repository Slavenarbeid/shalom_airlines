namespace backend.Models;

public class Plane : Model<Plane>
{
    public string Model { get; }
    public int BusinessSeats { get; }
    public int FirstClassSeats { get; }
    
    public Plane(string model, int businessSeats, int firstClassSeats)
    {
        Model = model;
        BusinessSeats = businessSeats;
        FirstClassSeats = firstClassSeats;
    }
}