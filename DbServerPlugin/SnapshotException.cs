using System;

namespace DbServerPlugin
{
    using System.Runtime.CompilerServices;

    public class SnapshotException : Exception
    {
        public SnapshotException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
