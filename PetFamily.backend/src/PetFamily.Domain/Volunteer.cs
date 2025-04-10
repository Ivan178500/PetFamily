using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace PetFamily.Domain;

public class Volunteer
{
    private readonly List<SocialNetwork> _socialNetworks;
    private readonly List<Requisite> _requisites;
    private readonly List<Pet> _pets;
    
    private Volunteer(FullName name, Email emailAddress)
    {
        Id = Guid.NewGuid();
        _socialNetworks = [];
        _pets = [];
        _requisites = [];
        Name = name;
        EmailAddress = emailAddress;
    }
    
    public Guid Id { get;}
    public FullName Name { get; }
    public Email EmailAddress { get; }
    public string Description { get; private set; }
    public uint ExperienceInYears { get; private set; }
    public string PhoneNumber { get; private set; }
    public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;
    public IReadOnlyList<Requisite> Requisites => _requisites;
    public IReadOnlyList<Pet> Pets => _pets;
   
    public void AddSocialNetwork(SocialNetwork socialNetwork)
    {
        _socialNetworks.Add(socialNetwork);
    }

    public void AddRequisite(Requisite requisite)
    {
        _requisites.Add(requisite);
    }

    public void AddPet(Pet pet)
    {
        _pets.Add(pet);
    }

    public int GetPetsFoundHomeCount()
    {
        var selectedPets = from pet in _pets where pet.HelpStatus == HelpStatus.FoundHome select pet;

        return selectedPets.Count();
    }

    public int GetPetsLookingHomeCount()
    {
        var selectedPets = from pet in _pets where pet.HelpStatus == HelpStatus.LookingHome select pet;

        return selectedPets.Count();
    }
    
    public int GetPetsNeedHelpCount()
    {
        var selectedPets = from pet in _pets where pet.HelpStatus == HelpStatus.NeedHelp select pet;

        return selectedPets.Count();
    }

    public Result<SocialNetwork> AddSocialNetwork(string name, string url)
    {
        var result = SocialNetwork.Create(name, url);
        
        if (result.IsSuccess)
            _socialNetworks.Add(result.Value);

        return result;
    }
    
    public static Result<Volunteer> Create(string surname, string name, string fathername, string emailAddress)
    {
        var resultFullName = FullName.Create(surname, name, fathername);
        var resultEmail = Email.Create(emailAddress);

        if (resultFullName.IsFailure || resultEmail.IsFailure)
            return Result.Failure<Volunteer>(resultFullName.Error + " " + resultEmail.Error);
        
        var volunteer = new Volunteer(resultFullName.Value, resultEmail.Value);

        return Result.Success<Volunteer>(volunteer);
    }
}