using System.IO;
using FlatSharp;

namespace EGameFrame.Message
{
    public static class FlatBuffersSerializeHelper
    {
        public static void SerializeTo(ushort opcode, object obj, MemoryStream stream)
        {
            switch (obj)
            {
				case EGameFrame.Message.FooBarContainer msg : EGameFrame.Message.FooBarContainer.Serializer.Write(stream.GetBuffer(), msg);break;
				case EGameFrame.Message.LoginRequest msg : EGameFrame.Message.LoginRequest.Serializer.Write(stream.GetBuffer(), msg);break;
				case EGameFrame.Message.LoginResponse msg : EGameFrame.Message.LoginResponse.Serializer.Write(stream.GetBuffer(), msg);break;
				case EGameFrame.Message.Monster msg : EGameFrame.Message.Monster.Serializer.Write(stream.GetBuffer(), msg);break;

                default:
                    return;
            }
        }

        public static object DeserializeFrom(ushort opcode, byte[] bytes, int index, int count)
        {
            switch (opcode)
            {
				case 101 : return EGameFrame.Message.FooBarContainer.Serializer.Parse(bytes);
				case 102 : return EGameFrame.Message.LoginRequest.Serializer.Parse(bytes);
				case 103 : return EGameFrame.Message.LoginResponse.Serializer.Parse(bytes);
				case 104 : return EGameFrame.Message.Monster.Serializer.Parse(bytes);

                default:
                    return null;
            }
        }

        public static object DeserializeFrom(ushort opcode, MemoryStream stream)
        {
            switch (opcode)
            {
				case 101 : return EGameFrame.Message.FooBarContainer.Serializer.Parse(stream.GetBuffer());
				case 102 : return EGameFrame.Message.LoginRequest.Serializer.Parse(stream.GetBuffer());
				case 103 : return EGameFrame.Message.LoginResponse.Serializer.Parse(stream.GetBuffer());
				case 104 : return EGameFrame.Message.Monster.Serializer.Parse(stream.GetBuffer());

                default:
                    return null;
            }
        }
    }
}
