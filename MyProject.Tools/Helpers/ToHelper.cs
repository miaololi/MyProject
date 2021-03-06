using System;
using System.Text.Json;
using System.Text.Json.Serialization;

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

    public class DatetimeJsonConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                if (DateTime.TryParse(reader.GetString(), out DateTime date))
                    return date;
            }
            return reader.GetDateTime();
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
