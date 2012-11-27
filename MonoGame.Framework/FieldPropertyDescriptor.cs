using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace Microsoft.Xna.Framework
{
    public class FieldPropertyDescriptor : PropertyDescriptor
    {
        private FieldInfo m_field = null;
        public FieldPropertyDescriptor(FieldInfo i_field) :
            base(i_field.Name,(Attribute[])i_field.GetCustomAttributes(typeof(Attribute),true))
        {
            m_field = i_field;
        }

        public override object GetValue(object component)
        {
            return m_field.GetValue(component);
        }

        public override void SetValue(object component, object value)
        {
            m_field.SetValue(component, value);
            OnValueChanged(component, EventArgs.Empty);
        }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }

        public override void ResetValue(object component)
        {
        }

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override bool IsReadOnly
        {
            get { return false; }
        }

        public override Type ComponentType
        {
            get { return m_field.DeclaringType; }
        }

        public override Type PropertyType
        {
            get { return m_field.FieldType; }
        }
    }
}
