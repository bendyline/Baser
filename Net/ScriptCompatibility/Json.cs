using System;
using Newtonsoft.Json;

namespace Bendyline.Base.ScriptCompatibility
{
    public static class Json
    {
        public static object Parse(String jsonContent)
        {
            return JsonConvert.DeserializeObject(jsonContent);
        }
    }
}
