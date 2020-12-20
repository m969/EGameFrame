using System;
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
        static string TemplateCode = @"
using System;
using System.IO;
using FlatSharp;

namespace EGameFrame.FlatBuffers
{
    public static class FlatBuffersSerializeHelper
    {
        public static void SerializeTo(ushort opcode, object obj, MemoryStream stream)
        {
            switch (opcode)
            {
{0}
                default:
                    return;
            }
        }

        public static object DeserializeFrom(ushort opcode, Type type, byte[] bytes, int index, int count)
        {
            switch (opcode)
            {
{1}
                default:
                    return null;
            }
        }

        public static object DeserializeFrom(ushort opcode, Type type, MemoryStream stream)
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

			var sb = new StringBuilder();
			var sb1 = new StringBuilder();
			var sb2 = new StringBuilder();
            var startOpcode = 0;
			foreach (var item in fbsFiles)
            {
                startOpcode++;
                Debug.Log($"{item} code {startOpcode}");
                sb.AppendLine($"\t\t\t\tcase {startOpcode} : EGameFrame.Message.{item}.Serializer.Write(stream.GetBuffer(), obj as EGameFrame.Message.{item});break;");
                sb1.AppendLine($"\t\t\t\tcase {startOpcode} : return EGameFrame.Message.{item}.Serializer.Parse(bytes);");
                sb2.AppendLine($"\t\t\t\tcase {startOpcode} : return EGameFrame.Message.{item}.Serializer.Parse(stream.GetBuffer());");
            }
            //var codeText = string.Format(TemplateCode, sb.ToString(), sb1.ToString(), sb2.ToString());
            var codeText = TemplateCode;
            codeText = codeText.Replace("{0}", sb.ToString());
            codeText = codeText.Replace("{1}", sb1.ToString());
            codeText = codeText.Replace("{2}", sb2.ToString());
            File.WriteAllText("Assets/Code.Modules/Message/FbsGenerated/FlatBuffersSerializeHelper.generated.cs", codeText);
			AssetDatabase.Refresh();
		}
	}
}
