using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using UnityEditor;

using UnityEngine;
using Debug = UnityEngine.Debug;


namespace FlatBuffers.Editor
{
    [UnityEditor.AssetImporters.ScriptedImporter(1, "fbs")]
    public class FbsImporter : UnityEditor.AssetImporters.ScriptedImporter
    {
        //[Tooltip("A folder to save generated scripts to")]
        //public DefaultAsset generatedSourcePath;

#if UNITY_EDITOR_WIN
        private const string k_FlatCompiler = "FlatSharp.Compiler.exe";
#else
        private const string k_FlatCompiler = "FlatSharp.Compiler";
#endif
        public override void OnImportAsset(UnityEditor.AssetImporters.AssetImportContext ctx)
        {
            return;

            var schemaFile = Path.GetFullPath(ctx.assetPath);

            //if (generatedSourcePath == null)
            //{
            //    generatedSourcePath = AssetDatabase.LoadAssetAtPath<DefaultAsset>(Path.GetDirectoryName(ctx.assetPath));
            //    ctx.AddObjectToAsset("generatedSourcePath", generatedSourcePath);
            //    ctx.SetMainObject(generatedSourcePath);
            //}

            //var sourceFolder = Path.GetFullPath(AssetDatabase.GetAssetPath(generatedSourcePath));

            var flatcPath = Path.GetFullPath(Path.Combine("../FlatSharp.Compiler", k_FlatCompiler)).Replace("\\", "/");
            var path = Path.GetDirectoryName(schemaFile);
            var procArgs = Path.GetFileName(schemaFile);

            Process process = ET.ProcessHelper.Run(flatcPath, procArgs, path, true);
            var output = process.StandardOutput.ReadToEnd();
            if (string.IsNullOrEmpty(output))
            {
                Debug.Log($"{procArgs} code generate success!");
            }
            else
            {
                Debug.Log(output);
            }
            AssetDatabase.Refresh();
        }
    }
}