using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Microsoft.Xna.Framework
{
    public class Vector2Converter : ExpandableObjectConverter
    {
        private PropertyDescriptorCollection m_properties = null;

        public Vector2Converter()
        {
            Type l_type = typeof(Vector2);
            m_properties = new PropertyDescriptorCollection(new PropertyDescriptor[] {new FieldPropertyDescriptor(l_type.GetField("X")),new FieldPropertyDescriptor(l_type.GetField("Y"))});
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return m_properties;
        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if(value is string)
            {
                string l_sValue = (string)value;
                string[] l_values = l_sValue.Split(new string[]{culture.TextInfo.ListSeparator},StringSplitOptions.None);
                return new Vector2(Convert.ToSingle(l_values[0]), Convert.ToSingle(l_values[1]));
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is Vector2)
            {
                Vector2 l_vValue = (Vector2)value;
                return string.Format("{0}{2} {1}",l_vValue.X, l_vValue.Y, culture.TextInfo.ListSeparator);
            }
            
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
