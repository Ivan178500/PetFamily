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
        CreateDate = DateOnly.FromDateTime(DateTime.Today);
        SpeciesId = species.Id;
        BreedId = breed.Id;
    }
    
    public Guid Id { get; }
    public string Name { get; }
    public string Species { get; }
    public string Description { get; private set; }
    public string Breed { get; }
    public string Color { get; private set;}
    public string HealthInfo { get; private set; }
    public string Address { get; private set; }
    public float Weight { get; private set; }
    public float Height { get; private set; }
    public string ContactInfo { get; private set; }
    public bool IsCastrate { get; private set; }
    public DateOnly BirthDay { get; }
    public bool IsVaccinated { get; private set; }
    public HelpStatus HelpStatus { get; private set; }
    public Requisite Requisite { get; private set; }
    public DateOnly CreateDate { get; }
    public Guid SpeciesId { get; }
    public Guid BreedId { get; }

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

    public void SetHelpStatus(HelpStatus helpStatus)
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
        string error = string.Empty;
        
        if (string.IsNullOrWhiteSpace(name))
            error += "Name cannot be empty ";
        
        if (string.IsNullOrWhiteSpace(color))
            error += "Color cannot be empty ";
        
        if (birthDay == DateOnly.FromDateTime(default(DateTime)))
            error += "Birthday cannot be empty ";

        if (species.IsBreedExist(breed) == false)
            error += "Breed not exist in species";

        if (error != string.Empty)
            return Result.Failure<Pet>(error);

        return Result.Success<Pet>(new Pet(name, species, breed, color, birthDay));
    }
}