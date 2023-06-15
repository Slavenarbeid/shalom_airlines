namespace backend.Models;

public class User : Model<User>
{
    public string ID { get; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string Lastname { get; set; }
    public string Password { get; set; }
    public string Level { get; }

    public User(string id, string email, string firstName, string lastname, string password, string level = "user")
    {
        ID = id;
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

    public override string ToString() => $"{Email} {FirstName} {Lastname} {Level}";

    public override bool Equals(object? newObj)
    {
        var item = newObj as User;

        if (ID != item.ID) return false;
        if (Email != item.Email) return false;
        if (FirstName != item.FirstName) return false;
        if (Lastname != item.Lastname) return false;
        if (Password != item.Password) return false;
        if (Level != item.Level) return false;

        return true;
    }
}
    
    
    