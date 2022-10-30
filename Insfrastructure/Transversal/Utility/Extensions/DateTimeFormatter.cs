using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IFramework.Infrastructure.Utility.Extensions
{
    /// <summary>
    /// JSON serialization ve deserialization yapılırken tarihi formatlamak için kullanılacak class.
    /// </summary>
    public class DateTimeFormatter : DateTimeConverterBase
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            try
            {
                writer.WriteValue(value.To<DateTime>());
            }
            catch
            {
                writer.WriteValue(string.Empty);
            }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                return DateTime.Parse(reader.Value.ToString());
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
    }
}
