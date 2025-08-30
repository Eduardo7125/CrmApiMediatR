using FluentValidation;
using System;

namespace Application.Features.Clients.Queries.GetClientById
{
    public class GetClientByIdQueryValidator : AbstractValidator<GetClientByIdQuery>
    {
        public GetClientByIdQueryValidator()
        {
            RuleFor(q => q.Id)
                .NotEmpty().WithMessage("El ID del cliente es obligatorio.");
        }
    }
}