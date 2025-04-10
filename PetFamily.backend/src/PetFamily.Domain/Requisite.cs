using CSharpFunctionalExtensions;

namespace PetFamily.Domain;

public class Requisite : ValueObject
{
    private Requisite( string name, string description)
    {
        Name = name;
        Description = description;
    }
    
    public string Name { get; }
    public string Description { get;  }
    

    public static Result<Requisite> Create(string name, string description = "")
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Requisite>("Name cannot be empty");

        return Result.Success<Requisite>(new Requisite(name, description));
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}