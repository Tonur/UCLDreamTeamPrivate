﻿using UCLToolBox;
using Xamarin.Forms;
using XamarinFormsApp.Model;

namespace XamarinFormsApp.Helpers
{
    internal class AuthService
    {
        private readonly ApiClientProxy _proxy;

        public AuthService(ApiClientProxy proxy)
        {
            _proxy = proxy;
        }

        public void Login(string token)
        {
            _proxy.HttpClient.DefaultRequestHeaders.Clear();
            _proxy.HttpClient.DefaultRequestHeaders.Add("Authorization", token);
            Application.Current.Properties["Authorization"] = token;
            UpdateUser();
        }

        public void UpdateUser(User user = null)
        {
            user = _proxy.Get<ApiResponse<User>>("User").Value;
            Application.Current.Properties["UserData"] = user;
        }
    }
}