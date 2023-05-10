using backend.Models;

namespace backend.Controllers;

public class AuthController
{
    public static (bool, User) Login(NStack.ustring email, NStack.ustring password)
    {
        
        JsonHandle<User> jsonHandle = new JsonHandle<User>("Users");
        List<User> userList = jsonHandle.LoadJson();
        User findUser = userList.Find(i => i.Email == (string)email);
        bool loggin = BCrypt.Net.BCrypt.Verify((string)password, findUser.Password);
        return (loggin, findUser);
    }
}
