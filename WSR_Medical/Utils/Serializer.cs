using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace WSR_Medical.Utils
{
    class Serializer
    {
        public static T JsonDeSerialization<T>(string Json) where T : class
        {

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            return serializer.ReadObject(new MemoryStream(Encoding.Default.GetBytes(Json))) is T obj ? obj : null;
        }

        public static string JsonSerialization<T>(T obj) where T : class
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, obj);
                return Encoding.Default.GetString(ms.ToArray());
            }
        }
    }
}
