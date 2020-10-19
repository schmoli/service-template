using System;

namespace Schmoli.Services.Core.Exceptions
{
    /// <summary>
    /// Exception thrown when a unique value is in use.
    /// </summary>
    public class ArgumentNotUniqueException : Exception
    {
        public string ArgumentName { get; private set; }

        public ArgumentNotUniqueException(string argumentName)
        {
            ArgumentName = argumentName;
        }
    }
}
