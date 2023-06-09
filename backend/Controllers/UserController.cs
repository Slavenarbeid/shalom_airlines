﻿using System.Runtime.InteropServices;
using backend.Models;


namespace backend.Controllers;

public class UserController
{
    public static User Create(string email, string firstName, string lastname, string password)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
        
        User user = new(User.CreateUUID(), email, firstName, lastname, passwordHash);

        JsonHandle<User> jsonHandle = new JsonHandle<User>("Users");
        jsonHandle.AddToJson(user);

        return user;
    }

    public static User Update(User oldUser, string email, string firstName, string lastname, string password)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

        User newUser = new(oldUser.ID, email, firstName, lastname, passwordHash, oldUser.Level);

        JsonHandle<User> jsonHandle = new JsonHandle<User>("Users");
        jsonHandle.UpdateJson(oldUser, newUser);
        return newUser;
    }
    
    public static User UpdateUserByID(string uuid, string email, string firstName, string lastname, string password, string level)
    {
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
        
        User newUser = new User(uuid, email, firstName, lastname, passwordHash, level);
        
        JsonHandle<User> jsonHandle = new JsonHandle<User>("Users");
        List<User> users = User.All();
        User userToUpdate = users.Find(obj => obj.ID == uuid);
        var index = users.IndexOf(userToUpdate);
        if(index != -1)
            users[index] = newUser;
        jsonHandle.SaveJsonFile(users);
        
        return newUser;
    }
}