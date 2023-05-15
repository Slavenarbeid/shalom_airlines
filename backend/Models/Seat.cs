namespace backend.Models;

public class Seat : Model<Seat>
{
    public string Type { get; }

    public User? Reservation { get; set; }

    public Seat(string type, User? reservation)
    {
        Type = type;
        Reservation = reservation;
    }
}