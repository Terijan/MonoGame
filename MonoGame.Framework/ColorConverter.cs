using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Reflection;

namespace Microsoft.Xna.Framework
{
    public class ColorConverter : ExpandableObjectConverter
    {
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
                return new Color(Convert.ToByte(l_values[0]), Convert.ToByte(l_values[1]), Convert.ToByte(l_values[2]), Convert.ToByte(l_values[3]));
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is Color)
            {
                Color l_value = (Color)value;
                return string.Format("{0}{4} {1}{4} {2}{4} {3}", l_value.R, l_value.G, l_value.B, l_value.A, culture.TextInfo.ListSeparator);
            }

            if ((destinationType == typeof(InstanceDescriptor)) && (value is Color))
            {
                Color l_value = (Color)value;
                ConstructorInfo l_construct = typeof(Color).GetConstructor(new Type[] { typeof(byte), typeof(byte), typeof(byte), typeof(byte) });
                if (l_construct != null)
                {
                    return new InstanceDescriptor(l_construct, new object[] { l_value.R, l_value.G, l_value.B, l_value.A });
                }
            }
            
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
