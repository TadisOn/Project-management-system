using FluentValidation;

namespace PMS.Data.DTOs
{
    public record CreateWorkerDto(string FirstName, string LastName, string Username);
    public record UpdateWorkerDto(string Username, string FirstName, string LastName);
    public class CreateWorkerDtoValidator : AbstractValidator<CreateWorkerDto>
    {
        public CreateWorkerDtoValidator()
        {
            RuleFor(dto => dto.FirstName).NotEmpty().NotNull();
            RuleFor(dto => dto.LastName).NotEmpty().NotNull();
            RuleFor(dto => dto.Username).NotEmpty().NotNull().Length(3, 20);
        }
    }

    public class UpdateWorkerDtoValidator : AbstractValidator<UpdateWorkerDto>
    {
        public UpdateWorkerDtoValidator()
        {
            RuleFor(dto => dto.Username).NotEmpty().NotNull().Length(3, 10);
        }
    }
}
