using System;

namespace Schmoli.Services.Core.Exceptions
{
    /// <summary>
    /// Exception thrown when an argument is in use
    /// (cannot be deleted, for example)
    /// </summary>
    public class ArgumentInUseException : Exception
    {
        public ArgumentInUseException(string argumentName)
        {
            ArgumentName = argumentName;
        }

        public string ArgumentName { get; }
    }
}
