using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site13Kernel.GameLogic
{
    [Serializable]
    public class GameDefinition
    {
        public List<MissionDefinition> MissionDefinitions;
    }
    [Serializable]
    public class MissionDefinition
    {
        public string NameID;
        public string DispFallback;
        public int TargetScene;
    }
}
