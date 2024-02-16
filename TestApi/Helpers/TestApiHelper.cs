using System.Reflection;
using Testable.Base;
using Testable.Interfaces;

namespace TestApi.Helpers
{
    public static class TestApiHelper
    {
        private static BindingFlags _flags = BindingFlags.Public | BindingFlags.Instance;

        public static async void PrePopDb(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var ts = scope.ServiceProvider.GetRequiredService<TestDb>();

                // Get all stock implementations of ITestBase, create an instance for each and add to the
                // database. 

                var type = typeof(ITestable);
                var testBasetypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()).
                    Where(t => type.IsAssignableFrom(t) && !t.IsInterface);

                foreach (var testBaseType in testBasetypes)
                {
                    var ctor = testBaseType.GetConstructor(_flags, new Type[] { });
                    if (ctor != null)
                    {
                        var instance = (ITestable)ctor.Invoke(null);

                        if (instance != null && instance.TestableID != Guid.Empty)
                            ts.TestObjectImps.Add((TestableBase)instance);
                    }
                }

                await ts.SaveChangesAsync();
            }
        }

        public static void ReloadDb(WebApplication app)
        {
            // ToDo ...
        }

        public static void AddNewTestObjects(WebApplication app)
        {
            // ToDo ...
        }
    }
}
