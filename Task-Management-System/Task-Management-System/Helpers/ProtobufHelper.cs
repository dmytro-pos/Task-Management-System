using ProtoBuf;

namespace Task_Management_System.Helpers
{
    public static class ProtobufHelper
    {
        public static byte[] Serialize(object objectForSerialization) 
        {
            byte[] serializedData;
            using (MemoryStream stream = new MemoryStream())
            {
                Serializer.Serialize(stream, objectForSerialization);
                serializedData = stream.ToArray();
            }

            return serializedData;
        }

        public static T Deserialize<T>(byte[] objectForDeserialization)
        {
            using (MemoryStream stream = new MemoryStream(objectForDeserialization))
            {
                return Serializer.Deserialize<T>(stream);
            }
        }
    }
}
