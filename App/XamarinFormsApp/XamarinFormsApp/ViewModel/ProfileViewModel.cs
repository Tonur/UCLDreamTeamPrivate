﻿using Autofac;
using AutoMapper;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFormsApp.Helpers;
using XamarinFormsApp.Model;

namespace XamarinFormsApp.ViewModel
{
  public class ProfileViewModel : AutoMapper.Profile
  {
    #region Constructor
    private ApiClientProxy _proxy;
    private Mapper _mapper;

    public ProfileViewModel()
    {
      _proxy = AutofacHelper.Container.Resolve<ApiClientProxy>();
      _mapper = AutofacHelper.Container.Resolve<Mapper>();
    }
    #endregion

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public int? ZipCode { get; set; }
    public string Country { get; set; }

    public string ErrorMessage { get; private set; }

    public async Task<bool> UpdateProfile()
    {
      var response = await _proxy.PostAsync(@"Auth/1", _mapper.Map<Model.Profile>(this));
      var result = await ApiClientProxy.ReadAnswerAsync<ApiResponse<string>>(response);
      if (response.IsSuccessStatusCode)
      {
        Application.Current.Properties["token"] = result.Value;
      }
      else
      {
        ErrorMessage = Enum.GetName(typeof(ApiResponseCode), result.Code);
      }
      return response.IsSuccessStatusCode;
      
    }
  }
}