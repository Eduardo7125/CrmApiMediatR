using Application.Features.Clients.Dtos;
using MediatR;

namespace Application.Features.Clients.Commands.CreateClient
{
    public class CreateClientCommand : IRequest<ClientDto>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string PhoneNumber { get; set; } = default!;
        public required string Company { get; set; } = default!;
        public required string Address { get; set; } = default!;
    }
}
