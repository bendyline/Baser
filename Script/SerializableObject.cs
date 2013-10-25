/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Diagnostics;

namespace BL
{
    /*
       b = boolean
       i = integer
       d = date
       n = number
       y = byte (unsigned 0-255)
       o = object
       e = element
       s = string
       u = url (string)
     */
    public partial class SerializableObject
    {
        private SerializableType serializableType;
        protected bool isInitializedForSerialization;

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual String SerializationName
        {
            get
            {
                return null;
            }
        }

        protected SerializableType SerializableType
        {
            get
            {
                return this.serializableType;
            }
        }

        public String Xml
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                return sb.ToString();
            }

            set
            {
            }
        }

        public SerializableObject()
        {
            this.EnsureInitializedForSerialization();
        }

        protected virtual void InitForSerialization()
        {

        }

        public object GetObject()
        {
            ICollection<SerializableProperty> properties = this.SerializableType.Properties;

            object o = new object();

            foreach (SerializableProperty sp in properties)
            {
                Script.Literal("var sv={0}.get_name();var sd={0}.getShortTypeName();var fn=this['get_'+sd+'_'+sv];if (fn != null) {{ var val=fn(); if (val != null){{o[{0}.get_name()]=val;}}}}", sp, o);
            }

            return o;
        }

        public void ApplyObject(object o)
        {
            ICollection<SerializableProperty> properties = this.SerializableType.Properties;

            foreach (SerializableProperty sp in properties)
            {
                Script.Literal(@"var sv = {0}.get_name();var sd={0}.getShortTypeName();var st={0}.get_type(); if (st==6) 
{{var fn=this['get_'+sd+'_'+sv];if (fn != null) {{ var coll=fn.apply(this); if (coll != null) {{var val = {1}[sv]; if (!(val === undefined)) {{coll.clear(); for (var i=0; i<val.length; i++) {{ var item=val[i]; coll.add(item); }} }}  }} }}  }} else if (st==10)
{{var fn=this['get_'+sd+'_'+sv];if (fn != null) {{ var coll=fn.apply(this); if (coll != null) {{var val = {1}[sv]; if (!(val === undefined)) {{coll.clear(); for (var i=0; i<val.length; i++) {{ var item=val[i]; var newobj = coll.create(); newobj.applyObject(item); coll.add(newobj); }} }}  }} }}  }} else 
{{var fn=this['set_'+sd+'_'+sv];if (fn != null) {{ var val = {1}[sv]; if (!(val === undefined)) {{ fn.apply(this, [val]); }}}}}}", sp, o);
            }
        }

        public void SetProperty(String propertyName, object value)
        {
            SerializableProperty sp = this.SerializableType.GetProperty(propertyName);
            Debug.Assert(sp != null);

            int initial = propertyName.CharCodeAt(0);

            if (initial >= 65 && initial < 93)
            {
                propertyName = String.FromCharCode(initial+32) + propertyName.Substring(1, propertyName.Length);
            }

            Script.Literal("var fn = this['set_' + {1} + '_' + {0}];  if (fn != null) {{fn.apply(this, [{2}] );}}", propertyName, sp.GetShortTypeName(), value);
        }

        private void EnsureInitializedForSerialization()
        {
            if (!isInitializedForSerialization)
            {
                isInitializedForSerialization = true;

                Type t = this.GetType();

                this.serializableType = SerializableTypeManager.Current.GetTypeByName(t.FullName);

                if (this.serializableType == null)
                {
                    this.serializableType = SerializableTypeManager.Current.Ensure(t);
                    this.serializableType.TagName = this.SerializationName;
                    this.serializableType.BeginInit();

                    Script.Literal("for (var s in this)  {{ if (s.length > 6) {{ if (s.substring(0,4) == \"get_\" && s.charAt(5) == '_') {{ var sp = new BL.SerializableProperty(); sp.set_name(s.substring(6, s.length)); sp.setTypeByShortName(s.charAt(4)); {0}.addProperty(sp); }}}}}}", this.serializableType);
                    this.InitForSerialization();
                    this.serializableType.EndInit();
                }
            }
        }

        protected void NotifyPropertyChanged(String propertyName)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChangedEventArgs pcea = new PropertyChangedEventArgs(propertyName);

                this.PropertyChanged(this, pcea);
            }
        }
    }
}
