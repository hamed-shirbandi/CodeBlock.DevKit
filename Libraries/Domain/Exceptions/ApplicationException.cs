namespace CodeBlock.DevKit.Domain.Exceptions;

/// <summary>
///
/// </summary>
public class ApplicationException : ManagedException
{
    #region Ctors


    /// <summary>
    ///
    /// </summary>
    public ApplicationException(string message)
        : base(message) { }

    /// <summary>
    ///
    /// </summary>
    public ApplicationException(string message, string metadata)
        : base(string.Format(message, metadata)) { }

    #endregion
}
