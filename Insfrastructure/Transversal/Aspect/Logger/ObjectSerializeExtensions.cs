using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace IFramework.Infrastructure.Transversal.Aspect.Log
{
    public static class ObjectSerializeExtensions
    {
        public static string ToSerializeIgnoreAttribute<T>(this object parameter) where T : Attribute
        {
            string jsonObject = JsonConvert.SerializeObject(parameter, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                Converters = new List<JsonConverter>() { new ComplexTypeConverter<T>() }
            });
            return jsonObject;
        }
    }
}
