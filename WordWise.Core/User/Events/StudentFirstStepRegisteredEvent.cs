using WordWise.Core.User.ValueObjects;
using WordWise.Framework;

namespace WordWise.Core.User.Events;

public sealed record StudentFirstStepRegisteredEvent(Phone Phone) : IDomainEvent;
public sealed record ContentUploadedEvent(Guid Id, string FileLocation) : IDomainEvent;
