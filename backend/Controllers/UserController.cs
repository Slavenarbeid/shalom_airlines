using backend.Models;


namespace backend.Controllers;

public class UserController
{
    public static User Create(string email, string firstName, string lastname, string password)
    {

        string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
        
        User user = new(email, firstName, lastname, passwordHash);

        JsonHandle<User> jsonHandle = new JsonHandle<User>("Users");
        jsonHandle.AddToJson(user);
        
        return user;
    }
}