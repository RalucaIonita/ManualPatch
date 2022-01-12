using System;

namespace PatchLibrary.Exceptions
{
    public class OpperationCannotBeDoneException : Exception
    {
        public OpperationCannotBeDoneException(string message) : base(message)
        {
        }
    }
}
