using System;

namespace MyProject.Tools
{
    public static class ToHelper
    {
        public static string ToSafeString(this object obj)
        {
            if (obj == null)
            {
                return "";
            }

            return obj.ToString();
        }

        public static Int32 ToSafeInt32(this object obj,int defaultValue=0)
        {
            if (obj == null)
            {
                return defaultValue;
            }
            if (Int32.TryParse(obj.ToString(), out Int32 i))
            {
                return i;
            }
            else
            { 
               return defaultValue;
            }
        }

        public static Int64 ToSafeInt64(this object obj, int defaultValue = 0)
        {
            if (obj == null)
            {
                return defaultValue;
            }
            if (Int64.TryParse(obj.ToString(), out Int64 i))
            {
                return i;
            }
            else
            {
                return defaultValue;
            }
        }
    }
}
