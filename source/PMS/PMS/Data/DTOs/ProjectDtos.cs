using FluentValidation;

namespace PMS.Data.DTOs
{
    public record CreateProjectDto(string Name, string Description);
    public record UpdateProjectDto(string Description);

    public class CreateProjectDtoValidator : AbstractValidator<CreateProjectDto>
    {
        public CreateProjectDtoValidator()
        {
            RuleFor(dto => dto.Name).NotEmpty().NotNull().Length(2, 100);
            RuleFor(dto => dto.Description).NotEmpty().NotNull().Length(10, 300);
        }
    }

    public class UpdateProjectDtoValidator : AbstractValidator<UpdateProjectDto>
    {
        public UpdateProjectDtoValidator()
        {
            RuleFor(dto => dto.Description).NotEmpty().NotNull().Length(10, 300);
        }
    }
}
