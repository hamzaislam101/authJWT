using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace authJWT.Common
{
    public class APIResponse<T>
    {

        public int StatusCode { get; set; }

        public bool Success { get; set; }

        public T ModelData { get; set; }
    }
}
