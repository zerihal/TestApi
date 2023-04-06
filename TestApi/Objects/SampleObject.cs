namespace TestApi.Objects
{
    public class SampleObject
    {
        public int Id { get; }
        public string Name { get; }
        public bool IsEnabled { get; set; }
        public SubSampleObj SubObj { get; }

        public SampleObject(int id, string name, bool isEnabled)
        {
            Id = id;
            Name = name;
            SubObj = new SubSampleObj($"{name}_sub");
            IsEnabled = isEnabled;
        }
    }
}
