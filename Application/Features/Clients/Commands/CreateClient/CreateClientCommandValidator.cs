using Application.Common.Interfaces; // Necesario para la validación asíncrona
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Clients.Commands.CreateClient
{
    public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
    {
        private readonly IClientRepository _clientRepository;

        public CreateClientCommandValidator(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;

            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(50).WithMessage("El nombre no puede exceder los 50 caracteres.")
                .Matches("^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\\s'-]+$").WithMessage("El nombre solo puede contener letras y espacios.");

            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("El apellido es obligatorio.")
                .MaximumLength(50).WithMessage("El apellido no puede exceder los 50 caracteres.")
                .Matches("^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\\s'-]+$").WithMessage("El apellido solo puede contener letras y espacios.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
                .MaximumLength(100).WithMessage("El correo electrónico no puede exceder los 100 caracteres.")
                .EmailAddress().WithMessage("Debe proporcionar una dirección de correo electrónico válida.")
                .MustAsync(BeUniqueEmail).WithMessage("Este correo electrónico ya está en uso. Por favor, utilice otro.");

            RuleFor(c => c.PhoneNumber)
                .MaximumLength(20).WithMessage("El número de teléfono no puede exceder los 20 caracteres.")
                .When(c => !string.IsNullOrEmpty(c.PhoneNumber))
                .Matches(@"^[\d\s\+\-\(\)]+$").WithMessage("El número de teléfono contiene caracteres no válidos.");

            RuleFor(c => c.Company)
                .NotEmpty().WithMessage("El nombre de la empresa es obligatorio.")
                .MaximumLength(150).WithMessage("El nombre de la empresa no puede exceder los 150 caracteres.");

            RuleFor(c => c.Address)
                .MaximumLength(250).WithMessage("La dirección no puede exceder los 250 caracteres.");
        }

        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            var existingClient = await _clientRepository.GetByEmail(email);
            return existingClient == null;
        }
    }
}