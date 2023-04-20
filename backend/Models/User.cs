﻿using System.Security.Cryptography;

namespace backend.Models;

public class User : Model<User>
{
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string Lastname { get; set; }
    public string Password { get; set; }

    public User(string email, string firstName, string lastname, string password)
    {
        Email = email;
        FirstName = firstName;
        Lastname = lastname;
        Password = password;
    }
}
    
    
    