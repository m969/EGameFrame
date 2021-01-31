using System.Collections.Generic;
using EGameFrame;

namespace EGameFrame.District
{
    public class DistrictScene : Entity
    {
        public Dictionary<string, Module> Modules { get; private set; } = new Dictionary<string, Module>();


        public Module AddModule(string moduleType)
        {
            Modules.Add(moduleType, Entity.CreateWithParent<Module>(this));
            return Modules[moduleType];
        }

        public Module GetModule(string moduleType)
        {
            return Modules[moduleType];
        }
    }
}