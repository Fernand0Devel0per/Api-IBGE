using AddressConsultation.Application.DTO;
using AddressConsultation.Application.Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalException;
using System.Text.Json;

namespace AddressConsultation.API.Endpoints
{
    public static class AddressEndpoint
    {
        public static void ConfigureAddressEndpoints(this WebApplication app)
        {
            app.MapGet("/api/address/{ibgeCode}", GetAddressByIbgeCode)
                .WithName("GetAddressByIbgeCode")
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Admin,UserDefault" })
                .Produces<AddressDTO>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status500InternalServerError);

            app.MapGet("/api/address/city/{cityName}", GetAddressesByCity)
                .WithName("GetAddressesByCity")
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Admin,UserDefault" })
                .Produces<IEnumerable<AddressDTO>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status500InternalServerError);

            app.MapGet("/api/address/state/{state}", GetAddressesByState)
                .WithName("GetAddressesByState")
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Admin,UserDefault" })
                .Produces<IEnumerable<AddressDTO>>(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status204NoContent)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status500InternalServerError);

            app.MapPost("/api/address", InsertAddress)
                .WithName("InsertAddress")
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" })
                .Accepts<AddressDTO>("application/json")
                .Produces<AddressDTO>(StatusCodes.Status201Created)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status500InternalServerError);

            app.MapPut("/api/address", UpdateAddress)
                .WithName("UpdateAddress")
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" })
                .Accepts<AddressDTO>("application/json")
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status400BadRequest)
                .Produces(StatusCodes.Status500InternalServerError);

            app.MapDelete("/api/address/{ibgeCode}", DeleteAddress)
                .WithName("DeleteAddress")
                .RequireAuthorization(new AuthorizeAttribute { Roles = "Admin" })
                .Produces(StatusCodes.Status200OK)
                .Produces(StatusCodes.Status500InternalServerError);
        }

        /// <summary>
        /// Retrieves the address details based on the provided IBGE code.
        /// </summary>
        /// <param name="addressService">The address service.</param>
        /// <param name="ibgeCode">The IBGE code of the desired address.</param>
        /// <returns>The address details for the provided IBGE code.</returns>
        /// <response code="200">Returns the address details.</response>
        /// <response code="204">No address found for the given IBGE code.</response>
        /// <response code="500">Internal server error.</response>
        private async static Task<IResult> GetAddressByIbgeCode(IAddressService addressService, [FromRoute]int ibgeCode)
        {
           
            try
            {
                AddressDTO result = await addressService.GetAddressByIbgeCodeAsync(ibgeCode);
                return Results.Ok(result);
            }
            catch (NotFoundItemException nf)
            {
                return Results.NoContent();
                
            }
            catch (Exception)
            {
                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }

           
        }

        /// <summary>
        /// Retrieves addresses based on the provided city name.
        /// </summary>
        /// <param name="addressService">The address service.</param>
        /// <param name="cityName">The name of the city.</param>
        /// <returns>A list of addresses for the provided city name.</returns>
        /// <response code="200">Returns a list of addresses.</response>
        /// <response code="204">No addresses found for the given city name.</response>
        /// <response code="400">Bad request, possible invalid parameters.</response>
        /// <response code="500">Internal server error.</response>
        private async static Task<IResult> GetAddressesByCity(IAddressService addressService, [FromRoute] string cityName, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var results = await addressService.GetAddressesByCityAsync(cityName, pageIndex, pageSize);
                return results == null || !results.Any() ? Results.NoContent() : Results.Ok(results);
            }
            catch (ArgumentOutOfRangeException ar)
            {
                return Results.BadRequest(new {Message = ar.Message });
            }
            catch (Exception)
            {
                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Retrieves addresses based on the provided state name.
        /// </summary>
        /// <param name="addressService">The address service.</param>
        /// <param name="state">The state code.</param>
        /// <returns>A list of addresses for the provided state.</returns>
        /// <response code="200">Returns a list of addresses.</response>
        /// <response code="204">No addresses found for the given state.</response>
        /// <response code="400">Bad request, possible invalid parameters.</response>
        /// <response code="500">Internal server error.</response>
        private async static Task<IResult> GetAddressesByState(IAddressService addressService, [FromRoute] string state, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var results = await addressService.GetAddressesByStateAsync(state, pageIndex, pageSize);
                return results == null || !results.Any() ? Results.NoContent() : Results.Ok(results);
            }
            catch (ArgumentOutOfRangeException ar)
            {
                return Results.BadRequest(new { Message = ar.Message });
            }
            catch (Exception)
            {
                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Retrieves the address by IBGE code.
        /// </summary>
        /// <returns>The address details.</returns>
        /// <response code="200">Returns the address details.</response>
        /// <response code="204">No address found for the given IBGE code.</response>
        /// <response code="500">Internal server error.</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/address
        ///     {
        ///         "IBGECode": 1234567,
        ///         "State": "SP",
        ///         "City": "Sao Paulo",
        ///     }
        /// </remarks>
        private async static Task<IResult> InsertAddress(IAddressService addressService, [FromBody] AddressDTO addressDto)
        {
            try
            {
                await addressService.InsertAddressAsync(addressDto);
                return Results.CreatedAtRoute("GetCoupon", new { id = addressDto.IBGECode }, addressDto);

            }
            catch(CustomValidationException cv)
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
        /// Updates the address details.
        /// </summary>
        /// <param name="addressDto">The address details to update.</param>
        /// <returns>A status indicating the result of the update operation.</returns>
        /// <response code="200">The address was successfully updated.</response>
        /// <response code="400">Bad request, possible invalid parameters or validation errors.</response>
        /// <response code="500">Internal server error.</response>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/address
        ///     {
        ///         "IBGECode": 1234567,
        ///         "State": "SP",
        ///         "City": "Sao Paulo",
        ///     }
        /// </remarks>
        private async static Task<IResult> UpdateAddress(IAddressService addressService, [FromBody] AddressDTO addressDto)
        {
            try
            {
                await addressService.UpdateAddressAsync(addressDto);
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
        /// Deletes an address based on the provided IBGE code.
        /// </summary>
        /// <param name="ibgeCode">The IBGE code of the address to delete.</param>
        /// <returns>A status indicating the result of the delete operation.</returns>
        /// <response code="200">The address was successfully deleted.</response>
        /// <response code="500">Internal server error.</response>
        private async static Task<IResult> DeleteAddress(IAddressService addressService, [FromRoute] string ibgeCode)
        {
            try
            {
                await addressService.DeleteAddressAsync(ibgeCode);
                return Results.Ok();
            }
            catch (Exception)
            {
                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
