namespace Tracker.Domain.Exceptions;

public class BusinessRuleException : DomainException
{
    public string RuleName {get; set;}

    public BusinessRuleException(string ruleName, string message) 
    : base(message, 403)
    {
        RuleName = ruleName;
    }

}
