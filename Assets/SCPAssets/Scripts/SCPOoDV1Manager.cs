using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Site13Kernel.Experimentals.OoD.V1
{

    public class SCPOoDV1Manager : MonoBehaviour
    {
        public static bool ContinueCheck = true;
        public static Task CheckTask;
        public static Dictionary<string, List<SCPOoDV1>> OoDs = new Dictionary<string, List<SCPOoDV1>>();
        public static Dictionary<string, List<Vector3>> LOoDs = new Dictionary<string, List<Vector3>>();
        public static Dictionary<string, List<bool>> CheckedVisibility = new Dictionary<string, List<bool>>();
        public float MinUpdateTime = 1f;
        public WorkMode workMode= WorkMode.MainManager;
        public enum WorkMode
        {
            MainManager,SubManager
        }
        static string TaskID = "";
        void Start()
        {
            if(workMode== WorkMode.SubManager)
            {
                //Codes here...
                //GameInfo.CurrentGame.CurrentOoDManager.OoDs.Add(SceneManager.GetActiveScene().name,FindObjectOfType<SCPOoDV1>())
                //Codes here...
                return;
            }
            ContinueCheck = false;
            TaskID = System.Guid.NewGuid().ToString();
            GameInfo.CurrentGame.CurrentOoDManager = this;
            OoDs.Add("MainScene", FindObjectsOfType<SCPOoDV1>().ToList());
            LOoDs.Add("MainScene", Site_13ToolLib.Data.CollectionTool.GenerateList(OoDs["MainScene"].Count, Vector3.zero));
            foreach (var item in OoDs)
            {
                for (int i = 0; i < item.Value.Count; i++)
                {
                    LOoDs["MainScene"][i] = OoDs["MainScene"][i].transform.position;
                }
            }
            CheckedVisibility.Add("MainScene", Site_13ToolLib.Data.CollectionTool.GenerateList<bool>(OoDs["MainScene"].Count, true));
            CheckTask = Task.Run(async () => await CheckMethod(TaskID));
            Player = GameInfo.CurrentGame.FirstPerson.transform.position;
        }
        Vector3 Player = Vector3.zero;
        async Task CheckMethod(string tID)
        {
            try
            {

                while (TaskID == tID)
                {
                    try
                    {
                        if (Player != Vector3.zero)
                        {
                            foreach (var item in OoDs)
                            {
                                for (int i = 0; i < item.Value.Count; i++)
                                {

                                    if (Vector3.Distance(LOoDs[item.Key][i], Player) > OoDs[item.Key][i].ViewDistance)
                                    {
                                        CheckedVisibility[item.Key][i] = false;
                                    }
                                    else
                                    {
                                        CheckedVisibility[item.Key][i] = true;
                                    }
                                }
                            }
                        }
                    }
                    catch (System.Exception e)
                    {
                    }
                    await Task.Delay((int)(MinUpdateTime * 500));
                    /**
                     * Since the task is running on a separated thread
                     * The update delta time can be shorter.
                     **/
                }
            }
            catch (System.Exception e)
            {
                dbg = e.Message; return;
            }
        }
        string dbg = "";
        float deltaTime = 0.0f;
        void ApplyOoD()
        {
            foreach (var item in OoDs)
            {

                for (int i = 0; i < item.Value.Count; i++)
                {
                    if (OoDs[item.Key][i].gameObject.activeSelf != CheckedVisibility[item.Key][i])
                    {
                        OoDs[item.Key][i].gameObject.SetActive(CheckedVisibility[item.Key][i]);
                    }
                }
            }
        }

        void Update()
        {
            if(workMode == WorkMode.SubManager)
            {
                //SubManagers do not do any work, let main manager do it!
                return;
            }
            deltaTime += Time.unscaledDeltaTime;
            if (deltaTime >= MinUpdateTime)
            {
                {
                    try
                    {

                        Player = GameInfo.CurrentGame.FirstPerson.transform.position;
                    }
                    catch (System.Exception)
                    {

                    }
                    ApplyOoD();
                }
                deltaTime = 0.0f;
            }
            if (dbg != "")
            {
                Debug.LogError(dbg);
            }
        }
    }

}