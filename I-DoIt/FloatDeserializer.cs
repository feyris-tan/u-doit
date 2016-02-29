using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace u_doit.I_DoIt
{
    /// <summary>
    /// Aus irgendeinem Grund serialisiert i-doit floats nicht mit einfach z.b. 100.0
    /// sondern so:
    /// 
    /// <code>"frequency": {
    ///  "title": 100
    /// },</code>
    /// 
    /// </summary>
    class FloatDeserializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType.Equals(typeof (Single))) return true;
            if (objectType.Equals(typeof(Double))) return true;
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string propName = String.Empty;
            object WhatWeWant = float.NaN;
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
                    case JsonToken.Integer:
                    case JsonToken.String:
                        if (propName.Equals("title"))
                        {
                            if (objectType.Equals(typeof (Single)))
                            {
                                WhatWeWant = Convert.ToSingle(reader.Value.ToString());
                            }
                            else
                            {
                                WhatWeWant = Convert.ToDouble(reader.Value.ToString());
                            }
                        }
                        break;
                    case JsonToken.EndArray:
                        if (objectType.Equals(typeof (Single)))
                        {
                            return 0.0f;
                        }
                        else
                        {
                            return 0.0;
                        }
                        break;
                    default:
                        throw new NotImplementedException(reader.TokenType.ToString());
                }
                reader.Read();
            }
            return WhatWeWant;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value.GetType().Equals(typeof (Single)))
            {
                writer.WriteValue((float) value);
            }
            else if (value.GetType().Equals(typeof (Double)))
            {
                writer.WriteValue((double)value);
            }
            else
            {
                throw new NotImplementedException(string.Format("Don't know what a {0} is.", value.GetType()));
            }
        }
    }
}
