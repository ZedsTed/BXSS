namespace util
{
    using System;

    public static class ThrowIf
    {
        public static void Null<T>(T obj, string desc = null)
            where T : class
        {
            if(obj == null)
                throw string.IsNullOrEmpty(desc) ? new ArgumentNullException() : new ArgumentNullException(desc);
        }

        public static void NullOrEmpty(string obj, string desc = null)
        {
            if (obj == null)
                throw string.IsNullOrEmpty(desc) ? new ArgumentNullException() : new ArgumentNullException(desc);
        }
    }
}
