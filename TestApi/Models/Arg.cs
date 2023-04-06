namespace TestApi.Models
{
    public class Arg
    {
        public object? Object { get; set; } = null;

        public static bool TryParse(object obj, out Arg arg)
        {
            if (obj is null)
            {
                arg = default!;
                return false;
            }

            arg = new Arg { Object = obj };
            return true;
        }
    }
}
