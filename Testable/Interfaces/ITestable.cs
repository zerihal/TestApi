namespace Testable.Interfaces
{
    public interface ITestable
    {
        /// <summary>
        /// Testable internal ID.
        /// </summary>
        int ID { get; set; }

        /// <summary>
        /// Testable unique ID.
        /// </summary>
        Guid TestableID { get; }

        /// <summary>
        /// Testable class name.
        /// </summary>
        string? Name { get; }

        /// <summary>
        /// Testable class description.
        /// </summary>
        string? Description { get; }

        /// <summary>
        /// Author.
        /// </summary>
        string? Author { get; }

        /// <summary>
        /// Class methods.
        /// </summary>
        IEnumerable<string> Methods { get; }

        /// <summary>
        /// Class properties.
        /// </summary>
        IEnumerable<string> Properties { get; }

        /// <summary>
        /// Executes the given method with any arguments passed.
        /// </summary>
        /// <param name="method">Method name.</param>
        /// <param name="args">Arguments.</param>
        /// <param name="error">Execution error.</param>
        /// <returns>Method return value/object (if any).</returns>
        object ExecuteMethod(string method, object[] args, out string error);
    }
}
