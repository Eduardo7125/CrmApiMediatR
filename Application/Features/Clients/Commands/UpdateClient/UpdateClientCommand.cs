using MediatR;
using System;

namespace Application.Features.Clients.Commands.UpdateClient
{
    public class UpdateClientCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? Company { get; set; }
        public bool IsActive { get; set; }
    }
}