using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace IFramework.Infrastructure.Transversal.Aspect.Log
{
    public class ComplexTypeConverter<T> : JsonConverter where T : Attribute
    {
        public override bool CanConvert(Type objectType)
        {
            return (typeof(iComplexType).IsAssignableFrom(objectType));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            object rootObject = Activator.CreateInstance(objectType);
            JToken objJSON = JToken.ReadFrom(reader);

            foreach (var token in objJSON)
            {
                PropertyInfo propInfo = rootObject.GetType().GetProperty(token.Path, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (propInfo.CanWrite)
                {
                    var tk = token as JProperty;
                    if (tk.Value is JObject)
                    {
                        JValue val = tk.Value.SelectToken("value") as JValue;
                        propInfo.SetValue(rootObject, Convert.ChangeType(val.Value, propInfo.PropertyType.UnderlyingSystemType), null);

                    }
                    else
                    {
                        propInfo.SetValue(rootObject, Convert.ChangeType(tk.Value, propInfo.PropertyType.UnderlyingSystemType), null);
                    }
                }
            }
            return rootObject;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var jo = new JObject();
            var type = value.GetType();
            foreach (PropertyInfo propInfo in type.GetProperties())
            {
                if (propInfo.CanRead)
                {
                    object propVal = propInfo.GetValue(value, null);

                    var cutomAttribute = propInfo.GetCustomAttribute<T>();
                    if (cutomAttribute != null)
                    {
                        jo.Add(propInfo.Name, JToken.FromObject(propVal ?? string.Empty, serializer));
                    }
                }
            }
            jo.WriteTo(writer);
        }
    }
}
