using Site13Kernel.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Site13Kernel
{
    public class SaveSystem : Savable
    {
        public static int DoorSaveMethod = 1;
        public GameObject Player;
        public float[] LiftSpawnPosition = { 0, 0, 0 };
        public float[] LiftSpawnRotation = { 0, 0, 0 };
        public float[] Stair01SpawnPosition = { 0, 0, 0 };
        public float[] Stair01SpawnRotation = { 0, 0, 0 };
        public float[] Tunnel01SpawnPosition = { 0, 0, 0 };
        public float[] Tunnel01SpawnRotation = { 0, 0, 0 };
        public GameObject[] objectsToCheckActive;
        public GameObject[] objectsToLogLocationAndRotation;
        [HideInInspector]
        public SCPDoor[] SCPDoorCollection;
        public SCPStoryNodeBaseCode[] StoryNodes;
        public Transform DoorsCollection;
        public Transform baseFacility;
        // Start is called before the first frame update, also the loader.
        int FindSeed(string n)
        {
            int i = int.MinValue + 1;
            for (int a = 0; a < n.Length; a++)
            {
                i += n[a] * (a * 3 + 1);
            }
            return -1;
        }
        List<GameObject> resboxes;
        void FindResBox(Transform baseT)
        {
            if (baseT.childCount > 0)
            {
                for (int i = 0; i < baseT.childCount; i++)
                {
                    var c = baseT.GetChild(i);
                    if (c.GetComponent<SCPResBox>() != null)
                    {
                        resboxes.Add(c.gameObject);
                    }
                    else FindResBox(c);
                }
            }
            else
            {
                return;
            }
        }
        void Start()
        {
            var roots=SceneManager.GetActiveScene().GetRootGameObjects();
            foreach (var item in roots)
            {
                if (item.name == "Map")
                {

                    baseFacility = item.transform.Find("Facility");

                }
            }
            try
            {

                FindResBox(baseFacility);
                var l1= objectsToCheckActive.ToList();
                foreach (var item in resboxes)
                {
                    l1.Add(item);
                }
                objectsToCheckActive = l1.ToArray();
            }
            catch (System.Exception)
            {
            }
            GameInfo.CurrentGame.BaseSeed = (FindSeed(GameInfo.CurrentGame.SaveName));
            //Find all SCPDoors
            GameInfo.CurrentGame.CurrentSceneSaveSystem = this;
            {
                SCPDoorCollection = new SCPDoor[DoorsCollection.childCount * 2];
                for (int i = 0; i < DoorsCollection.childCount; i++)
                {
                    var d = DoorsCollection.GetChild(i);
                    SCPDoorCollection[i * 2] = (d.Find("Buttons").GetChild(0).GetComponent<SCPDoor>());
                    SCPDoorCollection[i * 2 + 1] = (d.Find("Buttons").GetChild(1).GetComponent<SCPDoor>());
                }
            }
            GameInfo.CurrentGame.CurrentScene = SceneManager.GetActiveScene().name;
            GameInfo.CurrentGame.PlayerScene = SceneManager.GetActiveScene().buildIndex;
            Debug.Log(GameInfo.CurrentGame.PlayerScene) ;
            if (GameInfo.CurrentGame.saveControlProtocolMode == GameInfo.SaveControlProtocolMode.NewGame)
            {
                //GameInfo.CurrentGame.saveControlProtocolMode = GameInfo.SaveControlProtocolMode.Load;
            }
            else if (GameInfo.CurrentGame.saveControlProtocolMode == GameInfo.SaveControlProtocolMode.Load || GameInfo.CurrentGame.saveControlProtocolMode == GameInfo.SaveControlProtocolMode.Enter)
            {
                try
                {
                    SaveControlProtocol.TargetFile = GameInfo.CurrentGame.GetCurrentSceneSaveName();
                    var TotalData = SaveControlProtocol.Load();
                    try
                    {

                        SaveControlProtocol.Deserialize(objectsToCheckActive, TotalData, SaveControlProtocol.DataType.GameObjectActive);
                    }
                    catch (System.Exception)
                    {

                    }
                    try
                    {
                        SaveControlProtocol.Deserialize(objectsToLogLocationAndRotation, TotalData, SaveControlProtocol.DataType.GameObjectLocation);
                    }
                    catch (System.Exception)
                    {
                    }
                    try
                    {
                        SaveControlProtocol.Deserialize(objectsToLogLocationAndRotation, TotalData, SaveControlProtocol.DataType.GameObjectRotation);
                    }
                    catch (System.Exception)
                    {
                    }
                    try
                    {
                        SaveControlProtocol.Deserialize(SCPDoorCollection, TotalData, SaveControlProtocol.DataType.SCPDoor_isLocked);
                    }
                    catch (System.Exception)
                    {
                    }
                    try
                    {
                        SaveControlProtocol.Deserialize(SCPDoorCollection, TotalData, SaveControlProtocol.DataType.SCPDoor_LockMessage);
                    }
                    catch (System.Exception)
                    {
                    }
                    try
                    {
                        SaveControlProtocol.Deserialize(StoryNodes, TotalData, SaveControlProtocol.DataType.Story_Node_isStarted);
                    }
                    catch (System.Exception)
                    {
                    }
                    try
                    {
                        //Doors
                        if (DoorSaveMethod == 0)
                        {
                            //Old fashion way
                            List<GameObject> gameObjects = new List<GameObject>();
                            var c = DoorsCollection.childCount;
                            for (int i = 0; i < c; i++)
                            {
                                gameObjects.Add(DoorsCollection.GetChild(i).Find("Door1").gameObject);
                                gameObjects.Add(DoorsCollection.GetChild(i).Find("Door2").gameObject);
                            }
                            SaveControlProtocol.Deserialize(gameObjects.ToArray(), TotalData, SaveControlProtocol.DataType.SCPDoor_OpenState);
                        }else if (DoorSaveMethod == 1)
                        {
                            List<GameObject> gameObjects = new List<GameObject>();
                            var c = DoorsCollection.childCount;
                            for (int i = 0; i < c; i++)
                            {
                                gameObjects.Add(DoorsCollection.GetChild(i).gameObject);
                            }
                            SaveControlProtocol.Deserialize(gameObjects.ToArray(), TotalData, SaveControlProtocol.DataType.SCPDoor_OpenState);
                        }
                    }
                    catch (System.Exception)
                    {
                    }
                }
                catch (System.Exception e)
                {
                    Debug.Log("Error on loading:" + e);
                }
            }
            {
                {
                    //GameInfo.CurrentGame.PlayerHealth.MaxHealth = 100;
                    //GameInfo.CurrentGame.PlayerHealth.CurrentHealth = GameInfo.CurrentGame.LoadedHP;
                }
                Player.GetComponent<SCPEntity>().CurrentHealth =
                Player.GetComponent<SCPEntity>().MaxHealth;
                Player.GetComponent<SCPEntity>().ChangeHealth(GameInfo.CurrentGame.LoadedHP - 100);
            }
            {
                try
                {
                    string StepSetName = GameInfo.CurrentGame.FlagsGroup["FootStep"];
                    if (StepSetName == "Normal")
                    {
                        GameInfo.CurrentGame.FirstPerson.footStepSounds = GameInfo.CurrentGame.currentFootStepSFXManager.NormalSteps.ToArray();
                    }
                    else
                        GameInfo.CurrentGame.FirstPerson.footStepSounds = GameInfo.CurrentGame.currentFootStepSFXManager.StepCollections[StepSetName].ToArray();
                }
                catch (System.Exception)
                {

                }
            }
            StartCoroutine(LoseProtection());
        }
        public IEnumerator LoseProtection()
        {
            {
                yield return new WaitForSeconds(1);

                if (GameInfo.CurrentGame.saveControlProtocolMode == GameInfo.SaveControlProtocolMode.Load)
                {
                    GameInfo.CurrentGame.LoadGI_PlayerInfo(Player);
                }
                if (GameInfo.CurrentGame.saveControlProtocolMode == GameInfo.SaveControlProtocolMode.Enter)
                {

                    if (GameInfo.CurrentGame.EnterSource == GameInfo.EnterSourceType.Lift)
                    {
                        {
                            var lp = Player.transform.localPosition;
                            lp.x = LiftSpawnPosition[0];
                            lp.y = LiftSpawnPosition[1];
                            lp.z = LiftSpawnPosition[2];
                            Player.transform.localPosition = lp;
                        }
                        {
                            var lp = Player.transform.localRotation;
                            var angle = lp.eulerAngles;
                            angle.x = LiftSpawnRotation[0];
                            angle.y = LiftSpawnRotation[1];
                            angle.z = LiftSpawnRotation[2];
                            lp.eulerAngles = angle;
                            Player.transform.localRotation = lp;
                        }
                    }
                    else
                    if (GameInfo.CurrentGame.EnterSource == GameInfo.EnterSourceType.Stair1)
                    {
                        {
                            var lp = Player.transform.localPosition;
                            lp.x = Stair01SpawnPosition[0];
                            lp.y = Stair01SpawnPosition[1];
                            lp.z = Stair01SpawnPosition[2];
                            Player.transform.localPosition = lp;
                        }
                        {
                            var lp = Player.transform.localRotation;
                            var angle = lp.eulerAngles;
                            angle.x = Stair01SpawnRotation[0];
                            angle.y = Stair01SpawnRotation[1];
                            angle.z = Stair01SpawnRotation[2];
                            lp.eulerAngles = angle;
                            Player.transform.localRotation = lp;
                        }
                    }
                    else
                    if (GameInfo.CurrentGame.EnterSource == GameInfo.EnterSourceType.Tunnel1)
                    {
                        {
                            var lp = Player.transform.position;
                            lp.x = Tunnel01SpawnPosition[0];
                            lp.y = Tunnel01SpawnPosition[1];
                            lp.z = Tunnel01SpawnPosition[2];
                            Player.transform.position = lp;
                        }
                        {
                            var lp = Player.transform.localRotation;
                            var angle = lp.eulerAngles;
                            angle.x = Tunnel01SpawnRotation[0];
                            angle.y = Tunnel01SpawnRotation[1];
                            angle.z = Tunnel01SpawnRotation[2];
                            lp.eulerAngles = angle;
                            Player.transform.localRotation = lp;
                        }
                    }
                }
                yield return new WaitForSeconds(1);
                Player.GetComponent<SCPEntity>().Immortal = false;
            }
        }
        public override void Save()
        {

            Dictionary<string, string> TotalData = new Dictionary<string, string>();
            {
                var temp = SaveControlProtocol.Serialize(SCPDoorCollection, SaveControlProtocol.DataType.SCPDoor_isLocked);
                TotalData = SaveControlProtocol.Merge(TotalData, temp);
            }
            {
                var temp = SaveControlProtocol.Serialize(SCPDoorCollection, SaveControlProtocol.DataType.SCPDoor_LockMessage);
                TotalData = SaveControlProtocol.Merge(TotalData, temp);
            }
            {
                var temp = SaveControlProtocol.Serialize(StoryNodes, SaveControlProtocol.DataType.Story_Node_isStarted);
                TotalData = SaveControlProtocol.Merge(TotalData, temp);
            }
            {
                var temp = SaveControlProtocol.Serialize(objectsToCheckActive, SaveControlProtocol.DataType.GameObjectActive);
                TotalData = SaveControlProtocol.Merge(TotalData, temp);
            }
            {
                var temp = SaveControlProtocol.Serialize(objectsToLogLocationAndRotation, SaveControlProtocol.DataType.GameObjectLocation);
                TotalData = SaveControlProtocol.Merge(TotalData, temp);
            }
            {
                var temp = SaveControlProtocol.Serialize(objectsToLogLocationAndRotation, SaveControlProtocol.DataType.GameObjectRotation);
                TotalData = SaveControlProtocol.Merge(TotalData, temp);
            }
            {
                if (DoorSaveMethod == 0)
                {
                    List<GameObject> gameObjects = new List<GameObject>();
                    var c = DoorsCollection.childCount;
                    for (int i = 0; i < c; i++)
                    {
                        gameObjects.Add(DoorsCollection.GetChild(i).Find("Door1").gameObject);
                        gameObjects.Add(DoorsCollection.GetChild(i).Find("Door2").gameObject);
                        //try
                        //{
                        //    var d1 = cd.GetChild(0);
                        //    gameObjects.Add(d1.gameObject);
                        //}
                        //catch (System.Exception)
                        //{
                        //}
                        //try
                        //{
                        //    var d2 = cd.GetChild(1);
                        //    gameObjects.Add(d2.gameObject);

                        //}
                        //catch (System.Exception)
                        //{
                        //}
                    }
                    var temp = SaveControlProtocol.Serialize(gameObjects.ToArray(), SaveControlProtocol.DataType.SCPDoor_OpenState);
                    TotalData = SaveControlProtocol.Merge(TotalData, temp);
                }
                else if(DoorSaveMethod==1)
                {
                    List<GameObject> gameObjects = new List<GameObject>();
                    var c = DoorsCollection.childCount;
                    for (int i = 0; i < c; i++)
                    {
                        gameObjects.Add(DoorsCollection.GetChild(i).gameObject);
                    }
                    SaveControlProtocol.Merge(TotalData,SaveControlProtocol.Serialize(gameObjects.ToArray(), SaveControlProtocol.DataType.SCPDoor_OpenState));
                }
            }
            {
                //GameInfo.CurrentGame.PlayerLocation
                GameInfo.CurrentGame.PlayerLocation[0] = Player.transform.localPosition.x;
                GameInfo.CurrentGame.PlayerLocation[1] = Player.transform.localPosition.y;
                GameInfo.CurrentGame.PlayerLocation[2] = Player.transform.localPosition.z;
            }
            {
                //GameInfo.CurrentGame.PlayerLocation
                GameInfo.CurrentGame.PlayerRotation[0] = Player.transform.localRotation.eulerAngles.x;
                GameInfo.CurrentGame.PlayerRotation[1] = Player.transform.localRotation.eulerAngles.y;
                GameInfo.CurrentGame.PlayerRotation[2] = Player.transform.localRotation.eulerAngles.z;
            }
            GameInfo.CurrentGame.SaveGeneralInfo();
            SaveControlProtocol.TargetFile = GameInfo.CurrentGame.GetCurrentSceneSaveName();
            SaveControlProtocol.Save(TotalData);

        }
        // Update is called once per frame
        void Update()
        {

        }

        public override void Load()
        {
        }
    }

}