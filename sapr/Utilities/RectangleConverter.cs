using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Ink;
using System.Windows.Media;

namespace sapr.Utilities
{
    public class RectangleConverter : JsonConverter<Rectangle>
    {
        public override void WriteJson(JsonWriter writer, Rectangle value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("Width");
            writer.WriteValue(value.Width);
            writer.WritePropertyName("Height");
            writer.WriteValue(value.Height);
            writer.WritePropertyName("StrokeThickness");
            writer.WriteValue(value.StrokeThickness);

            writer.WriteEndObject();
        }

        public override Rectangle ReadJson(JsonReader reader, Type objectType, Rectangle existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            JObject obj = JObject.Load(reader);
            double width = (double)obj["Width"];
            double height = (double)obj["Height"];
            double strokeT = (double)obj["StrokeThickness"];
            // Инициализация новых значений
            return new Rectangle { Width = width, Height = height, StrokeThickness = strokeT};
        }
    }
}
