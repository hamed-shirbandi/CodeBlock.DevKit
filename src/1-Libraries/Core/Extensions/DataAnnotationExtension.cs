using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CodeBlock.DevKit.Core.Extensions;

public static class DataAnnotationExtension
{
    public static bool Validate<TObject>(this TObject obj, out ICollection<ValidationResult> results)
    {
        results = new List<ValidationResult>();

        return Validator.TryValidateObject(obj, new ValidationContext(obj), results, true);
    }

    public static string GetDisplayName(this Enum enumValue)
    {
        return enumValue.GetType().GetMember(enumValue.ToString()).First().GetCustomAttribute<DisplayAttribute>().GetName();
    }
}
