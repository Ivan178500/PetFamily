using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Species;

public class Breed
{
    private Breed(string name)
    {
        Id  = Guid.NewGuid();
        Name = name;
    }
    
    public Guid Id { get; }
    public string Name { get; }

    public static Result<Breed> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Breed>("Name not be empty");

        return Result.Success<Breed>(new Breed(name));
    }
}