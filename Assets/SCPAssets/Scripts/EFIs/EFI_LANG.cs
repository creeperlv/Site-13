using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Site_13_Plug_in_Lib.Game.GamingEnvironment.Materials;

namespace Site13Kernel.EFI
{
    public class EFI_LANG : EFIBase
    {
        public override void Run()
        {
            Site_13ToolLib.Globalization.Language.LoadLanguage();
            var LangCode = Site_13ToolLib.Globalization.Language.LanguageCode;
            try
            {
                {
                    ExternalMaterialLoader.ModifyMaterialMainTexture(EditableMaterials.Button01, $"./ExtraResources/Languages/{LangCode}/Button2_Normal.png", 500, 1000);
                }
                {
                    ExternalMaterialLoader.ModifyMaterialMainTexture(EditableMaterials.Button02, $"./ExtraResources/Languages/{LangCode}/Button2_Keycard.png", 500, 1000);
                }
                {
                    ExternalMaterialLoader.ModifyMaterialMainTexture(EditableMaterials.Button03, $"./ExtraResources/Languages/{LangCode}/Button2_Lockdown.png", 500, 1000);
                }
            }
            catch (System.Exception)
            {
            }
        }
    }

}