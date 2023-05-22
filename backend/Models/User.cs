using System.Security.Cryptography;

namespace backend.Models;

public class User : Model<User>
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string Lastname { get; set; }
    public string Password { get; set; }
    public string Level { get; }

    public User(string email, string firstName, string lastname, string password, string level = "user")
    {
        Email = email;
        FirstName = firstName;
        Lastname = lastname;
        Password = password;
        Level = level;
    }
    
    public static bool EmailUsedBefore(string email)
    {
        List<User> users = All();

        return users.Find(user => user.Email == email) != null;
    }
}
    
    
    