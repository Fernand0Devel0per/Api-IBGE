using AddressConsultation.Application.DTO;
using AddressConsultation.Application.Infra;
using AddressConsultation.Infra.Cache.Implements.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalException;
using System.Security.Claims;
using System.Text.Json;

namespace AddressConsultation.API.Endpoints
{
    public static class UserEndpoint
    {
        public static void ConfigureUserEndpoints(this WebApplication app)
        {
            app.MapPost("/api/user", InsertUser)
                .WithName("InsertUser")
                .Accepts<UserDTO>("application/json")
                .Produces<UserDTO>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status500InternalServerError);

            app.MapPut("/api/user", UpdateUser)
                .WithName("UpdateUser")
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Admin,UserDefault" })
                .Accepts<UserDTO>("application/json")
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status500InternalServerError);

            app.MapDelete("/api/user", DeleteUser)
                .WithName("DeleteUser")
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Admin,UserDefault" })
                .Accepts<UserDTO>("application/json")
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError);
        }

        /// <summary>
        /// Inserts a new user.
        /// </summary>
        /// <returns>A status indicating the result of the insertion operation.</returns>
        /// <response code="201">The user was successfully created.</response>
        /// <response code="400">Bad request, possible validation errors.</response>
        /// <response code="500">Internal server error.</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/user
        ///     {
        ///         "Username": "username",
        ///         "Email": "email@domain.com",
        ///         "Password": "password123",
        ///         "ConfirmPassword": "password123",
        ///         "Role": "Admin"
        ///     }
        /// </remarks>
        private async static Task<IResult> InsertUser(IUserService userService, [FromBody] UserDTO userDto)
        {
            try
            {
                await userService.InsertUserAsync(userDto);
                return Results.Ok(new { Message = "User created successfully." });
            }
            catch (CustomValidationException cv)
            {
                var errorObject = JsonSerializer.Deserialize<object>(cv.Message);
                return Results.BadRequest(new { Message = errorObject });
            }
            catch (Exception)
            {
                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Updates the user details.
        /// </summary>
        /// <returns>A status indicating the result of the update operation.</returns>
        /// <response code="200">The user was successfully updated.</response>
        /// <response code="400">Bad request, possible invalid parameters or validation errors.</response>
        /// <response code="500">Internal server error.</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/user
        ///     {
        ///         "Username": "username",
        ///         "Email": "email@domain.com",
        ///         "Password": "password123",
        ///         "ConfirmPassword": "password123",
        ///         "Role": "Admin"
        ///     }
        /// </remarks>
        private async static Task<IResult> UpdateUser(IUserService userService, [FromBody] UserDTO userDto, HttpContext httpContext)
        {
            var username = httpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            
            try
            {
                await userService.UpdateUserAsync(userDto, username);
                return Results.Ok();
            }
            catch (CustomValidationException cv)
            {
                var errorObject = JsonSerializer.Deserialize<object>(cv.Message);
                return Results.BadRequest(new { Message = cv.Message });
            }
            catch (Exception)
            {
                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <returns>A status indicating the result of the delete operation.</returns>
        /// <response code="200">The user was successfully deleted.</response>
        /// <response code="500">Internal server error.</response>
        private async static Task<IResult> DeleteUser(IUserService userService, HttpContext httpContext, IBlackListCache<TokenDTO> blackListCache)
        {
            var username = httpContext.User.FindFirst(ClaimTypes.Name)?.Value;
            var email = httpContext.User.FindFirst(ClaimTypes.Email)?.Value;
            try
            {
                await userService.DeleteUserAsync(username, email);
                var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
                await blackListCache.Set(token, new TokenDTO { Token = token });
                return Results.Ok();
            }
            catch (Exception)
            {
                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
