﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using BusinessLayer;
using BusinessLayer.Models;
using MailService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Models;
using Models.Interfaces;
using Models.Mail;
using Moq;
using UCLToolBox;

namespace MailService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MailController : Controller
    {
        private readonly ILogger<MailController> _logger;
        private readonly ApiClientProxy _proxy;
        private readonly IMailHelper _mailHelper;
        private readonly ICompositeViewEngine _viewEngine;

        public MailController(ILogger<MailController> logger, ApiClientProxy proxy, IMailHelper mailHelper, ICompositeViewEngine viewEngine)
        {
            _logger = logger;
            _proxy = proxy;
            _mailHelper = mailHelper;
            _viewEngine = viewEngine;
        }

        [HttpPost]
        public async Task<ApiResponse<string>> PostMail(Template template, string recipientId, string resourceId = null)
        {
            string mailContent = await RenderViewToString(template.ToString(), new TemplateViewModel());
            try
            {
                var response = new ApiResponse<User>(ApiResponseCode.OK, new User() { UserName = "Tonur", Address = "Østerbrogade 20", Email = "chriskpedersen@hotmail.com", }); //_proxy.Get<ApiResponse<User>>("User/GetProfile/");// + recipientId);
                MailMessage mail = _mailHelper.GenerateMail(mailContent, response.Value);
                _mailHelper.SendMail(mail);
            }
            catch (Exception e)
            {
                return new ApiResponse<string>(ApiResponseCode.InternalServerError, e.Message);
            }
            return new ApiResponse<string>(ApiResponseCode.OK, "Email send");
        }

        [HttpGet]
        private async Task<string> RenderViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.ActionDescriptor.ActionName;

            ViewData.Model = model;

            using (var writer = new StringWriter())
            {
                ViewEngineResult viewResult =
                    _viewEngine.FindView(ControllerContext, viewName, false);

                ViewContext viewContext = new ViewContext(
                    ControllerContext,
                    viewResult.View,
                    ViewData,
                    TempData,
                    writer,
                    new HtmlHelperOptions()
                );

                await viewResult.View.RenderAsync(viewContext);

                return writer.GetStringBuilder().ToString();
            }
        }
    }
}
