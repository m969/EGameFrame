using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace ETEditor
{
    public class GameDllAutoUpdate
    {
        private const string ScriptAssembliesDir = "Library/ScriptAssemblies";
        private const string CodeDir = "Temp/bin/Debug/";
        private const string BasicsDll = "Game.Basic.dll";
        private const string BasicsPdb = "Game.Basic.pdb";
        private const string ModulesDll = "Game.Modules.dll";
        private const string ModulesPdb = "Game.Modules.pdb";

        [UnityEditor.Callbacks.DidReloadScripts]
        static void OnReloadScripts()
        {
            File.Copy(Path.Combine(ScriptAssembliesDir, BasicsDll), Path.Combine(CodeDir, BasicsDll), true);
            File.Copy(Path.Combine(ScriptAssembliesDir, BasicsPdb), Path.Combine(CodeDir, BasicsPdb), true);
            File.Copy(Path.Combine(ScriptAssembliesDir, ModulesDll), Path.Combine(CodeDir, ModulesDll), true);
            File.Copy(Path.Combine(ScriptAssembliesDir, ModulesPdb), Path.Combine(CodeDir, ModulesPdb), true);

        }
    }
}