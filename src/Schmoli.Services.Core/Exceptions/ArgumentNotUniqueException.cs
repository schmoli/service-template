using System;

namespace Schmoli.Services.Core.Exceptions
{
    /// <summary>
    /// Exception thrown when a unique value is in use.
    /// </summary>
    public class ArgumentNotUniqueException : Exception
    {
        public ArgumentNotUniqueException(string argumentName)
        {
            ArgumentName = argumentName;
        }

        public string ArgumentName { get; }
    }
}
