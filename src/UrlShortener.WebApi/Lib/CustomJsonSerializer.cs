﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace UrlShortener.WebApi.Lib
{
    public class CustomJsonSerializer : JsonSerializer
    {
        public CustomJsonSerializer()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            ContractResolver = new CamelCasePropertyNamesContractResolver();
            Converters.Add(new StringEnumConverter { CamelCaseText = true });
            Formatting = Formatting.Indented;
            TypeNameHandling = TypeNameHandling.Auto;
            // ReSharper restore DoNotCallOverridableMethodsInConstructor
        }
    }
}
