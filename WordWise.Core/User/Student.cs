using WordWise.Core.User.ValueObjects;

namespace WordWise.Core.User;

public class Student : User
{
    private Student(Guid id, FirstName firstName, LastName lastName, Phone phone)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Phone = phone;
    }

    private Student(Guid id, FirstName firstName, LastName lastName, Email email, Phone phone)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
    }

    protected Student()
    {

    }

    public Email? Email { get; private set; }
    public bool IsEmailVerified { get; private set; } = false;
    public bool IsPhoneVerified { get; private set; } = false;

    public List<TakenMedia> TakenMedias { get; private set; } = [];

    public static Student Register(Guid id, FirstName firstName, LastName lastName, Phone phone)
    {
        Student customer = new(id, firstName, lastName, phone);

        // customer.RaiseDomainEvent(new CustomerFirstStepRegisteredEvent(phone));

        return customer;
    }

    public static Student Register(Guid id, FirstName firstName, LastName lastName, Email email, Phone phone)
    {
        Student customer = new(id, firstName, lastName, email, phone);

        // This Events Creates An 6 Digit OTP Code With Expiration And Expiration Time Skew
        //customer.RaiseDomainEvent(new CustomerFirstStepRegisteredEvent(phone));

        return customer;
    }


    public void VerifyEmail()
    {
        IsEmailVerified = true;
    }

    public void VerifyPhone()
    {
        IsPhoneVerified = true;
    }
}
