using AddressConsultation.Application.DTO;
using AddressConsultation.Application.Infra;
using Microsoft.AspNetCore.Mvc;
using System.Security.Authentication;
using System.Text.Json;

namespace AddressConsultation.API.Endpoints
{
    public static class IAuthenticationEndpoint
    {
        

        public static void ConfigureAuthenticationEndpoints(this WebApplication app)
        {
            app.MapPost("/api/authenticate", AuthenticateUser)
                .WithName("AuthenticateUser")
                .Accepts<LoginDTO>("application/json")
                .Produces<string>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status401Unauthorized)
                .Produces(StatusCodes.Status500InternalServerError);
        }

        /// <summary>
        /// Authenticates a user and returns a JWT token.
        /// </summary>
        /// <param name="authService">The authentication service.</param>
        /// <param name="loginDto">Data transfer object containing user's login information.</param>
        /// <returns>A JWT token if authentication is successful.</returns>
        /// <response code="200">Authentication successful and token returned.</response>
        /// <response code="400">Bad request, possible invalid input or argument error.</response>
        /// <response code="401">Authentication failed.</response>
        /// <response code="500">Internal server error.</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/authenticate
        ///     {
        ///         "Username": "exampleUsernameOrEmail@email.com",
        ///         "Password": "examplePassword",
        ///     }
        ///
        /// Note: The "Username" field can accept either the username or email for authentication.
        /// </remarks>
        private async static Task<IResult> AuthenticateUser(IAuthenticationService authService, [FromBody] LoginDTO loginDto)
        {
            try
            {
                string token = await authService.AuthenticateAsync(loginDto);
                if (string.IsNullOrEmpty(token))
                {
                    return Results.Unauthorized();
                }
                return Results.Ok(token);
            }
            catch (AuthenticationException ae)
            {
                return Results.Unauthorized();
            }
            catch (ArgumentNullException cv)
            {
                var errorObject = JsonSerializer.Deserialize<object>(cv.Message);
                return Results.BadRequest(new { Message = errorObject });
            }
            catch (Exception)
            {
                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

 
    }
}
