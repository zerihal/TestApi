using Microsoft.EntityFrameworkCore;
using Testable.Base;
using Testable.Interfaces;
using TestApi.TestImplementations;

namespace TestApi
{
    public class TestDb : DbContext
    {
        public TestDb(DbContextOptions<TestDb> options) : base(options) { }

        public DbSet<TestableBase> TestObjectImps => Set<TestableBase>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Get all types of ITestable in the app current domain and, excluding abstract (TestableBase) implementations
            // and the interface itself, set base type of TestableBase so that these can be populated in in the DB set 
            // (i.e. the TestObjectImps).

            var type = typeof(ITestable);
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(type.IsAssignableFrom);

            foreach (var iTestableType in types) 
            {
                if (!(iTestableType.IsAbstract || iTestableType.IsInterface))
                {
                    modelBuilder.Entity(iTestableType).HasBaseType(typeof(TestableBase));
                }
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
