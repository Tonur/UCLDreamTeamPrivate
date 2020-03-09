﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormsApp.Model;
using XamarinFormsApp.ViewModel;

namespace XamarinFormsApp.View
{
  [XamlCompilation(XamlCompilationOptions.Compile)]
  public partial class RegisterPage : ContentPage
  {
    private RegisterViewModel _registerViewModel;

    public RegisterPage()
    {
      InitializeComponent();
      BindingContext = _registerViewModel =
        new RegisterViewModel();
    }

    private async void RegisterButton_Clicked(object sender, EventArgs e)
    {
      if (await _registerViewModel.Register())
      {
        await Navigation.PushAsync(new HomePage());
      }
      else
      {
        await DisplayAlert("Alert", _registerViewModel.ErrorMessage, "OK");
      }
    }
  }
}