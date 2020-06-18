using Site_13_Plug_in_Lib.Game.GamingEnvironment;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace Site13Kernel.EFI
{
    public class ExternalMaterialLoader : EFIBase
    {
        public Material Door1Side1;
        public Material Door2Logo;
        public Material DoorButton01;//Normal
        public Material DoorButton02;//Keycard
        public Material DoorButton03;//Locked
        public Material DoorButton01_V2;//Normal
        public Material DoorButton02_V2;//Keycard
        public Material DoorButton03_V2;//Locked
        public Material HeavyDoor_Logo;
        public Material Foundation_Logo;
        public Material OfficeZone_Wall;
        public Material ContainZone_Wall;
        public Material OfficeZone_Floor;
        public override void Run()
        {
            StaticResources.MaterialLoader = this;
        }
        public static void ModifyMaterialMainTexture(Materials.EditableMaterials TargetMaterial,string TexturePath,int width,int height)
        {
            switch (TargetMaterial)
            {
                case Materials.EditableMaterials.Door1_Side_1:
                    {
                        MODIFYMAINTXTURE(StaticResources.MaterialLoader.Door1Side1,TexturePath,width,height);
                    }
                    break;
                case Materials.EditableMaterials.Door2_Logo:
                    {
                        MODIFYMAINTXTURE(StaticResources.MaterialLoader.Door2Logo, TexturePath, width, height);
                    }
                    break;
                case Materials.EditableMaterials.HeavyDoor_Logo:
                    {
                        MODIFYMAINTXTURE(StaticResources.MaterialLoader.HeavyDoor_Logo, TexturePath, width, height);
                    }
                    break;
                case Materials.EditableMaterials.Foundation_Logo:
                    {
                        MODIFYMAINTXTURE(StaticResources.MaterialLoader.Foundation_Logo, TexturePath, width, height);
                    }
                    break;
                case Materials.EditableMaterials.OfficeZone_Wall:
                    {
                        MODIFYMAINTXTURE(StaticResources.MaterialLoader.OfficeZone_Wall, TexturePath, width, height);
                    }
                    break;
                case Materials.EditableMaterials.ContainZone_Wall:
                    {
                        MODIFYMAINTXTURE(StaticResources.MaterialLoader.ContainZone_Wall, TexturePath, width, height);
                    }
                    break;
                case Materials.EditableMaterials.OfficeZone_Floor:
                    {
                        MODIFYMAINTXTURE(StaticResources.MaterialLoader.OfficeZone_Floor, TexturePath, width, height);
                    }
                    break;
                case Materials.EditableMaterials.Button01:
                    {
                        MODIFYMAINTXTURE(StaticResources.MaterialLoader.DoorButton01, TexturePath, width, height);
                    }
                    break;
                case Materials.EditableMaterials.Button02:
                    {
                        MODIFYMAINTXTURE(StaticResources.MaterialLoader.DoorButton02, TexturePath, width, height);
                    }
                    break;
                case Materials.EditableMaterials.Button03:
                    {
                        MODIFYMAINTXTURE(StaticResources.MaterialLoader.DoorButton03, TexturePath, width, height);
                    }
                    break;
                default:
                    break;
            }
        }
        public static void ModifyMaterialHeight(Materials.EditableMaterials TargetMaterial,float Height)
        {
            switch (TargetMaterial)
            {
                case Materials.EditableMaterials.Door1_Side_1:
                    {
                        MODIFYHEIGHT(StaticResources.MaterialLoader.Door1Side1,Height);
                    }
                    break;
                case Materials.EditableMaterials.Door2_Logo:
                    {
                        MODIFYHEIGHT(StaticResources.MaterialLoader.Door2Logo,Height);
                    }
                    break;
                case Materials.EditableMaterials.HeavyDoor_Logo:
                    {
                        MODIFYHEIGHT(StaticResources.MaterialLoader.HeavyDoor_Logo, Height);
                    }
                    break;
                case Materials.EditableMaterials.Foundation_Logo:
                    {
                        MODIFYHEIGHT(StaticResources.MaterialLoader.Foundation_Logo, Height);
                    }
                    break;
                case Materials.EditableMaterials.OfficeZone_Wall:
                    {
                        MODIFYHEIGHT(StaticResources.MaterialLoader.OfficeZone_Wall, Height);
                    }
                    break;
                case Materials.EditableMaterials.ContainZone_Wall:
                    {
                        MODIFYHEIGHT(StaticResources.MaterialLoader.ContainZone_Wall, Height);
                    }
                    break;
                case Materials.EditableMaterials.OfficeZone_Floor:
                    {
                        MODIFYHEIGHT(StaticResources.MaterialLoader.OfficeZone_Floor, Height);
                    }
                    break;
                case Materials.EditableMaterials.Button01:
                    {
                        MODIFYHEIGHT(StaticResources.MaterialLoader.DoorButton01, Height);
                    }
                    break;
                case Materials.EditableMaterials.Button02:
                    {
                        MODIFYHEIGHT(StaticResources.MaterialLoader.DoorButton02, Height);
                    }
                    break;
                case Materials.EditableMaterials.Button03:
                    {
                        MODIFYHEIGHT(StaticResources.MaterialLoader.DoorButton03, Height);
                    }
                    break;
                    //case Materials.EditableMaterials.
                default:
                    break;
            }
        }
        static void MODIFYHEIGHT(Material material,float Height)
        {
            material.SetFloat("_Parallax", Height);
        }
        static void MODIFYMAINTXTURE(Material material,string TexturePath,int width,int height)
        {

            FileStream fileStream = new FileStream(TexturePath, FileMode.Open, FileAccess.Read);
            fileStream.Seek(0, SeekOrigin.Begin);
            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);
            fileStream.Close();
            fileStream.Dispose();
            fileStream = null;
            Texture2D texture = new Texture2D(width, height);
            texture.LoadImage(bytes);
            material.mainTexture = texture;
        }
    }

}