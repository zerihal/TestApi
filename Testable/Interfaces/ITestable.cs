namespace Testable.Interfaces
{
    public interface ITestable
    {
        int ID { get; set; }
        Guid TestableID { get; }
        string? Name { get; }
        string? Description { get; }
        string? Author { get; }
        IEnumerable<string> Methods { get; }
        IEnumerable<string> Properties { get; }

        object ExecuteMethod(string method, object[] args, out string error);
    }
}
