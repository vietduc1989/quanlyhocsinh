// QUAN-20260531-154643
using FluentValidation;
using ONENET.Application.Features.Scores.Commands;

namespace ONENET.Application.Features.Scores.Validators
{
    public class UpdateScoreCommandValidator : AbstractValidator<UpdateScoreCommand>
    {
        public UpdateScoreCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Score ID is required.");
            RuleFor(x => x.Value)
                .NotEmpty().WithMessage("Score value is required.")
                .Must(BeAValidScoreValue).WithMessage("Score value must be between 0.0 and 10.0 with at most 2 decimal places.");
        }

        // BR01: Validation for score value range and decimal places
        private bool BeAValidScoreValue(decimal value)
        {
            if (value < 0.0m || value > 10.0m)
            {
                return false;
            }
            // Check for max 2 decimal places
            return decimal.Round(value, 2) == value;
        }
    }
}