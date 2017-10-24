using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace N5.Core
{
    public class JsonSerializer
    {
        public string Serialize(object o)
        {
            return JsonConvert.SerializeObject(o);
        }

        public T Deserialize<T>(string s) where T : class
        {
            return JsonConvert.DeserializeObject<T>(s);
        }
    }
}
