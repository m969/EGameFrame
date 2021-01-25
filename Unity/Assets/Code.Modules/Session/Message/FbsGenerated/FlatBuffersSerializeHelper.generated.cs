
using System;
using System.IO;
using FlatSharp;
using ET;

namespace EGameFrame.Message
{
    [Message(1)]
    public partial class Monster : IMessage
    {
    }

    [Message(2)]
    public partial class LoginRequest : IRequest, IResponse
    {
        public int RpcId { get; set; }
        public int Error { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Message { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    public static class FlatBuffersSerializeHelper
    {
        public static void SerializeTo(ushort opcode, object obj, MemoryStream stream)
        {
            switch (obj)
            {
				case EGameFrame.Message.FooBarContainer msg : EGameFrame.Message.FooBarContainer.Serializer.Write(stream.GetBuffer(), msg);break;
				case EGameFrame.Message.LoginRequest msg : EGameFrame.Message.LoginRequest.Serializer.Write(stream.GetBuffer(), msg);break;
				case EGameFrame.Message.Monster msg : EGameFrame.Message.Monster.Serializer.Write(stream.GetBuffer(), msg);break;

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
