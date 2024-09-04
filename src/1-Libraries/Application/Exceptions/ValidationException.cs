using CodeBlock.DevKit.Core.Exceptions;

namespace CodeBlock.DevKit.Application.Exceptions;

/// <summary>
///
/// </summary>
public class ValidationException : ManagedException
{
    #region Ctors

    /// <summary>
    ///
    /// </summary>
    public ValidationException()
        : base("") { }

    /// <summary>
    ///
    /// </summary>
    public ValidationException(string message)
        : base(message) { }

    #endregion
}
