namespace TestApi.Objects
{
    public class SubSampleObj
    {
        public Guid ObjId { get; }
        public string KeyName { get; }

        public SubSampleObj(string keyName)
        {
            ObjId = Guid.NewGuid();
            KeyName = keyName;
        }   
    }
}
