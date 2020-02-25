﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using Autofac;
using AutoMapper;

namespace XamarinFormsApp.Helpers
{
  public static class AutofacHelper
  {
    public static IContainer Container;

    public static void Initialize()
    {
      if (Container == null)
      {
        //Automapper setup
        var types = Assembly.GetExecutingAssembly().GetTypes();
        var config = new MapperConfiguration(cfg =>
        {
          foreach (var type in types)
          {
            string viewmodelNamespace = $"{nameof(XamarinFormsApp)}.{nameof(ViewModel)}";
            if (type.Namespace == viewmodelNamespace)
            {
              string name = type.Name.Replace("ViewModel", "");
              string modelNamespace = $"{nameof(XamarinFormsApp)}.{nameof(Model)}";
              var modelItem = types.FirstOrDefault(t => t.Namespace == modelNamespace && t.Name == name);
              if (modelItem != null)
              {
                cfg.CreateMap(type, modelItem);
              }
            }
          }
        });
        Mapper mapper = new Mapper(config);


        var client = new HttpClient
        {
          BaseAddress = new Uri("http://10.0.2.2:5000/")
        };

        var builder = new ContainerBuilder();
        builder.RegisterInstance(mapper);
        builder.RegisterInstance(client);
        builder.RegisterType<ApiClientProxy>();
        Container = builder.Build();
      }
    }
  }
}


