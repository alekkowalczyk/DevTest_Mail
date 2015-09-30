using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveloperTest.Utils
{
    public static class ExtensionMethods
    {
        public static int Count(this IEnumerable source)
        {
            int res = 0;

            foreach (var item in source)
                res++;

            return res;
        }
    }
}
