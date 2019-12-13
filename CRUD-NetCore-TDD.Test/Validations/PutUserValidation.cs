using CRUD_NetCore3._1_TDD.Test.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD_NetCore3._1_TDD.Test.Validations
{
    public class PutUserValidation: PostUserValidation
    {
        public PutUserValidation()
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThan(0)
                .WithErrorCode("103");
        }
    }
}
