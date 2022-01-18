namespace Bet.BuildingBlocks.Application.Abstractions;

public abstract class BaseResult
{
    private readonly List<string> _errors = new();

    protected BaseResult()
    {
    }

    protected BaseResult(IEnumerable<string> errors)
    {
        if (errors == null)
        {
            throw new ArgumentNullException(nameof(errors));
        }

        _errors.AddRange(errors);
    }

    public bool Succeeded => _errors.Count == 0;

    public IEnumerable<string> Errors => _errors;

    public void AddError(string error)
    {
        _errors.Add(error);
    }
}
