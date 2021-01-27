using System;
using System.Collections.Generic;
using EGameFrame;
using ET;

namespace EGameFrame.Message
{
	public class OpcodeTypeComponent : Entity
	{
        public static OpcodeTypeComponent Instance;

        private readonly DoubleMap<ushort, Type> opcodeTypes = new DoubleMap<ushort, Type>();

        private readonly Dictionary<ushort, object> typeMessages = new Dictionary<ushort, object>();

        public override void Awake()
        {
	        Instance = this;
        }

        public void Load()
        {
            this.opcodeTypes.Clear();
            this.typeMessages.Clear();

            HashSet<Type> types = AssemblyHelper.GetTypes(typeof(MessageAttribute));
            foreach (Type type in types)
            {
                object[] attrs = type.GetCustomAttributes(typeof(MessageAttribute), false);
                if (attrs.Length == 0)
                {
                    continue;
                }

                MessageAttribute messageAttribute = attrs[0] as MessageAttribute;
                if (messageAttribute == null)
                {
                    continue;
                }

                Log.Debug($"{messageAttribute.Opcode} {type.Name}");
                this.opcodeTypes.Add(messageAttribute.Opcode, type);
                this.typeMessages.Add(messageAttribute.Opcode, Activator.CreateInstance(type));
            }
        }

        public ushort GetOpcode(Type type)
		{
			return this.opcodeTypes.GetKeyByValue(type);
		}

		public Type GetType(ushort opcode)
		{
			return this.opcodeTypes.GetValueByKey(opcode);
		}
	}
}