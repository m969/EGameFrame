using System;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace FlatBuffers.Editor
{
	public class Fbs2CSEditor: EditorWindow
	{
		[MenuItem("Tools/Fbs2CS")]
		public static void AllFbs2CS()
		{
            var flatcPath = Path.GetFullPath(Path.Combine("../FlatSharp.Compiler", "FlatSharp.Compiler.exe"));
			Selection.activeObject = AssetDatabase.LoadAssetAtPath("Assets", typeof(UnityEngine.Object));
			var selects = Selection.GetFiltered<UnityEngine.Object>(SelectionMode.DeepAssets | SelectionMode.ExcludePrefab/* | SelectionMode.Deep*//* */);
            foreach (var item in selects)
            {
				var path = AssetDatabase.GetAssetPath(item);
				if (!path.EndsWith(".fbs"))
				{
					continue;
				}
				var fileName = Path.GetFileName(path);
				var directorPath = Path.GetDirectoryName(path);
				Process process = ET.ProcessHelper.Run(flatcPath, fileName, directorPath, true);
				var output = process.StandardOutput.ReadToEnd();
				if (string.IsNullOrEmpty(output))
				{
					Debug.Log($"{fileName} code generate success!");
				}
				else
				{
					Debug.Log(output);
				}
			}
			AssetDatabase.Refresh();
		}
	}
}
