using WordWise.Core.User.ValueObjects;

namespace WordWise.Core.User;

public class Administrator : User
{
    private Administrator(Guid id, FirstName firstName, LastName lastName, UserName userName, Email email, Phone phone)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        Email = email;
        Phone = phone;
    }

    protected Administrator()
    {
        
    }

    public UserName UserName { get; private set; }
    public Email Email { get; private set; }

    public static Administrator Create(Guid id, FirstName firstName, LastName lastName, UserName userName, Email email, Phone phone)
    {
        Administrator administrator = new(id, firstName, lastName, userName, email, phone);


        return administrator;
    }
}
