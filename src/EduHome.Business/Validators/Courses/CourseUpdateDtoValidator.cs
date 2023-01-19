using EduHome.Business.DTOs.Courses;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Business.Validators.Courses;

public class CourseUpdateDtoValidator:AbstractValidator<CourseUpdateDto>
{
    public CourseUpdateDtoValidator()
    {
        RuleFor(c => c.Id).Custom((Id, context) => {
            if (!int.TryParse(Id.ToString(), out var id))
            {
                context.AddFailure("please enter correct format");
            } 
        });
        RuleFor(c => c.Name)
            .NotEmpty().WithMessage("Name is Required")
            .NotNull().WithMessage("Name is Required")
            .MaximumLength(150).WithMessage("max length 150 symbol");
        RuleFor(c => c.Image)
            .MaximumLength(200).WithMessage("max length 200 symbol");
        RuleFor(c => c.Description)
            .MaximumLength(500).WithMessage("max length 500 symbol");
    }
}
