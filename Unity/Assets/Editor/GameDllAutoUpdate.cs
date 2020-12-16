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
        private const string BasicsDll = "Code.Basics.dll";
        private const string BasicsPdb = "Code.Basics.pdb";
        private const string ModulesDll = "Code.Modules.dll";
        private const string ModulesPdb = "Code.Modules.pdb";
        private const string GameModulesDll = "Game.Modules.dll";
        private const string GameModulesPdb = "Game.Modules.pdb";

        [UnityEditor.Callbacks.DidReloadScripts]
        static void OnReloadScripts()
        {
            File.Copy(Path.Combine(ScriptAssembliesDir, BasicsDll), Path.Combine(CodeDir, BasicsDll), true);
            File.Copy(Path.Combine(ScriptAssembliesDir, BasicsPdb), Path.Combine(CodeDir, BasicsPdb), true);
            File.Copy(Path.Combine(ScriptAssembliesDir, ModulesDll), Path.Combine(CodeDir, ModulesDll), true);
            File.Copy(Path.Combine(ScriptAssembliesDir, ModulesPdb), Path.Combine(CodeDir, ModulesPdb), true);
            File.Copy(Path.Combine(ScriptAssembliesDir, GameModulesDll), Path.Combine(CodeDir, GameModulesDll), true);
            File.Copy(Path.Combine(ScriptAssembliesDir, GameModulesPdb), Path.Combine(CodeDir, GameModulesPdb), true);
        }
    }
}