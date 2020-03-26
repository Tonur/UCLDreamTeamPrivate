﻿using Microsoft.AspNetCore.Mvc;
using Business_Layer;
using System;
using Business_Layer.Models;
using Data_Access_Layer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace Rest_API.Controllers
{
    [Authorize]
  [Route("[controller]")]
  [ApiController]
  public class UserController : ControllerBase
  {
    private UserService _userService;
    private AuthService _authService;

    public UserController(UserService userService, AuthService authService)
    {
      _userService = userService;
      _authService = authService;
    }

    [HttpGet("guid={guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ApiResponse<User> GetProfileFromGuid([FromRoute] Guid guid)
    {
        var response = new ApiResponse<User>(ApiResponseCode.NoContent, null);

        var userProfile = _userService.GetUserFromIdAsync(guid).Result;

        if (userProfile != null)
        {
            response = new ApiResponse<User>(ApiResponseCode.OK, _userService.GetUserFromIdAsync(guid).Result);
        }
        return response;
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status304NotModified)]
    public ApiResponse<User> UpdateProfile([FromBody] User user)
    {
      if (user == null || !ModelState.IsValid)
        return new ApiResponse<User>(ApiResponseCode.BadRequest, user);

      return _userService.Update(user);
    }
  }
}