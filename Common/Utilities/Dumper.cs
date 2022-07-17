using Newtonsoft.Json;

namespace Common.Utilities
{
    public static class Dumper
    {
        public static string Dump(this object obj)
        {
            return (string)(JsonConvert.SerializeObject(obj, Newtonsoft.Json.Formatting.Indented)); 
        }
    }
}