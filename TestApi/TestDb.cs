using Microsoft.EntityFrameworkCore;
using Testable.Base;
using TestApi.TestImplementations;

namespace TestApi
{
    public class TestDb : DbContext
    {
        public TestDb(DbContextOptions<TestDb> options) : base(options) { }

        public DbSet<TestableBase> TestObjectImps => Set<TestableBase>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MathsTest>().HasBaseType<TestableBase>();
            //base.OnModelCreating(modelBuilder);
        }
    }
}
