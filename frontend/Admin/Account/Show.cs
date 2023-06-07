﻿using System.CodeDom;
using backend.Controllers;
using backend.Models;
using Terminal.Gui;

namespace shalom_airlines.Admin.Account;

public class Show : Window
{
    public Show(backend.Models.User user)
    {
        Title = $"Viewing details of user {user.FirstName} {user.Lastname}";

        var userlabel = new Label()
        {
            Text =
                $"Email: {user.Email}\nFirstName: {user.FirstName}\nLastName: {user.Lastname}\nLevel: {user.Level}\n",
        };


        var btnEdit = new Button()
        {
            Text = "Edit",
            Y = Pos.Bottom(userlabel) + 1,
            X = 0,
            IsDefault = true,
        };

        btnEdit.Clicked += () => { Layout.OpenWindow<EditUser>(user); };


        var btnDelete = new Button()
        {
            Text = "Delete",
            Y = Pos.Bottom(userlabel) + 1,
            X = Pos.Bottom(btnEdit) + 6,
        };

        // btnDelete.Clicked += () =>
        // {
        //     if (FlightController.Delete(user))
        //     {
        //         MessageBox.Query("Deleting Flight", "Flight Deleted", "Ok");
        //         Layout.OpenWindow<Index>();
        //         return;
        //     }
        //
        //     MessageBox.Query("Deleting Flight Failed", "Flight not Deleted", "Ok");
        // };


        var btnBack = new Button()
        {
            Text = "Back",
            Y = Pos.Bottom(btnEdit) + 1,
            X = 0,
        };
        btnBack.Clicked += Layout.Back;

        Add(userlabel, btnDelete, btnEdit, btnBack);
    }
}