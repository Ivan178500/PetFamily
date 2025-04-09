using CSharpFunctionalExtensions;
using System.Text.RegularExpressions;

namespace PetFamily.Domain;

public class Volunteer
{
    private readonly List<SocialNetwork> _socialNetworks;
    private readonly List<Requisite> _requisites;
    private readonly List<Pet> _pets;
    
    private Volunteer()
    {
        Id = Guid.NewGuid();
        _socialNetworks = [];
        _pets = [];
        _requisites = [];
    }
    
    public Guid Id { get;}
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public string Description { get; private set; }
    public uint ExperienceInYears { get; private set; }
    public string PhoneNumber { get; private set; }
    public IReadOnlyList<SocialNetwork> SocialNetworks => _socialNetworks;
    public IReadOnlyList<Requisite> Requisites => _requisites;
    public IReadOnlyList<Pet> Pets => _pets;

    private Result SetName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            return Result.Failure("Name is not be empty");
        
        FullName = fullName;
        return Result.Success();
    }

    private Result SetEmail(string email)
    {
        Regex regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        bool isCorrect = regex.IsMatch(email);
        
        if (isCorrect==false)
            return Result.Failure("Email is not correct");
        
        Email = email;
        return Result.Success();
    }
    
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
        int count = 0;
        
        foreach (var pet in _pets)
            if (pet.HelpStatus == HelpStatus.FoundHome)
                count++;

        return count;
    }

    public int GetPetsLookingHomeCount()
    {
        int count = 0;
        
        foreach (var pet in _pets)
            if (pet.HelpStatus == HelpStatus.LookingHome)
                count++;

        return count;
    }
    
    public int GetPetsNeedHelpCount()
    {
        int count = 0;
        
        foreach (var pet in _pets)
            if (pet.HelpStatus == HelpStatus.NeedHelp)
                count++;

        return count;
    }

    public Result<SocialNetwork> AddSocialNetwork(string name, string url)
    {
        var result = SocialNetwork.Create(name, url);
        
        if (result.IsSuccess)
            _socialNetworks.Add(result.Value);

        return result;
    }
    
    public static Result<Volunteer> Create(string fullName, string email)
    {
        var volunteer = new Volunteer();
        var resultFullName = volunteer.SetName(fullName);
        var resultEmail = volunteer.SetEmail(email);

        if (resultFullName.IsFailure || resultEmail.IsFailure)
            return Result.Failure<Volunteer>(resultFullName.Error + " " + resultEmail.Error);

        return Result.Success<Volunteer>(volunteer);
    }
}

public class SocialNetwork
{
    private SocialNetwork(string name, string url)
    {
        Name = name;
        Url = url;
    }
    
    public string Name { get; private set; }
    public string Url { get; private set; }
    
    public static Result<SocialNetwork> Create(string name, string url)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<SocialNetwork>("Name is not be empty");

        if (string.IsNullOrWhiteSpace(url))
            return Result.Failure<SocialNetwork>("Url is not be empty");

        return Result.Success<SocialNetwork>(new SocialNetwork(name, url));
    }
}