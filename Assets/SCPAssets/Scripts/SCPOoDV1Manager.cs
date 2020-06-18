using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
namespace Site13Kernel.Experimentals.OoD.V1
{

    public class SCPOoDV1Manager : MonoBehaviour
    {
        public static bool ContinueCheck = true;
        public static Task CheckTask;
        static List<SCPOoDV1> OoDs = new List<SCPOoDV1>();
        static List<Vector3> LOoDs = new List<Vector3>();
        static List<bool> CheckedVisibility = new List<bool>();
        public float MinUpdateTime = 1f;
        static string TaskID = "";
        void Start()
        {
            ContinueCheck = false;
            TaskID = System.Guid.NewGuid().ToString();
            GameInfo.CurrentGame.CurrentOoDManager = this;
            OoDs = GameObject.FindObjectsOfType<SCPOoDV1>().ToList();
            LOoDs = Site_13ToolLib.Data.CollectionTool.GenerateList<Vector3>(OoDs.Count, Vector3.zero);
            for (int i = 0; i < OoDs.Count; i++)
            {
                LOoDs[i] = OoDs[i].transform.position;
            }
            CheckedVisibility = Site_13ToolLib.Data.CollectionTool.GenerateList<bool>(OoDs.Count, true);
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
                            for (int i = 0; i < OoDs.Count; i++)
                            {

                                if (Vector3.Distance(LOoDs[i], Player) > OoDs[i].ViewDistance)
                                {
                                    CheckedVisibility[i] = false;
                                }
                                else
                                {
                                    CheckedVisibility[i] = true;
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
            for (int i = 0; i < OoDs.Count; i++)
            {
                if (OoDs[i].gameObject.activeSelf != CheckedVisibility[i])
                {
                    OoDs[i].gameObject.SetActive(CheckedVisibility[i]);
                }
            }
        }
      
        void Update()
        {
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