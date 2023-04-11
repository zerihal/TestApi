using Testable.Attributes;
using Testable.Base;

namespace TestApi.TestImplementations
{
    public class StringTest : TestableBase
    {
        public override Guid TestableID => new Guid("{88C77A11-0DA3-4C1E-9A7B-1E5D7F9F31F9}");

        public override string? Name => "StringTest";

        public override string? Description => "Simple string Testable implementation";

        public override string? Author => "JPS";

        [ReflectedMethod]
        public string Concat(string str1, string str2)
        {
            return str1 + str2;
        }
    }
}
