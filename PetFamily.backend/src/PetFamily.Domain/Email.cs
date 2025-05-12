using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace PetFamily.Domain;

public class Email : ValueObject
{
    private Email(string emailAddress)
    {
        EmailAddress = emailAddress;
    }
    
    public string EmailAddress { get;  }

    public static Result<Email> Create(string emailAddress)
    {
        Regex regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        bool isCorrect = regex.IsMatch(emailAddress);
        
        if (isCorrect==false)
            return Result.Failure<Email>("Email is not correct");

        return Result.Success<Email>(new Email(emailAddress));
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return EmailAddress;
    }
}