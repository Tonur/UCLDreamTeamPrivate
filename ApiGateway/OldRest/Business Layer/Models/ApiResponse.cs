﻿namespace Business_Layer.Models
{
    public enum ApiResponseCode
    {
        OK = 200,
        Created = 201,
        NoContent = 204,
        NotModified = 304,
        BadRequest = 400,
        UnAuthenticated = 401,
        InternalServerError = 500,
        EmailAlreadyTaken = 1001,
        UsernameAlreadyTaken = 1002
    }

    public class ApiResponse<T>
    {
        public ApiResponse(ApiResponseCode code, T value)
        {
            Code = code;
            Value = value;
        }

        public ApiResponseCode Code { get; }

        public T Value { get; }
    }
}