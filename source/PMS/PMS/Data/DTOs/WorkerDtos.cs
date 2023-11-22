using FluentValidation;

namespace PMS.Data.DTOs
{
    public record CreateWorkerDto(string FirstName, string LastName, string Username, string Password);
    public record UpdateWorkerDto(string Username, string Password);
    public class CreateWorkerDtoValidator : AbstractValidator<CreateWorkerDto>
    {
        public CreateWorkerDtoValidator()
        {
            RuleFor(dto => dto.FirstName).NotEmpty().NotNull();
            RuleFor(dto => dto.LastName).NotEmpty().NotNull();
            RuleFor(dto => dto.Username).NotEmpty().NotNull().Length(3, 10);
            RuleFor(dto => dto.Password).NotEmpty().NotNull().Length(6, 15);
        }
    }

    public class UpdateWorkerDtoValidator : AbstractValidator<UpdateWorkerDto>
    {
        public UpdateWorkerDtoValidator()
        {
            RuleFor(dto => dto.Username).NotEmpty().NotNull().Length(3, 10);
            RuleFor(dto => dto.Password).NotEmpty().NotNull().Length(6, 15);
        }
    }
}
