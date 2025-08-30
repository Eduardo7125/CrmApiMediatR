using Application.Common.Interfaces;
using FluentValidation;

namespace Application.Features.Clients.Commands.UpdateClient
{
    public class UpdateClientValidator : AbstractValidator<UpdateClientCommand>
    {
        private readonly IClientRepository _clientRepository;

        public UpdateClientValidator(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;

            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("El ID del cliente es obligatorio para poder realizar la actualización.");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede exceder los 100 caracteres.")
                .Matches("^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\\s'-]+$").WithMessage("El nombre solo puede contener letras y espacios.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
                .MaximumLength(100).WithMessage("El correo electrónico no puede exceder los 100 caracteres.")
                .EmailAddress().WithMessage("Debe proporcionar una dirección de correo electrónico válida.")
                .MustAsync(BeUniqueEmailOnUpdate).WithMessage("Este correo electrónico ya está en uso por otro cliente.");

            RuleFor(c => c.PhoneNumber)
                .MaximumLength(20).WithMessage("El número de teléfono no puede exceder los 20 caracteres.")
                .When(c => !string.IsNullOrEmpty(c.PhoneNumber))
                .Matches(@"^[\d\s\+\-\(\)]+$").WithMessage("El número de teléfono contiene caracteres no válidos.");

            RuleFor(c => c.Company)
                .NotEmpty().WithMessage("El nombre de la empresa es obligatorio.")
                .MaximumLength(150).WithMessage("El nombre de la empresa no puede exceder los 150 caracteres.");
        }

        private async Task<bool> BeUniqueEmailOnUpdate(UpdateClientCommand command, string email, CancellationToken cancellationToken)
        {
            var existingClient = await _clientRepository.GetByEmail(email);

            if (existingClient == null)
            {
                return true;
            }

            return existingClient.Id == command.Id;
        }
    }
}