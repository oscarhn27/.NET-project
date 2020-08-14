using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aquaservice.Common.LoopbackFilter
{
    public class LoopbackFilter
    {
        public Dictionary<string, string> Where { get; set; }
        public int Limit { get; set; }
        public int Skip { get; set; }
        public string Order { get; set; }
        public bool Ascending { get; set; }
        public IEnumerable<string> Fields { get; set; }

        public LoopbackFilter()
        {
            Where = new Dictionary<string, string>();
            Fields = new List<string>();
        }

        public string GetStringFromKey(string key)
        {
            return Where.ContainsKey(key) ? Where[key] : string.Empty;
        }

        public int GetIntFromKey(string key)
        {
            int result = 0;

            try
            {
                int.TryParse(GetStringFromKey(key), out result);
            }
            catch
            {
            }

            return result;
        }

        public float GetFloatFromKey(string key)
        {
            float result = 0;

            try
            {
                float.TryParse(GetStringFromKey(key), out result);
            }
            catch
            {
            }

            return result;
        }
    }
}
