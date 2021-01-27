using System;
using System.Collections.Generic;
using System.Reflection;
using ET;

namespace EGameFrame
{
	public static class AssemblyHelper
	{
		public static readonly Dictionary<long, Entity> allComponents = new Dictionary<long, Entity>();

		public static readonly Dictionary<string, Assembly> assemblies = new Dictionary<string, Assembly>();

		public static readonly UnOrderMultiMapSet<Type, Type> types = new UnOrderMultiMapSet<Type, Type>();


        public static void Add(Assembly assembly)
		{
			assemblies[assembly.ManifestModule.ScopeName] = assembly;
			types.Clear();
			foreach (Assembly value in assemblies.Values)
			{
				foreach (Type type in value.GetTypes())
				{
					if (type.IsAbstract)
					{
						continue;
					}

					object[] objects = type.GetCustomAttributes(typeof(BaseAttribute), true);
					if (objects.Length == 0)
					{
						continue;
					}

					foreach (BaseAttribute baseAttribute in objects)
					{
						types.Add(baseAttribute.AttributeType, type);
					}
				}
			}
		}

		public static HashSet<Type> GetTypes(Type systemAttributeType)
		{
			if (!types.ContainsKey(systemAttributeType))
			{
				return new HashSet<Type>();
			}
			return types[systemAttributeType];
		}
	}
}
