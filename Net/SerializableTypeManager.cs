/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Bendyline.Base
{
    public class SerializableTypeManager
    {
        private static SerializableTypeManager current;
        private readonly Dictionary<String, SerializableType> serializableTypes;
        private Type[] allTypes;

        public static SerializableTypeManager Current
        {
            get
            {
                if (current == null)
                {
                    current = new SerializableTypeManager();
                }

                return current;
            }
        }

        public SerializableTypeManager()
        {
            this.serializableTypes = new Dictionary<string, SerializableType>();
        }

        public bool HasType(string typeName)
        {
            return this.serializableTypes.ContainsKey(typeName);
        }

        public SerializableType GetTypeByName(string typeName)
        {
            if (this.serializableTypes.ContainsKey(typeName))
            {
                return this.serializableTypes[typeName];
            }

            return null;
        }

        public SerializableType Ensure(Type t)
        {
            String typeName = t.FullName;

            SerializableType st = null;

            if (this.serializableTypes.ContainsKey(typeName))
            {
                st = this.serializableTypes[typeName];
            }
            else
            {
                st = new SerializableType(this, t);
                this.allTypes = null;
                this.serializableTypes[typeName] = st;
            }

            Debug.Assert(st != null);

            return st;
        }

        public SerializableType EnsureSerializableType(object o)
        {
            Type t = o.GetType();

            String typeName = t.FullName;

            SerializableType st = null;

            if (this.serializableTypes.ContainsKey(typeName))
            {
                st = this.serializableTypes[typeName];
            }
            else
            {
                st = new SerializableType(this, t);
                this.allTypes = null;
                this.serializableTypes[typeName] = st;
            }

            Debug.Assert(st != null);

            return st;
        }


        internal Type[] GetAllTypes()
        {
            if (this.allTypes == null)
            {
                this.allTypes = new Type[this.serializableTypes.Count];
                int count = 0;

                foreach (KeyValuePair<String, SerializableType> kvp in this.serializableTypes)
                {
                    SerializableType st = kvp.Value;
                    this.allTypes[count] = st.Type;
                    count++;
                }
            }

            return this.allTypes;
        }
    }
}
