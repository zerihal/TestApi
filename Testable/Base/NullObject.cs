using Testable.Interfaces;

namespace Testable.Base
{
    public class NullObject : INullObject
    {
        public string? Error { get; }

        public NullObject() { }

        public NullObject(string error) => Error = error;
    }
}
