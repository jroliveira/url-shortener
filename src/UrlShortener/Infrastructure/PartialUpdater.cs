﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace UrlShortener.Infrastructure
{
    public class PartialUpdater
    {
        public virtual void Apply<T>(dynamic partialModel, T destination)
        {
            var partialModelAsJToken = JToken.FromObject(partialModel);

            using (var reader = partialModelAsJToken.CreateReader())
            {
                JsonSerializer.CreateDefault().Populate(reader, destination);
            }
        }
    }
}