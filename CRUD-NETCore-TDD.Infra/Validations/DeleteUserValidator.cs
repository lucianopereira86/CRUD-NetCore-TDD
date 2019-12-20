using CRUD_NETCore_TDD.Infra.Models;
using FluentValidation;

namespace CRUD_NETCore_TDD.Infra.Validations
{
    public class DeleteUserValidator : AbstractValidator<User>
    {
        public DeleteUserValidator()
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThan(0)
                .WithErrorCode("103");
        }
    }
}
