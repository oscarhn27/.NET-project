using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Common
{
    public class JWTSettings
    {
        public string Secret { get; set; }
        public int TokenLifeTime { get; set; }
    }
}
