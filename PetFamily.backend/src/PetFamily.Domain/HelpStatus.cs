using CSharpFunctionalExtensions;

namespace PetFamily.Domain;

public class HelpStatus : ValueObject
{
    public static readonly HelpStatus NeedHelp = new HelpStatus("Нуждается в помощи");
    public static readonly HelpStatus LookingHome = new HelpStatus("Ищет дом");
    public static readonly HelpStatus FoundHome = new HelpStatus("Нашел дом");

    private static readonly HelpStatus[] _all = [NeedHelp, LookingHome, FoundHome];
    private HelpStatus(string value)
    {
        Value = value;
    }
    
    public string Value { get; }

    public static Result<HelpStatus> Create(string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            return Result.Failure<HelpStatus>("Status empty");

        if (_all.Any(s => s.Value.ToLower() == status) == false)
            return Result.Failure<HelpStatus>("Status incorrect");

        return Result.Success<HelpStatus>(new HelpStatus(status));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}