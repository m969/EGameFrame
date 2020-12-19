using System;
using System.IO;
using FlatSharp;

namespace ET
{
    public static class MessagePackHelper
    {
        public static void SerializeTo(ushort opcode, object obj, MemoryStream stream)
        {
            if (opcode >= 20000)
            {
                MyGame.Sample.Monster.Serializer.Write(stream.GetBuffer(), obj as MyGame.Sample.Monster);
                //ProtobufHelper.ToStream(obj, stream);
                return;
            }

            //MongoHelper.ToBson(obj, stream);
        }

        public static object DeserializeFrom(ushort opcode, Type type, byte[] bytes, int index, int count)
        {
            if (opcode >= 20000)
            {
                return MyGame.Sample.Monster.Serializer.Parse(bytes);
                //return ProtobufHelper.FromBytes(type, bytes, index, count);
            }

            //return MongoHelper.FromBson(type, bytes, index, count);
            return null;
        }

        public static object DeserializeFrom(ushort opcode, Type type, MemoryStream stream)
        {
            if (opcode >= 20000)
            {
                return MyGame.Sample.Monster.Serializer.Parse(stream.GetBuffer());
                //return ProtobufHelper.FromStream(type, stream);
            }

            //return MongoHelper.FromStream(type, stream);
            return null;
        }
    }
}