using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace u_doit.I_DoIt
{
    /// <summary>
    /// Aus irgendeinem Grund serialisiert i-doit Ja/Nein Werte nicht mit true oder false, sondern mit so:
    /// <code>
    /// "system_drive": {
    ///  "value": "1",
    ///  "title": "Yes"
    ///},</code>
    /// 
    /// Von daher ist das hier leider nötig...
    /// 
    /// </summary>
    class BrokenInt32Deserializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            if (objectType.Equals(typeof (Int32))) return true;
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string propName = String.Empty;
            int WhatWeWant = 0;
            bool ended = false;
            while (reader.TokenType != JsonToken.EndObject)
            {
                switch (reader.TokenType)
                {
                    case JsonToken.Null:
                        WhatWeWant = 0;
                        break;
                    case JsonToken.StartObject:
                    case JsonToken.StartArray:
                        break;
                    case JsonToken.PropertyName:
                        propName = reader.Value.ToString();
                        break;
                    case JsonToken.Integer:
                        WhatWeWant = (Int32)reader.Value;
                        break;
                    case JsonToken.String:
                        if (propName.Equals("value"))
                        {
                            WhatWeWant = Convert.ToInt32(reader.Value.ToString());
                        }
                        break;
                    case JsonToken.EndArray:
                        return 0;
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
            if (value.GetType().Equals(typeof (Int32)))
            {
                writer.WriteValue((int) value);
            }
            else
            {
                throw new NotImplementedException(string.Format("Don't know what a {0} is.", value.GetType()));
            }
        }
    }
}
