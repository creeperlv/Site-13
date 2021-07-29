using Site13Kernel.GameLogic;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace Site13Kernel.EFI
{
    public class DefinitionLoader : EFIBase
    {
        public List<MissionDefinition> definitions=new List<MissionDefinition>();
        public List<RefSprite> sprites=new List<RefSprite>();
        public List<RefTexture2D> textures=new List<RefTexture2D>();
        public override void Run()
        {
            GameInfo.CurrentGameDef = new GameDefinition();
            GameInfo.CurrentGameDef.MissionCollections.Add(new MissionCollection { MissionDefinitions = definitions });
            StartCoroutine(LoadResources());
        }
        IEnumerator LoadResources()
        {
            foreach (var item in sprites)
            {
                switch (item.WorkMode)
                {
                    case WorkMode.Internal:
                        {
                            if (item.LoadedSprite == null)
                            {
                                item.LoadedSprite = Resources.Load<Sprite>(item.Path);
                                Debug.Log("Loaded");
                            }
                        }
                        break;
                    case WorkMode.ExternalFile:
                        {
                            var uri="file://"+Path.Combine(Application.persistentDataPath,item.Path);
                            yield return LoadSprite(uri, item);
                        }
                        break;
                    case WorkMode.ExternalServer:
                        {
                            yield return LoadSprite(item.Path, item);
                        }
                        break;
                    default:
                        break;
                }
            }
            GameInfo.CurrentGameDef.Sprites = sprites;

            GameInfo.CurrentGameDef.Textures = textures;
        }
        IEnumerator LoadSprite(string uri, RefSprite refSprite)
        {
            using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture(uri))
            {
                yield return uwr.SendWebRequest();

                if (uwr.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(uwr.error);
                }
                else
                {
                    var texture = DownloadHandlerTexture.GetContent(uwr);
                    refSprite.LoadedSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(.5f, .5f));
                }
            }
        }
    }

}