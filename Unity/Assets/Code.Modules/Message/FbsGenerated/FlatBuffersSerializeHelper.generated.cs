
using System;
using System.IO;
using FlatSharp;

namespace EGameFrame.FlatBuffers
{
    public static class FlatBuffersSerializeHelper
    {
        public static void SerializeTo(ushort opcode, object obj, MemoryStream stream)
        {
            switch (obj)
            {
				case EGameFrame.Message.FooBarContainer a: EGameFrame.Message.FooBarContainer.Serializer.Write(stream.GetBuffer(), a);break;
				case EGameFrame.Message.LoginRequest a: EGameFrame.Message.LoginRequest.Serializer.Write(stream.GetBuffer(), a);break;
				case EGameFrame.Message.Monster a: EGameFrame.Message.Monster.Serializer.Write(stream.GetBuffer(), a);break;

                default:
                    return;
            }
        }

        public static object DeserializeFrom(ushort opcode, byte[] bytes, int index, int count)
        {
            switch (opcode)
            {
				case 1 : return EGameFrame.Message.FooBarContainer.Serializer.Parse(bytes);
				case 2 : return EGameFrame.Message.LoginRequest.Serializer.Parse(bytes);
				case 3 : return EGameFrame.Message.Monster.Serializer.Parse(bytes);

                default:
                    return null;
            }
        }

        public static object DeserializeFrom(ushort opcode, MemoryStream stream)
        {
            switch (opcode)
            {
				case 1 : return EGameFrame.Message.FooBarContainer.Serializer.Parse(stream.GetBuffer());
				case 2 : return EGameFrame.Message.LoginRequest.Serializer.Parse(stream.GetBuffer());
				case 3 : return EGameFrame.Message.Monster.Serializer.Parse(stream.GetBuffer());

                default:
                    return null;
            }
        }
    }
}
