using System;

namespace da2mvc.core.injection
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
