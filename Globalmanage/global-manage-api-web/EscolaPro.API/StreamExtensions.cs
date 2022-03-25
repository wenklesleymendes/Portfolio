using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace EscolaPro.API
{
    public static class StreamExtensions
    {
        public static void SerializeTo<T>(this T o, Stream stream)
        {
            new BinaryFormatter().Serialize(stream, o);  // serialize o not typeof(T)
        }

        public static T Deserialize<T>(this Stream stream)
        {
            return (T)new BinaryFormatter().Deserialize(stream);
        }
    }
}
