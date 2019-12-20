using FluentValidation;

namespace CRUD_NETCore_TDD.Infra.Validations
{
    public class PutUserValidator: PostUserValidator
    {
        public PutUserValidator()
        {
            RuleFor(x => x.Id)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .GreaterThan(0)
                .WithErrorCode("103");
        }
    }
}
