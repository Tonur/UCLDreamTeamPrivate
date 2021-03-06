﻿using Autofac;
using UCLDreamTeam.SharedInterfaces.Interfaces;
using UCLToolBox;
using Xamarin.Forms;
using XamarinFormsApp.Helpers;
using XamarinFormsApp.Model;
using XamarinFormsApp.View;

namespace XamarinFormsApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //DependencyService.Register<HttpClient>();
            //DependencyService.Register<ApiClientProxy>();

            AutofacHelper.Initialize();

            ////TODO remove in future
            //var authService = AutofacHelper.Container.Resolve<AuthService>();
            //var api = AutofacHelper.Container.Resolve<ApiClientProxy>();
            //var response = api.Post("Auth/Login", new Login { UsernameOrEmail = "test", PasswordHash = "P@ssw0rd" });
            //var result = ApiClientProxy.ReadAnswer<ApiResponse<string>>(response);
            //if (result.Code == ApiResponseCode.OK)
            //{
            //    authService.Login(result.Value);
            //    MainPage = new NavigationPage(new ResourceView());
            //}
            //else
            //{
            //    MainPage = new NavigationPage(new MainPage());
            //}

            MainPage = new NavigationPage(new MainPage());
    }
    protected override void OnStart()
        {
            //DependencyService.Get<HttpClient>().BaseAddress = new Uri("http://10.0.2.2:5000/Auth/");
        }
        public static string User = "TestUser";

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}