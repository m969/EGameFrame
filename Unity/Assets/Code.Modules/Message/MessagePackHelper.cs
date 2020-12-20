using System;
using System.IO;
using FlatSharp;

namespace EGameFrame.Message
{
    public static class MessagePackHelper
    {
        public static void SerializeTo(ushort opcode, object obj, MemoryStream stream)
        {
            EGameFrame.FlatBuffers.FlatBuffersSerializeHelper.SerializeTo(opcode, obj, stream);
        }

        public static object DeserializeFrom(ushort opcode, byte[] bytes, int index, int count)
        {
            return EGameFrame.FlatBuffers.FlatBuffersSerializeHelper.DeserializeFrom(opcode, bytes, index, count);
        }

        public static object DeserializeFrom(ushort opcode, MemoryStream stream)
        {
            return EGameFrame.FlatBuffers.FlatBuffersSerializeHelper.DeserializeFrom(opcode, stream);
        }
    }
}