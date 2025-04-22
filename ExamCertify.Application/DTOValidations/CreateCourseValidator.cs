using ExamCertify.Application.DTOs;
using ExamCertify.Application.Interfaces.Courses;
using FluentValidation;

namespace ExamCertify.Application.DTOValidations
{
    public class CreateCourseValidator:AbstractValidator<CreateCourseDto>
    {
        private readonly ICourseRepository courseRepository;

        public CreateCourseValidator(ICourseRepository courseRepository)
        {
            RuleFor(x => x.Title).NotEmpty().NotNull().MaximumLength(100)
                .MustAsync(async (title,cancellation)=> !await courseRepository.IsTitleDuplicateAsync(title))
                .WithMessage("The course title must be unique");
            RuleFor(x => x.Description).NotEmpty().NotNull().MaximumLength(500);
            this.courseRepository = courseRepository;
        }
    }
}
