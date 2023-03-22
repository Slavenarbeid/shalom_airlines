namespace backend.Controllers;

public class AuthController
{
    public static bool Login(NStack.ustring username, NStack.ustring password)
    {
        return username == "admin" && password == "password";
    }
}
