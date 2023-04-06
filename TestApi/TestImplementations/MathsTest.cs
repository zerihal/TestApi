using Testable.Attributes;
using Testable.Base;

namespace TestApi.TestImplementations
{
    public class MathsTest : TestableBase
    {
        public override string? Name => "MathsTest";

        public override Guid TestableID => new Guid("{B510C950-F320-4F4A-8D61-588896FC1534}");

        public override string? Description => "Simple maths Testable implementation";

        public override string? Author => "JPS";

        [ReflectedMethod]
        public int AddNumbers(int num1, int num2)
        {
            return num1 + num2;
        }

        [ReflectedMethod]
        public int GetRandomNumber(int min, int max) 
        {
            var ran = new Random();
            return ran.Next(min, max);
        }
    }
}
