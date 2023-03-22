using backend.Models;

namespace backend.Controllers;

public static class PlaneController
{
    public static Plane Create(string model, int businessSeats, int firstClassSeats)
    {
        return new Plane(model, businessSeats, firstClassSeats);
    }
    
    
}