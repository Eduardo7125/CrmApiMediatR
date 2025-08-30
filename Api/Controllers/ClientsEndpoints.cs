using Api.Controllers;
using Application.Features.Clients.Commands.CreateClient;
using Application.Features.Clients.Commands.UpdateClient;
using Application.Features.Clients.Dtos;
using Application.Features.Clients.Queries.GetAllClients;
using Application.Features.Clients.Queries.GetClientById;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;

namespace Api.Controllers
{
    public static class ClientsEndpoints
    {
        public static void MapClientsEndpoints(this IEndpointRouteBuilder routes)
        {
            var group = routes.MapGroup("/api/clients").WithTags("Clients");

            // POST /api/clients
            group.MapPost("/", async (CreateClientCommand command, ISender sender) =>
            {
                var result = await sender.Send(command);
                return Results.Created($"/api/clients/{result.Id}", result);
            })
            .WithName("CreateClient")
            .Produces<ClientDto>(StatusCodes.Status201Created)
            .Produces(StatusCodes.Status400BadRequest);

            // GET /api/clients
            group.MapGet("/", async ([AsParameters] GetAllClientsQuery query, ISender sender) =>
            {
                var result = await sender.Send(query);
                return Results.Ok(result);
            })
            .WithName("GetAllClients")
            .Produces<List<ClientDto>>(StatusCodes.Status200OK);

            // GET /api/clients/{id}
            group.MapGet("/{id:guid}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetClientByIdQuery { Id = id });
                return result is not null ? Results.Ok(result) : Results.NotFound();
            })
            .WithName("GetClientById")
            .Produces<ClientDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound);

            // PUT /api/clients/{id}
            group.MapPut("/{id:guid}", async (Guid id, UpdateClientCommand command, ISender sender) =>
            {
                if (id != command.Id) return Results.BadRequest("ID mismatch");

                await sender.Send(command);
                return Results.NoContent();
            })
            .WithName("UpdateClient")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status404NotFound);

            // DELETE /api/clients/{id}
            group.MapDelete("/{id:guid}", async (Guid id, ISender sender) =>
            {
                // Para eliminar, necesitarías un 'DeleteClientCommand'
                // await sender.Send(new DeleteClientCommand { Id = id }); 
                return Results.NoContent();
            })
            .WithName("DeleteClient")
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound);
        }
    }
}