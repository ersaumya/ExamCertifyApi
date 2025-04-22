﻿using ExamCertify.Application.DTOs;
using ExamCertify.Application.Interfaces.Courses;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamCertify.Application.DTOValidations
{
    public class UpdateCourseValidator : AbstractValidator<UpdateCourseDto>
    {
        public UpdateCourseValidator(ICourseRepository repository)
        {
            RuleFor(x => x.Title).NotNull()
                .NotEmpty()
                .MaximumLength(100)
                .MustAsync(async (title, cancellation) =>
                    title == null || !await repository.IsTitleDuplicateAsync(title))
                .WithMessage("The course title must be unique.The course title you passed is already exist.");
            RuleFor(x => x.Description)
                .NotNull()
                .NotEmpty()
               .MaximumLength(500);
        }
    }
}
