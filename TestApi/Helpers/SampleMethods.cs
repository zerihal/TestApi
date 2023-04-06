using TestApi.Objects;

namespace TestApi.Helpers
{
    public static class SampleMethods
    {
        public static string TestStringOut() => "This is a test!";

        public static int TestIntOut() => 1;

        public static IEnumerable<int> TestIntArrayOut() => new[] { 1, 2 };

        public static IEnumerable<SampleObject> TestObjArrayOut() => new List<SampleObject>() 
        {
            new SampleObject(1, "TestObj1", true),
            new SampleObject(2, "TestObj2", false),
            new SampleObject(3, "TestObjXX", true)
        };
    }
}
