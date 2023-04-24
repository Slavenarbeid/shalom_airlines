using backend.Models;

namespace backend.Controllers;

public class UserController
{
    public static User Create(string email, string firstName, string lastname, string password)
    {
        User user = new(email, firstName, lastname, password);

        JsonHandle<User> jsonHandle = new JsonHandle<User>("Users");
        jsonHandle.AddToJson(user);
        
        return user;
    }
}