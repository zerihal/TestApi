using System.Reflection;
using System.Text.Json;
using Testable.Attributes;
using Testable.Interfaces;

namespace Testable.Base
{
    public abstract class TestableBase : ITestable
    {
        private const BindingFlags _flags = BindingFlags.Public | BindingFlags.Instance;

        public int ID { get; set; }

        public abstract Guid TestableID { get; }

        public abstract string? Name { get; }

        public abstract string? Description { get; }

        public abstract string? Author { get; }

        public IEnumerable<string> Methods
        {
            get
            {
                var methods = new List<string>();

                foreach (var meth in GetType().GetMethods(_flags))
                {
                    if (meth.GetCustomAttribute<ReflectedMethod>() == null)
                        continue;

                    var args = string.Empty;

                    foreach (var arg in meth.GetParameters())
                    {
                        var aType = arg.ParameterType.Name;
                        var aName = arg.Name ?? "param";

                        args += $" {aType} {aName},";
                    }

                    if (args != string.Empty)
                        args = args.TrimStart().TrimEnd(',');

                    methods.Add($"{meth.Name}({args})");
                }

                return methods;
            }
        }

        public IEnumerable<string> Properties
        {
            get
            {
                var properties = new List<string>();

                foreach (var prop in GetType().GetProperties(_flags))
                {
                    if (prop.GetCustomAttribute<ReflectedProperty>() == null)
                        continue;

                    var type = prop.PropertyType.Name;
                    var propName = prop.Name;

                    properties.Add($"{type} {propName}");
                }

                return properties;
            }
        }

        public object ExecuteMethod(string method, object[] args, out string error)
        {
            error = string.Empty;
            var meth = GetType().GetMethod(method, _flags);

            if (meth != null)
            {
                var methArgs = meth.GetParameters();
                var inputArgs = ConvertJsonArgs(methArgs, args);

                if (methArgs.Length == inputArgs.Length)
                {
                    try
                    {
                        var rtn = meth.Invoke(this, inputArgs);

                        if (rtn == null)
                            return new object();
                        else
                            return rtn;
                    }
                    catch (Exception ex)
                    {
                        error = ex.Message;
                    }
                }
                else
                {
                    error = "Unable to convert input args for method. Ensure that these are " +
                        "ordered correctly";
                }
            }
            else
            {
                error = $"Method ({method}) not found";
            }

            return new NullObject();
        }

        private object[] ConvertJsonArgs(ParameterInfo[] methParams, object[] args)
        {
            var rtnObjs = new List<object>();

            if (methParams.Length == args.Length)
            {
                for (var i = 0; i < methParams.Length; i++) 
                {
                    var baseType = methParams[i].ParameterType;

                    if (args[i] is JsonElement jsonObj)
                    {
                        try
                        {
                            var json = jsonObj.GetRawText();
                            var deserialized = JsonSerializer.Deserialize(json, baseType);

                            if (deserialized != null)
                                rtnObjs.Add(deserialized);
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        rtnObjs.Add(args[i]);
                    }
                }
            }

            return rtnObjs.ToArray();
        }
    }
}
