using CSharpFunctionalExtensions;

namespace PetFamily.Domain;

public class FullName : ValueObject
{
    private FullName(string surname, string name, string fatherName)
    {
        Surname = surname;
        Name = name;
        Fathername = fatherName;
    }
    
    public string Name { get; }
    public string Surname { get; }
    public string Fathername { get; }
    public string Fullname => $"{Surname} {Name} {Fathername}";
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Surname;
        yield return Name;
        yield return Fathername;
    }

    public static Result<FullName> Create(string surname, string name, string fatherName)
    {
        string error = string.Empty;

        if (string.IsNullOrWhiteSpace(name))
            error += "Name is not be empty. ";
        
        if (string.IsNullOrWhiteSpace(surname))
            error += "Surname is not be empty.";
        
        if (error != string.Empty)
            return Result.Failure<FullName>(error);

        return Result.Success<FullName>(new FullName(surname, name, fatherName));
    }
}