using System;
using System.IO;
using FlatSharp;

namespace EGameFrame.Message
{
    public static class MessagePackHelper
    {
        public static Action<ushort, object, MemoryStream> SerializeToAction { get; set; }
        public static void SerializeTo(ushort opcode, object obj, MemoryStream stream)
        {
            SerializeToAction?.Invoke(opcode, obj, stream);
            //FlatBuffersSerializeHelper.SerializeTo(opcode, obj, stream);
        }

        public static Func<ushort, byte[], int, int, object> DeserializeFromFunc { get; set; }
        public static object DeserializeFrom(ushort opcode, byte[] bytes, int index, int count)
        {
            return DeserializeFromFunc?.Invoke(opcode, bytes, index, count);
            //return FlatBuffersSerializeHelper.DeserializeFrom(opcode, bytes, index, count);
        }

        public static Func<ushort, MemoryStream, object> DeserializeFromFunc2 { get; set; }
        public static object DeserializeFrom(ushort opcode, MemoryStream stream)
        {
            return DeserializeFromFunc2?.Invoke(opcode, stream);
            //return FlatBuffersSerializeHelper.DeserializeFrom(opcode, stream);
        }
    }
}