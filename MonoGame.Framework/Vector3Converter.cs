using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;

namespace Microsoft.Xna.Framework
{
    public class Vector3Converter : ExpandableObjectConverter
    {
        private PropertyDescriptorCollection m_properties = null;

        public Vector3Converter()
        {
            Type l_type = typeof(Vector3);
            m_properties = new PropertyDescriptorCollection(new PropertyDescriptor[] {new FieldPropertyDescriptor(l_type.GetField("X")),new FieldPropertyDescriptor(l_type.GetField("Y")),new FieldPropertyDescriptor(l_type.GetField("Z"))});
        }

//         public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
//         {
//             return m_properties;
//         }

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
                return new Vector3(Convert.ToSingle(l_values[0]), Convert.ToSingle(l_values[1]), Convert.ToSingle(l_values[2]));
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is Vector3)
            {
                Vector3 l_vValue = (Vector3)value;
                return string.Format("{0}{3} {1}{3} {2}",l_vValue.X, l_vValue.Y, l_vValue.Z, culture.TextInfo.ListSeparator);
            }
            
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
