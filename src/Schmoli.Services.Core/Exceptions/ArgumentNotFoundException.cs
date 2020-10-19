using System;

namespace Schmoli.Services.Core.Exceptions
{
    public class ArgumentNotFoundException : Exception
    {
        public ArgumentNotFoundException(string argumentName)
        {
            ArgumentName = argumentName;
        }

        public string ArgumentName { get; }
    }
}
