using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Site13Kernel.GameLogic
{
    [Serializable]
    public class GameDefinition
    {
        public List<MissionCollection> MissionCollections=new List<MissionCollection>();
        public List<RefSprite> Sprites;
        public List<RefTexture2D> Textures;
    }
    [Serializable]
    public class MissionCollection
    {
        public string ID;
        public string FallbackName;
        public List<MissionDefinition> MissionDefinitions;
    }
    public enum WorkMode
    {
        Internal,ExternalFile,ExternalServer
    }
    [Serializable]
    public class RefSprite
    {
        public string Path;
        public WorkMode WorkMode;
        public Sprite LoadedSprite;
    }
    [Serializable]
    public class RefTexture2D
    {
        public string Path;
        public WorkMode WorkMode;
        public Texture2D LoadedTexture;
    }


    [Serializable]
    public class MissionDefinition
    {
        public string NameID;
        public string DispFallback;
        public int TargetScene;
        public int ImageID;
    }
}
