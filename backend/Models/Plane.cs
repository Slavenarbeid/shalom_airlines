namespace backend.Models;

public class Plane
{
    public string Model { get; }
    public int BusinessSeats { get; }
    public int FirstClassSeats { get; }

    public string Info { get; set; }

    public Plane(string model, int businessSeats, int firstClassSeats, string info = "")
    {
        Model = model;
        BusinessSeats = businessSeats;
        FirstClassSeats = firstClassSeats;
        Info = info;
    }
}