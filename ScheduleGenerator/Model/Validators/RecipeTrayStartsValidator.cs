namespace ScheduleGenerator.Model.Validators
{
    using FluentValidation;
    using ScheduleGenerator.Model.Input;

    public class RecipeTrayStartsValidator : AbstractValidator<RecipeTrayStarts>
    {
        public RecipeTrayStartsValidator()
        {
            RuleForEach(x => x.Input).SetValidator(new RecipeTrayStartValidator());
        }
    }

    public class RecipeTrayStartValidator : AbstractValidator<RecipeTrayStart>
    {
        public RecipeTrayStartValidator()
        {
            RuleFor(r => r.TrayNumber).GreaterThanOrEqualTo(0);

            RuleFor(r => r.RecipeName).NotEmpty();

            RuleFor(r => r.StartDate).GreaterThanOrEqualTo(DateTime.UtcNow);
        }
    }
}
