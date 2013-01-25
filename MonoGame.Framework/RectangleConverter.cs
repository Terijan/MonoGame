using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Reflection;

namespace Microsoft.Xna.Framework
{
    public class RectangleConverter : ExpandableObjectConverter
    {
        private PropertyDescriptorCollection m_properties = null;

        public RectangleConverter()
        {
            Type l_type = typeof(Rectangle);
            m_properties = new PropertyDescriptorCollection(new PropertyDescriptor[] {new FieldPropertyDescriptor(l_type.GetField("X")),new FieldPropertyDescriptor(l_type.GetField("Y")),new FieldPropertyDescriptor(l_type.GetField("Width")),new FieldPropertyDescriptor(l_type.GetField("Height"))});
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
            return (destinationType == typeof(InstanceDescriptor)) || base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if(value is string)
            {
                string l_sValue = (string)value;
                string[] l_values = l_sValue.Split(new string[]{culture.TextInfo.ListSeparator},StringSplitOptions.None);
                return new Rectangle(Convert.ToInt32(l_values[0]), Convert.ToInt32(l_values[1]), Convert.ToInt32(l_values[2]), Convert.ToInt32(l_values[3]));
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is Rectangle)
            {
                Rectangle l_value = (Rectangle)value;
                return string.Format("{0}{4} {1}{4} {2}{4} {3}", l_value.X, l_value.Y, l_value.Width, l_value.Height, culture.TextInfo.ListSeparator);
            }

            if ((destinationType == typeof(InstanceDescriptor)) && (value is Rectangle))
            {
                Rectangle l_value = (Rectangle)value;
                ConstructorInfo l_construct = typeof(Rectangle).GetConstructor(new Type[] { typeof(int), typeof(int), typeof(int), typeof(int) });
                if (l_construct != null)
                {
                    return new InstanceDescriptor(l_construct, new object[] { l_value.X, l_value.Y, l_value.Width, l_value.Height });
                }
            }
            
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
