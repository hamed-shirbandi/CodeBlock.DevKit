namespace CodeBlock.DevKit.Domain.ValueObjects;

/// <summary>
///
/// </summary>
public record CreationTime : BaseValueObject
{
    public DateTime CreateDateTime { get; private set; }
    public DateTime ModifiedDateTime { get; private set; }
    public int CreateDay { get; private set; }
    public int CreateMonth { get; private set; }
    public int CreateYear { get; private set; }

    private CreationTime(DateTime dateTime)
    {
        CreateDateTime = dateTime;
        ModifiedDateTime = CreateDateTime;
        CreateDay = CreateDateTime.Day;
        CreateMonth = CreateDateTime.Month;
        CreateYear = CreateDateTime.Year;
    }

    public static CreationTime CreateNowDateTime()
    {
        return new CreationTime(DateTime.Now);
    }

    public CreationTime UpdateModifiedDateTime()
    {
        return new CreationTime(DateTime.Now);
    }

    protected override void CheckPolicies() { }
}
