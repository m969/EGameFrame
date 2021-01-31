using System;
using System.IO;
using ET;
using FlatSharp;

namespace EGameFrame.Message
{
    public static class MessageSerializeHelper
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

        public static MemoryStream GetStream(int count = 0)
        {
            MemoryStream stream;
            if (count > 0)
            {
                stream = new MemoryStream(count);
            }
            else
            {
                stream = new MemoryStream();
            }

            return stream;
        }

        public static (ushort, MemoryStream) MessageToStream(object message, int count = 0)
        {
            MemoryStream stream = GetStream();

            ushort opcode = OpcodeTypeComponent.Instance.GetOpcode(message.GetType());

            //stream.Seek(Packet.OpcodeLength, SeekOrigin.Begin);
            //stream.SetLength(Packet.OpcodeLength);

            stream.GetBuffer().WriteTo(0, opcode);

            MessageSerializeHelper.SerializeTo(opcode, message, stream);

            stream.Seek(0, SeekOrigin.Begin);
            return (opcode, stream);
        }

        public static (ushort, MemoryStream) MessageToStream(long actorId, object message, int count = 0)
        {
            //int actorSize = sizeof(long);
            MemoryStream stream = GetStream();

            ushort opcode = OpcodeTypeComponent.Instance.GetOpcode(message.GetType());

            //stream.Seek(actorSize + Packet.OpcodeLength, SeekOrigin.Begin);
            //stream.SetLength(actorSize + Packet.OpcodeLength);

            //// –¥»ÎactorId
            //stream.GetBuffer().WriteTo(0, actorId);
            //stream.GetBuffer().WriteTo(actorSize, opcode);

            MessageSerializeHelper.SerializeTo(opcode, message, stream);

            stream.Seek(0, SeekOrigin.Begin);
            return (opcode, stream);
        }
    }
}