using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace da2mvc.injection
{
    class TypeMapping
    {
        public TypeMapping(Type map, Type mapTo, bool isSingleton)
        {
            Map = map;
            MapTo = mapTo;
            IsSingleton = isSingleton;
        }

        public Type Map { get; }
        public Type MapTo { get; }
        public bool IsSingleton { get; }
    }
}
