namespace CodeBlock.DevKit.Domain.ValueObjects;

public abstract record BaseValueObject
{
    protected abstract void CheckPolicies();
}
