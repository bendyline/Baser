/* Copyright (c) Bendyline LLC. All rights reserved. Licensed under the Apache License, Version 2.0.
    You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Diagnostics;
using System.Serialization;

namespace BL
{
    /*
       b = boolean
       i = integer
       d = date
       n = number
       y = byte (unsigned 0-255)
       o = object
       p = collection of objects
       e = element
       s = string
       u = url (string)
     */
    public partial class SerializableObject : INotifyPropertyChanged
    {
        private SerializableType serializableType;
        protected bool isInitialized;

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual String SerializationName
        {
            get
            {
                return null;
            }
        }

        public SerializableType SerializableType
        {
            get
            {
                return this.serializableType;
            }
        }

        public SerializableObject()
        {
            this.EnsureInitializedForSerialization();
        }

        protected virtual void InitType(SerializableType type)
        {

        }

        public void CopyFromObject(SerializableObject sourceObject)
        {
            this.ApplyObject(sourceObject.GetObject());
        }

        public String ToJson()
        {
            return Json.Stringify(this.GetObject());
        }

        public object GetObject()
        {
            ICollection<SerializableProperty> properties = this.SerializableType.Properties;

            object o = new object();

            foreach (SerializableProperty sp in properties)
            {
                Script.Literal(@"var sv={0}.get_name();var sd={0}.getShortTypeName();var st={0}.get_type();var fn=this['get_'+sd+'_'+sv];if (fn != null) {{ 
                if (st==10) {{var val=fn.apply(this); var arr = new Array(); var enumer=ss.IEnumerator.getEnumerator(val); while (enumer.moveNext()) {{ arr.push(enumer.current.getObject()); }} {1}[{0}.get_name()]=arr;  }}
                else if (st==5) {{var val=fn.apply(this); if (val != null) {{ val = val.getObject(); {1}[{0}.get_name()]=val; }}  }}
                else {{var val=fn.apply(this);if (val != null){{{1}[{0}.get_name()]=val;}}}}}}", sp, o);
            }

            return o;
        }

        public void ApplyString(String jsonSerialization)
        {
            object o = Json.Parse(jsonSerialization);

            if (o == null)
            {
                return;
            }

            this.ApplyObject(o);
        }


        public void LoadFromJson(String jsonObject)
        {
            object o = Json.Parse(jsonObject);

            if (o == null)
            {
                return;
            }

            this.ApplyObject(o);
        }

        public void ApplyObject(object o)
        {
            ICollection<SerializableProperty> properties = this.SerializableType.Properties;

            if (o == null)
            {
                throw new Exception("Applying empty object");
            }

            /*
             Uncomment for debugging serialization for certain objects..
            if (this.serializableType.Type.FullName == "QLA.AppList")
            {
                // Log.DA("Serializing app list");
            }*/

            foreach (SerializableProperty sp in properties)
            {
                /*REMOVED: val = new Date(Date.UTC(val.getFullYear(), val.getMonth(), val.getDate(), val.getHours(), val.getMinutes(), val.getSeconds(), val.getMilliseconds()));*/
                Script.Literal(@"var sv = {0}.get_name();var sd={0}.getShortTypeName();var st={0}.get_type(); if (st==6) 
{{var fn=this['get_'+sd+'_'+sv];if (fn != null) {{ var coll=fn.apply(this); if (coll != null) {{var val = {1}[sv]; coll.clear(); if (!(val === undefined) && val != null) {{for (var i=0; i<val.length; i++) {{ var item=val[i]; coll.add(item); }} }}  }} }}  }} else if (st==10)
{{var fn=this['get_'+sd+'_'+sv];if (fn != null) {{ var coll=fn.apply(this); if (coll != null) {{var val = {1}[sv]; coll.clear(); if (!(val === undefined) && val != null) {{for (var i=0; i<val.length; i++) {{ var item=val[i]; var newobj = coll.create(); newobj.applyObject(item); coll.add(newobj); }} }}  }} }}  }} else if (st == 7)
{{var fn=this['set_'+sd+'_'+sv];if (fn != null) {{ var val = {1}[sv]; if (!(val === undefined)) {{ val = new Date(parseInt(val.replace('/Date(', '').replace(')/', '')));  }} if (!(val === undefined)) {{ fn.apply(this, [val]); }}}}}} else if (st == 5)
{{var fn=this['get_'+sd+'_'+sv];if (fn != null) {{ var obj=fn.apply(this); if (obj != null) {{ var val = {1}[sv]; if (val != null && !(val === undefined)) {{ obj.applyObject(val); }}}}}}}} else
{{var fn=this['set_'+sd+'_'+sv];if (fn != null) {{ var val = {1}[sv]; if (!(val === undefined)) {{ fn.apply(this, [val]); }}}}}}", sp, o);
            }
        }

        public object GetProperty(String propertyName)
        {
            SerializableProperty sp = this.SerializableType.GetProperty(propertyName);

            Debug.Assert(sp != null, "Could not find property " + propertyName +" on type " + this.SerializableType.TagName);

            if (sp == null)
            {
                return null;
            }

            Script.Literal("var fn = this['get_' + {1} + '_' + {0}];  if (fn != null) {{return fn.apply(this, [] );}}", sp.Name, sp.GetShortTypeName());

            return null;
        }

        public void SetProperty(String propertyName, object value)
        {
            SerializableProperty sp = this.SerializableType.GetProperty(propertyName);

            Debug.Assert(sp != null);

            if (sp == null)
            {
                return;
            }

            int initial = propertyName.CharCodeAt(0);

            if (initial >= 65 && initial < 93)
            {
                propertyName = String.FromCharCode(initial+32) + propertyName.Substring(1, propertyName.Length);
            }

            // for lists of strings, convert a comma seperated list to a list of strings
            if (sp.Type == SerializablePropertyType.ScalarCollection && value is String)
            {
                String[] values = ((String)value).Split(',');

                List<String> valueList = new List<String>();

                foreach (String val in values)
                {
                    valueList.Add(val);
                }

                value = values;
            }

            value = sp.GetValue(value);

            Script.Literal("var fn = this['set_' + {1} + '_' + {0}];  if (fn != null) {{fn.apply(this, [{2}] );}}", sp.Name, sp.GetShortTypeName(), value);
        }

        private void EnsureInitializedForSerialization()
        {
            if (!isInitialized)
            {
                isInitialized = true;

                Type t = this.GetType();

                this.serializableType = SerializableTypeManager.Current.GetTypeByName(t.FullName);

                if (this.serializableType == null)
                {
                    this.serializableType = SerializableTypeManager.Current.Ensure(t);

                    if (!this.serializableType.IsInitialized)
                    {
                        this.serializableType.TagName = this.SerializationName;

                        this.serializableType.BeginInit();

                        /// NOTE NOTE NOTE 
                        /// ISSUE10001
                        /// If derived objects attempt to ScriptName/serialize a property defined in an implemented interface, do NOTE that the ScriptName (e.g., "s_title")
                        /// won't apply.  So if you are wondering why a property isn't getting serialized, check the underlying implemented interfaces to make sure
                        /// that property isn't defined there.

                        Script.Literal("for (var s in this)  {{ if (s.length > 6) {{ if (s.substring(0,4) == \"get_\" && s.charAt(5) == '_') {{ var sp = new BL.SerializableProperty(); sp.set_name(s.substring(6, s.length)); sp.setTypeByShortName(s.charAt(4)); {0}.addProperty(sp); }}}}}}", this.serializableType);
                        this.InitType(this.serializableType);

                        /*
                            Uncomment for debugging initialization of certain objects.
                        if (this.serializableType.Type.FullName == "QLA.AppList")
                        {
                            // Log.DA("Creating app list type");
                        }
                        */

                        this.serializableType.EndInit();
                    }
                }
            }
        }


        public int GetPropertyChangedEventCount()
        {
            int count = -1;

            Script.Literal("{0}={1}._targets.length / 2", count, this.PropertyChanged);

            return count;
        }

        public void ApplyTo(SerializableObject objectToApplyTo)
        {
            // this is a bit inefficient to get and then apply a JSON serialization, we can do something a bit more efficient here.
            object o = this.GetObject();

            objectToApplyTo.ApplyObject(o);
        }

        public bool IsEqualTo(SerializableObject compareTo)
        {
            if (compareTo.SerializableType != this.SerializableType)
            {
                return false;
            }

            object o = this.GetObject();
            String source = Json.Stringify(o);

            object compareObject = compareTo.GetObject();
            String compare = Json.Stringify(compareObject);

            return compare.CompareTo(source) == 0;
        }

        protected void NotifyPropertyChanged(String propertyName)
        {
            this.OnPropertyChanged(propertyName);

            if (this.PropertyChanged != null)
            {
                PropertyChangedEventArgs pcea = new PropertyChangedEventArgs(propertyName);

                this.PropertyChanged(this, pcea);
            }
        }

        protected virtual void OnPropertyChanged(String propertyName)
        {

        }

        protected void NotifyPropertyChangedWithValues(String propertyName, object previousValue, object newValue)
        {
            if (this.PropertyChanged != null)
            {
                PropertyChangedEventArgs pcea = new PropertyChangedEventArgs(propertyName);
                pcea.PreviousValue = previousValue;
                pcea.NewValue = newValue;
                this.PropertyChanged(this, pcea);
            }
        }
    }
}
