using backend.Models;

namespace backend.Controllers;

public class AuthController
{
    public static (bool, User) Login(NStack.ustring email, NStack.ustring password)
    {
        List<User> userList = User.All();
        User? findUser = userList.Find(i => i.Email == (string)email);
        if (findUser == null)
            return (false, null);
        
        bool login = BCrypt.Net.BCrypt.Verify((string)password, findUser.Password);
        return (login, findUser);
    }
}
