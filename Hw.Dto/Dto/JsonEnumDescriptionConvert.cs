using System.ComponentModel;
using System.IO;
using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Hw.Dto.Dto
{
    public class JsonEnumDescriptionConvert : System.Text.Json.Serialization.JsonConverter<Hw.Model.MenuType>
    {
        public override Hw.Model.MenuType Read(
                  ref Utf8JsonReader reader,
                 Type typeToConvert,
                  JsonSerializerOptions options)
        {

            return (Hw.Model.MenuType)Enum.Parse(typeof(Hw.Model.MenuType), reader.GetString(), true);

        }

        public override void Write(
            Utf8JsonWriter writer,
            Hw.Model.MenuType value,
            JsonSerializerOptions options)
        {
            Type enumType = value.GetType();
            string name = Enum.GetName(enumType, value);
            if (name != null)
            {
                System.Reflection.FieldInfo fileInfo = enumType.GetField(name);
                if (fileInfo != null)
                {
                    DescriptionAttribute attr = Attribute.GetCustomAttribute(fileInfo, typeof(DescriptionAttribute), false) as DescriptionAttribute;
                    if (attr != null)
                        writer.WriteStringValue(attr?.Description);
                    return;
                }
            }


        }
    }

}