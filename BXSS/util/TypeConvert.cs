namespace util
{
    using System;
    using System.ComponentModel;
    using System.Globalization;

    public static class TypeConvert
    {
        public static T Convert<T>(object obj)
            where T : IConvertible
        {
            return (T) Convert(typeof (T), obj);
        }

        public static object Convert(Type type, object obj)
        {
            var typeConverter = TypeDescriptor.GetConverter(type);
            return typeConverter.ConvertFrom(null, CultureInfo.InvariantCulture, obj);
        }
    }
}