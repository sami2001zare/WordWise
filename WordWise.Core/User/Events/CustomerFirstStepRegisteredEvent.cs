using WordWise.Core.User.ValueObjects;
using WordWise.Framework;

namespace WordWise.Core.User.Events;

public sealed record CustomerFirstStepRegisteredEvent(Phone Phone) : IDomainEvent;
