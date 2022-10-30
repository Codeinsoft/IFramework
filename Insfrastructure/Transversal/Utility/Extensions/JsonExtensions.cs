using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IFramework.Infrastructure.Utility.Extensions
{
    /// <summary>
    /// JSON serialization ve deserilization işlemlerini yapan class.
    /// </summary>
    public static class JsonExtensions
    {
        public static string ToJson(this object data)
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Converters = new List<JsonConverter>
                        {
                            new DateTimeFormatter()
                        }
            };

            return JsonConvert.SerializeObject(data, jsonSerializerSettings);
        }

        public static string ToJson(this object data, bool customDateFormatter)
        {
            return customDateFormatter
                       ? JsonConvert.SerializeObject(data, new DateTimeFormatter())
                       : JsonConvert.SerializeObject(data);
        }

        public static T ToObject<T>(this string jsonString)
        {
            try
            {
                JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                    //,
                    //Converters = new List<JsonConverter>
                    //    {
                    //        new DateTimeFormatter()
                    //    }
                };

                return !string.IsNullOrEmpty(jsonString)
                    ? JsonConvert.DeserializeObject<T>(jsonString, jsonSerializerSettings)
                    : default(T);
            }
            catch
            {
                return default(T);
            }
        }

        public static object ToObject(this string jsonString, Type typeToDeserialize)
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Converters = new List<JsonConverter>
                        {
                            new DateTimeFormatter()
                        }
            };

            return !string.IsNullOrEmpty(jsonString)
                ? JsonConvert.DeserializeObject(jsonString, typeToDeserialize, jsonSerializerSettings)
                : null;
        }
    }
}
