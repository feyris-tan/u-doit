using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace u_doit.I_DoIt
{
    class EnumDeserializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType.IsEnum) return true;
            if (objectType.Equals(typeof (Int32))) return true;
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string propName = String.Empty;
            object WhatWeWant = null;
            bool ended = false;
            while (reader.TokenType != JsonToken.EndObject)
            {
                switch (reader.TokenType)
                {
                    case JsonToken.Null:
                    case JsonToken.StartObject:
                    case JsonToken.StartArray:
                        break;
                    case JsonToken.PropertyName:
                        propName = reader.Value.ToString();
                        break;
                    case JsonToken.String:
                        if (propName.Equals("id"))
                            WhatWeWant = Convert.ToInt32(reader.Value.ToString());
                        else
                        {
                        }
                        break;
                    case JsonToken.EndArray:
                        return 0;
                    case JsonToken.Boolean:
                        if (reader.Value.Equals(true))
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    default:
                        throw new NotImplementedException(reader.TokenType.ToString());
                }
                reader.Read();
            }
            if (WhatWeWant == null) return 0;
            return WhatWeWant;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue((int) value);
            return;
        }
    }
}
