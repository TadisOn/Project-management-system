using FluentValidation;

namespace PMS.Data.DTOs
{
    public record CreateTaskDto(string Name, string Description);
    public record UpdateTaskDto(string Description);

    public class CreateTaskDtoValidator : AbstractValidator<CreateTaskDto>
    {
        public CreateTaskDtoValidator()
        {
            RuleFor(dto => dto.Name).NotEmpty().NotNull().Length(2, 100);
            RuleFor(dto => dto.Description).NotEmpty().NotNull().Length(10, 300);
        }
    }

    public class UpdateTaskDtoValidator : AbstractValidator<UpdateTaskDto>
    {
        public UpdateTaskDtoValidator()
        {
            RuleFor(dto => dto.Description).NotEmpty().NotNull().Length(10, 300);
        }
    }
}
