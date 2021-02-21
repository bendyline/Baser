/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;

namespace BL
{
    public enum SerializablePropertyType
    {
        String = 0,
        Integer = 1,
        Choice = 2,
        Bool = 3,
        Number = 4,
        Object = 5,
        VariantArray = 6,
        Date = 7,
        SmallInteger = 8,
        UrlString = 9,
        ObjectCollection = 10,
        StringArray = 11, 
        IntegerArray = 12
    }
}
