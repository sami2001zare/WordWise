using WordWise.Core.User.Events;
using WordWise.Core.User.ValueObjects;

namespace WordWise.Core.User.Student;

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


    public ProficiencyLevel? ProficiencyLevel { get; private set; }
    public LearningGoal? LearningGoal { get; private set; }
    public LearningStyle? LearningStyle { get; private set; }
    public ContentFocus? ContentFocus { get; private set; }

    private readonly List<TakenMedia> _takenMedias = [];
    public IReadOnlyCollection<TakenMedia> TakenMedias => _takenMedias.AsReadOnly();

    private readonly List<UploadedContent> _uploadedContents = [];
    public IReadOnlyCollection<UploadedContent> UploadedContents => _uploadedContents.AsReadOnly();

    private readonly List<SavedVocabulary> _savedVocabularies = [];
    public IReadOnlyCollection<SavedVocabulary> SavedVocabularies => _savedVocabularies.AsReadOnly();

    // Review frequency settings (R-21)
    public string ReminderFrequency { get; private set; } = "daily"; // daily, weekly, monthly
    public bool RemindersEnabled { get; private set; } = true;

    public void ToggleReminders(bool enabled)
    {
        RemindersEnabled = enabled;
    }

    public void SetReminderFrequency(string frequency)
    {
        ReminderFrequency = frequency;
    }

    public void SaveVocabulary(SavedVocabulary vocabulary)
    {
        _savedVocabularies.Add(vocabulary);

        // R-49: Trigger success notification when reaching a learning milestone (e.g., 100 words)
        if (_savedVocabularies.Count % 100 == 0)
        {
            RaiseDomainEvent(new LearningMilestoneReachedEvent(Id, _savedVocabularies.Count));
        }
    }

    public void RecordTakenMedia(TakenMedia media)
    {
        _takenMedias.Add(media);
    }

    public void AddUploadedContent(UploadedContent content)
    {
        _uploadedContents.Add(content);
    }

    public static Student Register(Guid id, FirstName firstName, LastName lastName, Phone phone)
    {
        Student customer = new(id, firstName, lastName, phone);

        customer.RaiseDomainEvent(new StudentFirstStepRegisteredEvent(phone));

        return customer;
    }

    public static Student Register(Guid id, FirstName firstName, LastName lastName, Email email, Phone phone)
    {
        Student customer = new(id, firstName, lastName, email, phone);

        // This Events Creates An 6 Digit OTP Code With Expiration And Expiration Time Skew
        customer.RaiseDomainEvent(new StudentFirstStepRegisteredEvent(phone));

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

    public bool IsSurvayDateFilled()
    {
        if (ProficiencyLevel is null || LearningGoal is null || LearningStyle is null || ContentFocus is null)
        {
            return false;
        }

        return true;
    }


    public void SetProficiency(ProficiencyLevel proficiencyLevel) => ProficiencyLevel = proficiencyLevel;
    public void SetLearningGoal(LearningGoal learningGoal) => LearningGoal = learningGoal;
    public void SetLearningStyle(LearningStyle learningStyle) => LearningStyle = learningStyle;
    public void SetContentFocus(ContentFocus contentFocus) => ContentFocus = contentFocus;
}