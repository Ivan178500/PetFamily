using CSharpFunctionalExtensions;

namespace PetFamily.Domain;

public class SocialNetwork : ValueObject
{
    private SocialNetwork(string name, string url)
    {
        Name = name;
        Url = url;
    }
    
    public string Name { get; }
    public string Url { get; }
    
    public static Result<SocialNetwork> Create(string name, string url)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<SocialNetwork>("Name is not be empty");

        if (string.IsNullOrWhiteSpace(url))
            return Result.Failure<SocialNetwork>("Url is not be empty");

        return new SocialNetwork(name, url);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Url;
    }
}