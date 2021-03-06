﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace FlatBuffers.Editor
{
	public class Fbs2CSEditor: EditorWindow
	{
        static string SerializeHelperTemplateCode = @"using System.IO;
using FlatSharp;

namespace EGameFrame.Message
{
    public static class FlatBuffersSerializeHelper
    {
        public static void SerializeTo(ushort opcode, object obj, MemoryStream stream)
        {
            switch (obj)
            {
{0}
                default:
                    return;
            }
        }

        public static object DeserializeFrom(ushort opcode, byte[] bytes, int index, int count)
        {
            switch (opcode)
            {
{1}
                default:
                    return null;
            }
        }

        public static object DeserializeFrom(ushort opcode, MemoryStream stream)
        {
            switch (opcode)
            {
{2}
                default:
                    return null;
            }
        }
    }
}
";
        static string MessageDefineTemplateCode = @"using ET;

namespace EGameFrame.Message
{
{0}
}
";
        static string MessageDefineClassTemplateCode = @"    [Message({0})]
    public partial class {1} : {2}
    {
    }
";
        [MenuItem("Tools/Fbs2CS")]
		public static void AllFbs2CS()
		{
            var flatcPath = Path.GetFullPath(Path.Combine("../FlatSharp.Compiler", "FlatSharp.Compiler.exe"));
			Selection.activeObject = AssetDatabase.LoadAssetAtPath("Assets", typeof(UnityEngine.Object));
			var selects = Selection.GetFiltered<UnityEngine.Object>(SelectionMode.DeepAssets | SelectionMode.ExcludePrefab);
			var fbsFiles = new List<string>();
            foreach (var item in selects)
            {
				var path = AssetDatabase.GetAssetPath(item);
				if (!path.EndsWith(".fbs"))
				{
					continue;
				}
				var fileNameExt = Path.GetFileName(path);
                var fileName = Path.GetFileNameWithoutExtension(path);
                var directorPath = Path.GetDirectoryName(path);
				Process process = ET.ProcessHelper.Run(flatcPath, fileNameExt, directorPath, true);
				fbsFiles.Add(fileName);
				var output = process.StandardOutput.ReadToEnd();
				if (string.IsNullOrEmpty(output))
				{
					Debug.Log($"{fileNameExt} code generate success!");
				}
				else
				{
					Debug.Log(output);
				}
			}
			fbsFiles.Sort((a, b) => a.CompareTo(b));

            {
                var sb = new StringBuilder();
                var sb1 = new StringBuilder();
                var sb2 = new StringBuilder();
                var startOpcode = 100;
                foreach (var item in fbsFiles)
                {
                    startOpcode++;
                    sb.AppendLine($"\t\t\t\tcase EGameFrame.Message.{item} msg : EGameFrame.Message.{item}.Serializer.Write(stream.GetBuffer(), msg);break;");
                    sb1.AppendLine($"\t\t\t\tcase {startOpcode} : return EGameFrame.Message.{item}.Serializer.Parse(bytes);");
                    sb2.AppendLine($"\t\t\t\tcase {startOpcode} : return EGameFrame.Message.{item}.Serializer.Parse(stream.GetBuffer());");
                }
                //var codeText = string.Format(TemplateCode, sb.ToString(), sb1.ToString(), sb2.ToString());
                var codeText = SerializeHelperTemplateCode;
                codeText = codeText.Replace("{0}", sb.ToString());
                codeText = codeText.Replace("{1}", sb1.ToString());
                codeText = codeText.Replace("{2}", sb2.ToString());
                File.WriteAllText("Assets/Code.Modules/Serialize.generated/FlatBuffersSerializeHelper.generated.cs", codeText);
            }

            {
                var sb = new StringBuilder();
                var startOpcode = 100;
                foreach (var item in fbsFiles)
                {
                    startOpcode++;
                    var messageType = "IMessage";
                    if (item.EndsWith("Request")) messageType = "IRequest";
                    if (item.EndsWith("Response")) messageType = "IResponse";
                    var messageClass = MessageDefineClassTemplateCode;
                    messageClass = messageClass.Replace("{0}", startOpcode.ToString());
                    messageClass = messageClass.Replace("{1}", item);
                    messageClass = messageClass.Replace("{2}", messageType);
                    sb.AppendLine(messageClass);
                }
                //var codeText = string.Format(TemplateCode, sb.ToString(), sb1.ToString(), sb2.ToString());
                var codeText = MessageDefineTemplateCode;
                codeText = codeText.Replace("{0}", sb.ToString());
                File.WriteAllText("Assets/Code.Modules/Serialize.generated/FbsMessageDefine.generated.cs", codeText);
            }

			AssetDatabase.Refresh();
		}
	}
}
