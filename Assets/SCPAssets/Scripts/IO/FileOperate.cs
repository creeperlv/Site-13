using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Linq;
using System;
using System.Text;

namespace Site13Kernel.IO
{
    /// <summary>
    /// Save System V 1
    /// Base version completed in 21/1/2019 at 22:10
    /// </summary>
    /**
     * Target Content:
     * <Save>
     *  <SingleData Name="AAAAA" Data="AAAA"/>
     *  <SingleData Name="AAAAB" Data="AAAA"/>
     *  <SingleData Name="AAAAC" Data="AAAA"/>
     * </Save>
     **/
    public class SaveControlProtocol
    {
        private static string targetFile = "";

        public static string TargetFile { get => targetFile; set => targetFile = "./Saves/" + GameInfo.CurrentGame.SaveName + "/" + value + ".sav"; }

        public static Dictionary<string, string> Load()
        {
            return Load(TargetFile);
        }
        public static Dictionary<string, string> Load(string file, bool VerFlag = true)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            File.WriteAllText("./TMP",DecodeBase64(File.ReadAllText(file)));
            var cont = File.ReadAllLines("./TMP");
            File.Delete("./TMP");
            XDocument doc;
            if (VerFlag)
            {
                doc = XDocument.Parse(cont[1]);
            }
            else
            {
                doc = XDocument.Parse(cont[0]);
            }
            var e = doc.Root.Elements();
            foreach (var item in e)
            {
                var a = (string)item.Attribute("Name");
                var b = (string)item.Attribute("Data");
                data.Add(a, b.Replace(":R;","\r\n"));
            }
            return data;

        }

        public static string DecodeBase64( string result)
        {
            string decode = "";
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                decode = Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                decode = result;
            }
            return decode;
        }
        public static string EncodeBase64( string source)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(source);
            string encode;
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = source;
            }
            return encode;
        }

        public static void Save(Dictionary<string, string> data)
        {
            Directory.CreateDirectory("./Saves");
            Directory.CreateDirectory("./Saves/" + GameInfo.CurrentGame.SaveName + "/");
            Save(data, TargetFile);
        }
        public static void Save(Dictionary<string, string> data, string file,string rootName="Save", bool VerFlag = true)
        {

            File.CreateText(file).Close();
            string pref = "";
            if (VerFlag)
            {
                pref = "SCP 1\r\n";
            }
            //Save Control Protocol version 1;
            XmlDocument xmlDocument = new XmlDocument();
            var root = xmlDocument.CreateElement(rootName);
            {
                foreach (var item in data)
                {
                    var sd = xmlDocument.CreateElement("SingleData");
                    sd.SetAttribute("Name", item.Key);
                    sd.SetAttribute("Data", item.Value);
                    root.AppendChild(sd);
                }
            }
            xmlDocument.AppendChild(root);
            File.WriteAllText(file, EncodeBase64(pref+xmlDocument.OuterXml));
        }
        public static List<string> GetList()
        {
            List<string> res = new List<string>();
            try
            {
                var l = Directory.GetDirectories("./Saves/");
                foreach (var item in l)
                {
                    var tmp = item.Substring("./Saves/".Length);
                    Debug.Log(tmp);
                    res.Add(tmp);
                }
            }
            catch (System.Exception)
            {
            }
            return res;
        }
        public enum DataType
        {
            GameObjectActive,
            GameObjectLocation,
            GameObjectRotation,
            SCPDoor_isLocked,
            SCPDoor_LockMessage,
            Story_Node_isStarted,
            SCPDoor_OpenState,
        }
        public static Dictionary<string, string> Serialize(System.Object[] Source, DataType dataType)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();
            switch (dataType)
            {
                case DataType.GameObjectActive:
                    for (int i = 0; i < Source.Length; i++)
                    {
                        var Casted = (GameObject)Source[i];
                        data.Add($"Active-{i}", "" + Casted.activeInHierarchy);
                    }
                    break;
                case DataType.GameObjectLocation:
                    for (int i = 0; i < Source.Length; i++)
                    {
                        var Casted = (GameObject)Source[i];
                        data.Add($"Location-{i}", "" + Casted.transform.localPosition.x + "," + Casted.transform.localPosition.y + "," + Casted.transform.localPosition.z);
                    }
                    break;
                case DataType.GameObjectRotation:

                    for (int i = 0; i < Source.Length; i++)
                    {
                        var Casted = (GameObject)Source[i];
                        data.Add($"Rotation-{i}", "" + Casted.transform.localRotation.x + "," + Casted.transform.localRotation.y + "," + Casted.transform.localRotation.z);
                    }
                    break;
                case DataType.SCPDoor_isLocked:
                    for (int i = 0; i < Source.Length; i++)
                    {
                        var Casted = (SCPDoor)Source[i];
                        try
                        {
                            data.Add($"SCPDoor.isLocked-{i}", "" + Casted.IsLocked);
                        }
                        catch (Exception)
                        {
                            data.Add($"SCPDoor.isLocked-{i}", "" + false);
                        }
                    }
                    break;
                case DataType.SCPDoor_LockMessage:
                    for (int i = 0; i < Source.Length; i++)
                    {
                        var Casted = (SCPDoor)Source[i];
                        SCPDoor doors = Casted.GetComponent<SCPDoor>();
                        try
                        {

                            data.Add($"SCPDoor.LockMessage-{i}", "" + doors.LockMessage);

                        }
                        catch (Exception)
                        {

                            data.Add($"SCPDoor.LockMessage-{i}", "The door is locked.");
                        }
                    }
                    break;
                case DataType.Story_Node_isStarted:
                    for (int i = 0; i < Source.Length; i++)
                    {
                        var Casted = (SCPStoryNodeBaseCode)Source[i];
                        SCPStoryNodeBaseCode node = Casted.GetComponent<SCPStoryNodeBaseCode>();
                        var nodes=Casted.GetComponents<SCPStoryNodeBaseCode>();
                        for (int index = 0; index < nodes.Length; index++)
                        {
                            data.Add($"StoryNode.started-{i}-{index}", "" + node.isStarted);
                        }
                    }
                    break;
                case DataType.SCPDoor_OpenState:
                    for (int i = 0; i < Source.Length; i++)
                    {
                        var Casted = (GameObject)Source[i];
                        data.Add($"DoorState-{i}", "" + Casted.activeInHierarchy);
                    }
                    break;
                default:
                    break;
            }
            return data;
        }
        public enum MergeMethod
        {
            Replace,
            CreateNew
        }
        public static Dictionary<string, string> Merge(Dictionary<string, string> Master, Dictionary<string, string> Slave, MergeMethod mergeMethod = MergeMethod.Replace)
        {
            foreach (var item in Slave)
            {
                switch (mergeMethod)
                {
                    case MergeMethod.Replace:
                        {
                            if (Master.ContainsKey(item.Key))
                            {
                                Master[item.Key] = item.Value;
                            }
                            else
                                Master.Add(item.Key, item.Value);
                        }
                        break;
                    case MergeMethod.CreateNew:
                        break;
                    default:
                        break;
                }
            }
            return Master;
        }
        static float[] Resolve(string value)
        {
            float[] result = new float[3];
            var tmp = value.Split(',');
            result[0] = float.Parse(tmp[0]);
            result[1] = float.Parse(tmp[1]);
            result[2] = float.Parse(tmp[2]);
            return result;
        }
        public static void Deserialize(UnityEngine.Object[] Target, Dictionary<string, string> Source, DataType dataType)
        {
            switch (dataType)
            {
                case DataType.GameObjectActive:
                    foreach (var item in Source)
                    {
                        if (item.Key.StartsWith("Active"))
                        {
                            var id = int.Parse(item.Key.Substring("Active-".Length));
                            (Target[id] as GameObject).SetActive(bool.Parse(item.Value));
                            if (!(Target[id] as GameObject).activeInHierarchy)
                            {
                                try
                                {
                                    //(Target[id] as GameObject).GetComponent<SCPBaseScript>().Start();
                                }
                                catch (System.Exception)
                                {
                                }
                            }
                        }
                    }
                    break;
                case DataType.GameObjectLocation:
                    foreach (var item in Source)
                    {
                        if (item.Key.StartsWith("Location"))
                        {
                            var id = int.Parse(item.Key.Substring("Location-".Length));
                            var lp = (Target[id] as GameObject).transform.localPosition;
                            var position = Resolve(item.Value);
                            lp.x = position[0];
                            lp.y = position[1];
                            lp.z = position[2];
                            (Target[id] as GameObject).transform.localPosition = lp;
                        }
                    }
                    break;
                case DataType.GameObjectRotation:
                    foreach (var item in Source)
                    {
                        if (item.Key.StartsWith("Rotation"))
                        {
                            var id = int.Parse(item.Key.Substring("Rotation-".Length));
                            var lp = (Target[id] as GameObject).transform.localRotation;
                            var position = Resolve(item.Value);
                            lp.x = position[0];
                            lp.y = position[1];
                            lp.z = position[2];
                            (Target[id] as GameObject).transform.localRotation = lp;
                        }
                    }
                    break;
                case DataType.SCPDoor_isLocked:
                    foreach (var item in Source)
                    {
                        if (item.Key.StartsWith("SCPDoor.isLocked"))
                        {
                            var id = int.Parse(item.Key.Substring("SCPDoor.isLocked-".Length));
                            (Target[id] as SCPDoor).IsLocked = (bool.Parse(item.Value));
                        }
                    }
                    break;
                case DataType.SCPDoor_LockMessage:
                    foreach (var item in Source)
                    {
                        if (item.Key.StartsWith("SCPDoor.LockMessage"))
                        {
                            var id = int.Parse(item.Key.Substring("SCPDoor.LockMessage-".Length));
                            (Target[id] as SCPDoor).LockMessage = item.Value;
                        }
                    }
                    break;
                case DataType.Story_Node_isStarted:
                    foreach (var item in Source)
                    {
                        if (item.Key.StartsWith("StoryNode.started"))
                        {
                            var strprase = item.Key.Split('-');
                            var id = int.Parse(strprase[1]);
                            var NodeID = int.Parse(strprase[2]);
                            (Target[id] as SCPStoryNodeBaseCode).GetComponents<SCPStoryNodeBaseCode>()[NodeID].isStarted = (bool.Parse(item.Value));
                        }
                    }
                    break;
                case DataType.SCPDoor_OpenState:
                    foreach (var item in Source)
                    {
                        if (item.Key.StartsWith("DoorState"))
                        {
                            var id = int.Parse(item.Key.Substring("DoorState-".Length));
                            (Target[id] as GameObject).SetActive(bool.Parse(item.Value));
                            if (!(Target[id] as GameObject).activeInHierarchy)
                            {
                                try
                                {
                                    //(Target[id] as GameObject).GetComponent<SCPBaseScript>().Start();
                                }
                                catch (System.Exception eeeeee)
                                {
                                    Debug.LogError("Error on running disabled script" + eeeeee.Message);
                                }
                            }
                        }
                    }
                    break;
                default:
                    break;
            }
        }
    }
}