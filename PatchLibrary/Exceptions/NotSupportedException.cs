using System;

namespace PatchLibrary.Exceptions
{
    public class NotSupportedException : Exception
    {
        public NotSupportedException(string message) : base(message)
        { }
    }
}
