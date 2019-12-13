using CRUD_NetCore3._1_TDD.Test.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_NetCore3._1_TDD.Test.Validations
{
    public class PostUserValidation: AbstractValidator<User>
    {
        public PostUserValidation()
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .WithErrorCode("101");

            RuleFor(x => x.Age)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThan(0)
                .WithErrorCode("102");
        }
    }
}
