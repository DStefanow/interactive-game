﻿using System;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace InteractiveGame
{
    public partial class RegisterWindow : Window
    {
        private const short USERNAME_LENGTH = 5;
        private const short PASSWORD_LENGTH = 8;
        private const short FULLNAME_LENGTH = 4;

        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void BackClickEvent(object sender, RoutedEventArgs e)
        {
            NavigateToLoginWindow();
        }

        private void RegisterClickEvent(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string password = PasswordBox.Password;
            string fullName = FullNameBox.Text;

            if (!IsValidInputParams(username, password, fullName))
            {
                return;
            }

            GameUser newUser = new GameUser(username, password, fullName);
            App.DbManager.GameUser.Add(newUser);

            if (!Items.SaveChangesUniqueHandler())
            {
                MessageBox.Show("Потребителско име: " + username + " вече същестува!");
                return;
            }

            NavigateToLoginWindow();
        }

        public void NavigateToLoginWindow()
        {
            LoginWindow logWindow = new LoginWindow();
            this.Close();
            logWindow.Show();
        }

        private bool IsValidInputParams(string username, string password, string fullName)
        {
            if (username.Length < USERNAME_LENGTH)
            {
                MessageBox.Show("Потребителското име трябва да е поне " + USERNAME_LENGTH + " символа");
                return false;
            }
            else if (password.Length < PASSWORD_LENGTH)
            {
                MessageBox.Show("Паролата трябва да е поне " + PASSWORD_LENGTH + " символа");
                return false;
            }
            else if (fullName.Length < FULLNAME_LENGTH)
            {
                MessageBox.Show("Името трябва да е поне " + FULLNAME_LENGTH + " символа");
                return false;
            }

            return true;
        }
    }
}