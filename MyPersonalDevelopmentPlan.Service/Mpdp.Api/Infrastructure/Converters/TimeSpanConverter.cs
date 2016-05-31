﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using Newtonsoft.Json;

namespace Mpdp.Api.Infrastructure.Converters
{
  public class TimeSpanConverter : JsonConverter
  {
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
      var ts = (TimeSpan)value;
      var tsString = XmlConvert.ToString(ts);
      serializer.Serialize(writer, tsString);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
        JsonSerializer serializer)
    {
      if (reader.TokenType == JsonToken.Null)
      {
        return null;
      }

      var value = serializer.Deserialize<String>(reader);
      return XmlConvert.ToTimeSpan(value);
    }

    public override bool CanConvert(Type objectType)
    {
      return objectType == typeof(TimeSpan) || objectType == typeof(TimeSpan?);
    }
  }
}