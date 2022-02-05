using FluentValidation;


namespace Application.Search.Queries
{
    public class SearchEngineQueryValidator : AbstractValidator<SearchEngineQuery>
    {
        public SearchEngineQueryValidator()
        {
            RuleFor(x => x.Query)
                .NotEmpty().WithMessage("Query is required.");
        }
    }
}
