using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Species;

public class Species
{
    private static List<Species> s_species = new List<Species>();

    private List<Breed> _breeds;
    
    private Species(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        _breeds = new List<Breed>();
    }
    
    public static IReadOnlyList<Species> s_Species => s_species;
    
    public Guid Id { get; }
    public string Name { get; private set; }
    public IReadOnlyList<Breed> Breeds => _breeds;

    public Result<Breed> AddBreed(string name)
    {
        var result = Breed.Create(name);
        
        if (result.IsSuccess)
            _breeds.Add(result.Value);

        return result;
    }
    
    public static Result<Species> AddSpecies(string name)
    {
        var result = Species.Create(name);
        
        if (result.IsSuccess)
            s_species.Add(result.Value);

        return result;
    }

    public bool IsBreedExist(Breed breed)
    {
        var selectedBreeds = from item in _breeds where item.Id == breed.Id select item;
        
        return selectedBreeds.Count()!=0;
    }

    public static Result<Species> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Species>("Name is not be empty");

        return Result.Success<Species> (new Species(name));
    }
}