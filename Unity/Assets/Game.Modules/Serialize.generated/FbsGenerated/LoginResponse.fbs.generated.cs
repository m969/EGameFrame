
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the FlatSharp FBS to C# compiler (source hash: 1.0.0.M8UzJCmIUog1xsfcy0s8K2kfWltt4nYo7Ut4mRxUTJ4=)
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using FlatSharp;
using FlatSharp.Attributes;
namespace EGameFrame
{
    namespace Message
    {
        [FlatBufferTable]
        [System.Runtime.CompilerServices.CompilerGenerated]
        public partial class LoginResponse : object
        {
            partial void OnInitialized();
            public LoginResponse()
            {
                checked
                { this.OnInitialized(); }
            }

            public LoginResponse(LoginResponse source)
            {
                checked
                {
                    this.RpcId = source.RpcId;
                    this.Error = source.Error;
                    this.Message = source.Message;
                    this.Account = source.Account;
                    this.Password = source.Password;
                    this.OnInitialized();
                }
            }

            [FlatBufferItem(0)]
            public virtual System.Int32 RpcId { get; set; }
            [FlatBufferItem(1)]
            public virtual System.Int32 Error { get; set; }
            [FlatBufferItem(2)]
            public virtual System.String Message { get; set; }
            [FlatBufferItem(3)]
            public virtual System.String Account { get; set; }
            [FlatBufferItem(4)]
            public virtual System.String Password { get; set; }
            public static ISerializer<EGameFrame.Message.LoginResponse> Serializer { get; } = new GeneratedSerializer().AsISerializer();

            #region Serializer for EGameFrame.Message.LoginResponse

            [FlatSharpGeneratedSerializerAttribute(FlatBufferDeserializationOption.GreedyMutable)]
            private sealed class GeneratedSerializer : IGeneratedSerializer<EGameFrame.Message.LoginResponse>
            {

                public void Write<TSpanWriter>(TSpanWriter writer, Span<byte> target, EGameFrame.Message.LoginResponse root, int offset, SerializationContext context)
                    where TSpanWriter : ISpanWriter
                {
                    checked
                    {
                        WriteInlineValueOf_82a7570238f84355a488561f55cfc02c(writer, target, root, offset, context);
                    }
                }

                public int GetMaxSize(EGameFrame.Message.LoginResponse root)
                {
                    checked
                    {
                        return GetMaxSizeOf_82a7570238f84355a488561f55cfc02c(root);
                    }
                }

                public EGameFrame.Message.LoginResponse Parse<TInputBuffer>(TInputBuffer buffer, int offset)
                    where TInputBuffer : IInputBuffer
                {
                    checked
                    {
                        return Read_82a7570238f84355a488561f55cfc02c(buffer, offset);
                    }
                }

                private static int GetMaxSizeOf_82a7570238f84355a488561f55cfc02c(EGameFrame.Message.LoginResponse value)
                {
                    checked
                    {

                        int runningSum = 42 + 15;
                        var index2Value = value.Message;

                        if (!object.ReferenceEquals(index2Value, null))
                        {
                            runningSum += GetMaxSizeOf_77a73dd52ff1495990f082f6aedca219(index2Value);
                        }
                        var index3Value = value.Account;

                        if (!object.ReferenceEquals(index3Value, null))
                        {
                            runningSum += GetMaxSizeOf_77a73dd52ff1495990f082f6aedca219(index3Value);
                        }
                        var index4Value = value.Password;

                        if (!object.ReferenceEquals(index4Value, null))
                        {
                            runningSum += GetMaxSizeOf_77a73dd52ff1495990f082f6aedca219(index4Value);
                        };
                        return runningSum;

                    }
                }

                private static EGameFrame.Message.LoginResponse Read_82a7570238f84355a488561f55cfc02c<TInputBuffer>(
                    TInputBuffer buffer,
                    int offset) where TInputBuffer : IInputBuffer
                {
                    checked
                    {
                        return new tableReader_acef15ceeba44c79b2cc2f31affa879a<TInputBuffer>(buffer, offset + buffer.ReadUOffset(offset));
                    }
                }

                private sealed class tableReader_acef15ceeba44c79b2cc2f31affa879a<TInputBuffer> : EGameFrame.Message.LoginResponse where TInputBuffer : IInputBuffer
                {



                    public tableReader_acef15ceeba44c79b2cc2f31affa879a(TInputBuffer buffer, int offset)
                    {
                        checked
                        {
                            this.__index0 = __ReadIndex0Value(buffer, offset);
                            this.__index1 = __ReadIndex1Value(buffer, offset);
                            this.__index2 = __ReadIndex2Value(buffer, offset);
                            this.__index3 = __ReadIndex3Value(buffer, offset);
                            this.__index4 = __ReadIndex4Value(buffer, offset);
                        }
                    }

                    private System.Int32 __index0;
                    public override System.Int32 RpcId
                    {
                        get
                        {
                            checked
                            { return this.__index0; }
                        }

                        set
                        {
                            checked
                            { this.__index0 = value; }
                        }
                    }

                    [MethodImpl(MethodImplOptions.AggressiveInlining)]
                    private static System.Int32 __ReadIndex0Value(TInputBuffer buffer, int offset)
                    {
                        checked
                        {
                            int absoluteLocation = buffer.GetAbsoluteTableFieldLocation(offset, 0);
                            if (absoluteLocation == 0)
                            {
                                return default(System.Int32);
                            }
                            else
                            {
                                return Read_da8d7fc0a8e349f1b6754a7f3933f42b(buffer, absoluteLocation);
                            }
                        }
                    }

                    private System.Int32 __index1;
                    public override System.Int32 Error
                    {
                        get
                        {
                            checked
                            { return this.__index1; }
                        }

                        set
                        {
                            checked
                            { this.__index1 = value; }
                        }
                    }

                    [MethodImpl(MethodImplOptions.AggressiveInlining)]
                    private static System.Int32 __ReadIndex1Value(TInputBuffer buffer, int offset)
                    {
                        checked
                        {
                            int absoluteLocation = buffer.GetAbsoluteTableFieldLocation(offset, 1);
                            if (absoluteLocation == 0)
                            {
                                return default(System.Int32);
                            }
                            else
                            {
                                return Read_da8d7fc0a8e349f1b6754a7f3933f42b(buffer, absoluteLocation);
                            }
                        }
                    }

                    private System.String __index2;
                    public override System.String Message
                    {
                        get
                        {
                            checked
                            { return this.__index2; }
                        }

                        set
                        {
                            checked
                            { this.__index2 = value; }
                        }
                    }

                    [MethodImpl(MethodImplOptions.AggressiveInlining)]
                    private static System.String __ReadIndex2Value(TInputBuffer buffer, int offset)
                    {
                        checked
                        {
                            int absoluteLocation = buffer.GetAbsoluteTableFieldLocation(offset, 2);
                            if (absoluteLocation == 0)
                            {
                                return default(System.String);
                            }
                            else
                            {
                                return Read_77a73dd52ff1495990f082f6aedca219(buffer, absoluteLocation);
                            }
                        }
                    }

                    private System.String __index3;
                    public override System.String Account
                    {
                        get
                        {
                            checked
                            { return this.__index3; }
                        }

                        set
                        {
                            checked
                            { this.__index3 = value; }
                        }
                    }

                    [MethodImpl(MethodImplOptions.AggressiveInlining)]
                    private static System.String __ReadIndex3Value(TInputBuffer buffer, int offset)
                    {
                        checked
                        {
                            int absoluteLocation = buffer.GetAbsoluteTableFieldLocation(offset, 3);
                            if (absoluteLocation == 0)
                            {
                                return default(System.String);
                            }
                            else
                            {
                                return Read_77a73dd52ff1495990f082f6aedca219(buffer, absoluteLocation);
                            }
                        }
                    }

                    private System.String __index4;
                    public override System.String Password
                    {
                        get
                        {
                            checked
                            { return this.__index4; }
                        }

                        set
                        {
                            checked
                            { this.__index4 = value; }
                        }
                    }

                    [MethodImpl(MethodImplOptions.AggressiveInlining)]
                    private static System.String __ReadIndex4Value(TInputBuffer buffer, int offset)
                    {
                        checked
                        {
                            int absoluteLocation = buffer.GetAbsoluteTableFieldLocation(offset, 4);
                            if (absoluteLocation == 0)
                            {
                                return default(System.String);
                            }
                            else
                            {
                                return Read_77a73dd52ff1495990f082f6aedca219(buffer, absoluteLocation);
                            }
                        }
                    }
                }



                private static void WriteInlineValueOf_82a7570238f84355a488561f55cfc02c<TSpanWriter>(
                    TSpanWriter spanWriter,
                    Span<byte> span,
                    EGameFrame.Message.LoginResponse value,
                    int offset,
                    SerializationContext context) where TSpanWriter : ISpanWriter
                {
                    checked
                    {

                        int tableStart = context.AllocateSpace(39, sizeof(int));
                        spanWriter.WriteUOffset(span, offset, tableStart, context);
                        int currentOffset = tableStart + sizeof(int); // skip past vtable soffset_t.

                        Span<byte> vtable = stackalloc byte[14];
                        int maxVtableIndex = -1;
                        vtable.Clear(); // reset to 0. Random memory from the stack isn't trustworthy.


                        var index0Value = value.RpcId;
                        var index0Offset = 0;
                        if (index0Value != default(System.Int32))
                        {

                            currentOffset += SerializationHelpers.GetAlignmentError(currentOffset, 4);
                            index0Offset = currentOffset;
                            spanWriter.WriteUShort(vtable, (ushort)(currentOffset - tableStart), 4, context);
                            maxVtableIndex = 0;
                            currentOffset += 4;

                        }

                        var index1Value = value.Error;
                        var index1Offset = 0;
                        if (index1Value != default(System.Int32))
                        {

                            currentOffset += SerializationHelpers.GetAlignmentError(currentOffset, 4);
                            index1Offset = currentOffset;
                            spanWriter.WriteUShort(vtable, (ushort)(currentOffset - tableStart), 6, context);
                            maxVtableIndex = 1;
                            currentOffset += 4;

                        }

                        var index2Value = value.Message;
                        var index2Offset = 0;
                        if (index2Value != default(System.String))
                        {

                            currentOffset += SerializationHelpers.GetAlignmentError(currentOffset, 4);
                            index2Offset = currentOffset;
                            spanWriter.WriteUShort(vtable, (ushort)(currentOffset - tableStart), 8, context);
                            maxVtableIndex = 2;
                            currentOffset += 4;

                        }

                        var index3Value = value.Account;
                        var index3Offset = 0;
                        if (index3Value != default(System.String))
                        {

                            currentOffset += SerializationHelpers.GetAlignmentError(currentOffset, 4);
                            index3Offset = currentOffset;
                            spanWriter.WriteUShort(vtable, (ushort)(currentOffset - tableStart), 10, context);
                            maxVtableIndex = 3;
                            currentOffset += 4;

                        }

                        var index4Value = value.Password;
                        var index4Offset = 0;
                        if (index4Value != default(System.String))
                        {

                            currentOffset += SerializationHelpers.GetAlignmentError(currentOffset, 4);
                            index4Offset = currentOffset;
                            spanWriter.WriteUShort(vtable, (ushort)(currentOffset - tableStart), 12, context);
                            maxVtableIndex = 4;
                            currentOffset += 4;

                        }
                        int tableLength = currentOffset - tableStart;
                        context.Offset -= 39 - tableLength;
                        int vtableLength = 6 + (2 * maxVtableIndex);
                        spanWriter.WriteUShort(vtable, (ushort)vtableLength, 0, context);
                        spanWriter.WriteUShort(vtable, (ushort)tableLength, sizeof(ushort), context);
                        int vtablePosition = context.FinishVTable(span, vtable.Slice(0, vtableLength));
                        spanWriter.WriteInt(span, tableStart - vtablePosition, tableStart, context);

                        if (index0Offset != 0)
                        {
                            WriteInlineValueOf_da8d7fc0a8e349f1b6754a7f3933f42b(
                                spanWriter,
                                span,
                                index0Value,
                                index0Offset,
                                context);

                        }

                        if (index1Offset != 0)
                        {
                            WriteInlineValueOf_da8d7fc0a8e349f1b6754a7f3933f42b(
                                spanWriter,
                                span,
                                index1Value,
                                index1Offset,
                                context);

                        }

                        if (index2Offset != 0)
                        {
                            WriteInlineValueOf_77a73dd52ff1495990f082f6aedca219(
                                spanWriter,
                                span,
                                index2Value,
                                index2Offset,
                                context);

                        }

                        if (index3Offset != 0)
                        {
                            WriteInlineValueOf_77a73dd52ff1495990f082f6aedca219(
                                spanWriter,
                                span,
                                index3Value,
                                index3Offset,
                                context);

                        }

                        if (index4Offset != 0)
                        {
                            WriteInlineValueOf_77a73dd52ff1495990f082f6aedca219(
                                spanWriter,
                                span,
                                index4Value,
                                index4Offset,
                                context);

                        }
                    }
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                private static int GetMaxSizeOf_da8d7fc0a8e349f1b6754a7f3933f42b(System.Int32 value)
                {
                    checked
                    {
                        return 7;
                    }
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                private static System.Int32 Read_da8d7fc0a8e349f1b6754a7f3933f42b<TInputBuffer>(
                    TInputBuffer buffer,
                    int offset) where TInputBuffer : IInputBuffer
                {
                    checked
                    {
                        return buffer.ReadInt(offset);
                    }
                }

                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                private static void WriteInlineValueOf_da8d7fc0a8e349f1b6754a7f3933f42b<TSpanWriter>(
                    TSpanWriter spanWriter,
                    Span<byte> span,
                    System.Int32 value,
                    int offset,
                    SerializationContext context) where TSpanWriter : ISpanWriter
                {
                    checked
                    {
                        spanWriter.WriteInt(span, value, offset, context);
                    }
                }

                private static int GetMaxSizeOf_77a73dd52ff1495990f082f6aedca219(System.String value)
                {
                    checked
                    {
                        return SerializationHelpers.GetMaxSize(value);
                    }
                }

                private static System.String Read_77a73dd52ff1495990f082f6aedca219<TInputBuffer>(
                    TInputBuffer buffer,
                    int offset) where TInputBuffer : IInputBuffer
                {
                    checked
                    {
                        return buffer.ReadString(offset);
                    }
                }

                private static void WriteInlineValueOf_77a73dd52ff1495990f082f6aedca219<TSpanWriter>(
                    TSpanWriter spanWriter,
                    Span<byte> span,
                    System.String value,
                    int offset,
                    SerializationContext context) where TSpanWriter : ISpanWriter
                {
                    checked
                    {
                        spanWriter.WriteString(span, value, offset, context);
                    }
                }
            }

            #endregion
        }
    }
}
