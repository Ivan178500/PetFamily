using CSharpFunctionalExtensions;
using PetFamily.Domain.Species;

namespace PetFamily.Domain;

public class Pet
{
    private Pet(string name, Species.Species species, Breed breed, string color, DateOnly birthDay)
    {
        Id = Guid.NewGuid();
        Name = name;
        Species = species.Name;
        Description = "";
        Breed = breed.Name;
        Color = color;
        HealthInfo = "";
        Address = "";
        Weight = 0f;
        Height = 0f;
        ContactInfo = "";
        IsCastrate = false;
        BirthDay = birthDay;
        IsVaccinated = false;
        HelpStatus = "";
        CreateDate = DateOnly.FromDateTime(DateTime.Today);
        SpeciesId = species.Id;
        BreedId = breed.Id;
    }
    
    public Guid Id { get; }
    public string Name { get; private set; }
    public string Species { get; private set; }
    public string Description { get; private set; }
    public string Breed { get; private set;}
    public string Color { get; private set;}
    public string HealthInfo { get; private set; }
    public string Address { get; private set; }
    public float Weight { get; private set; }
    public float Height { get; private set; }
    public string ContactInfo { get; private set; }
    public bool IsCastrate { get; private set; }
    public DateOnly BirthDay { get; }
    public bool IsVaccinated { get; private set; }
    public string HelpStatus { get; private set; }
    public Requisite Requisite { get; private set; }
    public DateOnly CreateDate { get; }
    public Guid SpeciesId { get; }
    public Guid BreedId { get; }
    

    public void SetName(string name)
    {
        Name = name;
    }
    
    public void SetDescription(string description)
    {
        Description = description;
    }

    public void SetHealthInfo(string healthInfo)
    {
        HealthInfo = healthInfo;
    }

    public void SetAddress(string address)
    {
        Address = address;
    }

    public Result SetWeight(float weight)
    {
        if (weight <= 0)
            return Result.Failure("Weight cannot be negative");
        
        Weight = weight;
        return Result.Success();
    }

    public Result SetHeight(float height)
    {
        if (height <= 0)
            return Result.Failure("Height cannot be negative");
        
        Height = height;
        return Result.Success();
    }

    public void SetContactInfo(string contactInfo)
    {
        ContactInfo = contactInfo;
    }

    public void SetCastrateStatus(bool isCastrate)
    {
        IsCastrate = isCastrate;
    }

    public void SetVaccinatedStatus(bool isVaccinated)
    {
        IsVaccinated = isVaccinated;
    }

    public void SetHelpStatus(string helpStatus)
    {
        HelpStatus = helpStatus;
    }

    public Result SetRequisites(string name, string description)
    {
        var result = Requisite.Create(name, description);
        Requisite = result.Value;
        return result;
    }

    public static Result<Pet> Create(string name, Species.Species species, Breed breed, string color, DateOnly birthDay)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Pet>("Name cannot be empty");
        
        if (string.IsNullOrWhiteSpace(color))
            return Result.Failure<Pet>("Color cannot be empty");
        
        if (birthDay == DateOnly.FromDateTime(default(DateTime)))
            return Result.Failure<Pet>("Birthday cannot be empty");

        return Result.Success<Pet>(new Pet(name, species, breed, color, birthDay));
    }
}

public static class HelpStatus
{
    public const string NeedHelp = "Нуждается в помощи";
    public const string LookingHome = "Ищет дом";
    public const string FoundHome = "Нашел дом";
}

public class Requisite
{
    private Requisite( string name, string description)
    {
        Name = name;
        Description = description;
    }
    
    public string Name { get; private set; }
    public string Description { get; private set; }

    public void SetDescription(string description)
    {
        Description = description;
    }

    public static Result<Requisite> Create(string name, string description = "")
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Requisite>("Name cannot be empty");

        return Result.Success<Requisite>(new Requisite(name, description));
    }
}