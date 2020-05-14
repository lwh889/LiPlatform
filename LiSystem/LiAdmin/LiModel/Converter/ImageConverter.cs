using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Newtonsoft.Json;
using LiCommon.Util;

namespace LiModel.Converter
{
    public class ImageConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return true;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return ImageUtil.byteArrayToImage(Convert.FromBase64String(Convert.ToString(reader.Value))); ;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer,Convert.ToBase64String( ImageUtil.imageToByteArray(value as Image)));
        }
    }
}
