using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Site13Kernel
{

    public class DebugConsole : MonoBehaviour
    {
        public Button CloseBtn;
        public Button HealBtn;
        public Button MakeImmortalBtn;
        public Button MakeMortalBtn;

        #region SceneSettings
        public InputField SceneIndex;
        public Button Jump_None;
        public Button Jump_Stair1;
        public Button Jump_Stair2;
        public Button Jump_Lift;
        public Button Jump_Tunnel1;
        #endregion

        #region CONSOLE

        public TMP_InputField ConsoleInput;

        public TMP_Text ConsoleOutput;

        public Scrollbar ChatScrollbar;

        #endregion
        public List<GameObject> UIObjes;
        // Start is called before the first frame update
        void Start()
        {
            ConsoleInput.onSubmit.AddListener(ResolveCommand);
            CloseBtn.onClick.AddListener(delegate ()
            {

                this.gameObject.SetActive(false);
                GameInfo.CurrentGame.isPaused = false;
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                GameInfo.CurrentGame.FirstPerson.enabled = true;
                AudioListener.pause = false;
            });
            HealBtn.onClick.AddListener(delegate ()
            {
                GameInfo.CurrentGame.PlayerHealth.ChangeHealth(1000);
            });
            MakeImmortalBtn.onClick.AddListener(delegate ()
            {
                GameInfo.CurrentGame.PlayerHealth.Immortal = true;
            });
            MakeMortalBtn.onClick.AddListener(delegate ()
            {
                GameInfo.CurrentGame.PlayerHealth.Immortal = false;
            });
            Jump_Tunnel1.onClick.AddListener(delegate ()
            {
                GameInfo.CurrentGame.isPaused = false;
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                GameInfo.CurrentGame.FirstPerson.enabled = true;
                AudioListener.pause = false;
                GameInfo.CurrentGame.EnterSource = GameInfo.EnterSourceType.Tunnel1;
                GameInfo.CurrentGame.NextScene = int.Parse(SceneIndex.text);
                GameInfo.CurrentGame.saveControlProtocolMode = GameInfo.SaveControlProtocolMode.Enter;
                SceneManager.LoadScene(2);
            });
            Jump_Lift.onClick.AddListener(delegate ()
            {
                GameInfo.CurrentGame.isPaused = false;
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                GameInfo.CurrentGame.FirstPerson.enabled = true;
                AudioListener.pause = false;
                GameInfo.CurrentGame.NextScene = int.Parse(SceneIndex.text);
                GameInfo.CurrentGame.EnterSource = GameInfo.EnterSourceType.Lift;
                GameInfo.CurrentGame.saveControlProtocolMode = GameInfo.SaveControlProtocolMode.Enter;
                SceneManager.LoadScene(2);
            });

        }
        void ResolveCommand(string s)
        {
            string cmd = s;
            string arg = "";
            if (s.IndexOf(" ") > 0)
            {
                cmd = s.Substring(0, s.IndexOf(" "));
                arg = s.Substring(s.IndexOf(" ") + 1);
            }
            if (s.ToUpper() == "crazy diamond".ToUpper() || s.ToUpper() == "クレイジーダイヤモンド" || s.ToUpper() == "疯狂钻石")
            {
                GameInfo.CurrentGame.PlayerHealth.ChangeHealth(1000);
                switch (UnityEngine.Random.Range(0, 3))
                {
                    case 0:
                        ConsoleOutput.text += Environment.NewLine + "<size=28>ドララララララ！</size>";
                        break;
                    case 1:
                        ConsoleOutput.text += Environment.NewLine + "<size=28>DORARARARARARA!</size>";
                        break;
                    case 2:
                        ConsoleOutput.text += Environment.NewLine + "<size=32>無敵のスタープラチナで何とかして下さいよォ――――ッ！</size>";
                        break;
                    default:
                        break;
                }
            }
            else if (s.ToUpper() == "ORAORA" || s.ToUpper() == "オラオラ" || s.ToUpper() == "欧拉欧拉")
            {
                GameInfo.CurrentGame.PlayerHealth.Immortal = true;
                switch (UnityEngine.Random.Range(0, 3))
                {
                    case 0:
                        ConsoleOutput.text += Environment.NewLine + "<size=28>STAR PLATINUM!</size>";
                        break;
                    case 1:
                        ConsoleOutput.text += Environment.NewLine + "<size=28>There's a reason you lost, DIO. One simple reason... You pissed me off.</size>";
                        break;
                    case 2:
                        ConsoleOutput.text += Environment.NewLine + "<size=28>やれやれだ☆ぜ</size>";
                        break;
                    default:
                        break;
                }
            }
            else if (s.ToUpper() == "MUDAMUDA" || s.ToUpper() == "無駄無駄" || s.ToUpper() == "むだむだ" || s.ToUpper() == "木大木大")
            {
                GameInfo.CurrentGame.PlayerHealth.Immortal = false;
                switch (UnityEngine.Random.Range(0, 2))
                {
                    case 0:
                        ConsoleOutput.text += Environment.NewLine + "<size=36>WRYYYYYYYYYYY!</size>";
                        break;
                    case 1:
                        ConsoleOutput.text += Environment.NewLine + "<size=36>最高に「ハイ！」ってやつだアアアアアアハハハハハハハハハハーッ!</size>";
                        break;
                    default:
                        break;
                }
            }else if (s.ToUpper() == "HIDEUI")
            {
                foreach (var item in UIObjes)
                {
                    item.SetActive(false);
                }
            }else if (s.ToUpper() == "SHOWUI")
            {
                foreach (var item in UIObjes)
                {
                    item.SetActive(true);
                }
            }
            else if (s.ToUpper() == "HERMIT PURPLE"||s.ToUpper() == "隐者之紫")
            {
                ConsoleOutput.text += Environment.NewLine + "<color=#DF20DF>Scene IDs of maps:</size>";
                ConsoleOutput.text += Environment.NewLine + "<size=20>Section B</size>";
                ConsoleOutput.text += Environment.NewLine + "\tLevel 1 = 5";
                ConsoleOutput.text += Environment.NewLine + "\tLevel 2 = 6";
                ConsoleOutput.text += Environment.NewLine + "\tLevel 3 = 7";
                ConsoleOutput.text += Environment.NewLine + "<size=20>Section C</size>";
                ConsoleOutput.text += Environment.NewLine + "\tLevel 1 = 9";
                ConsoleOutput.text += Environment.NewLine + "<size=20>MTF, Delta-12</size>";
                ConsoleOutput.text += Environment.NewLine + "\tOnGround01 = 14";
                ConsoleOutput.text += Environment.NewLine + "<size=20>Other</size>";
                ConsoleOutput.text += Environment.NewLine + "\tFPS Test Map = 10";
                ConsoleOutput.text += Environment.NewLine + "\tPreBreach1 = 12";
                ConsoleOutput.text += Environment.NewLine;
                ConsoleOutput.text += Environment.NewLine + "\tOld Main Menu = 11";
            }
            if (s.ToUpper().StartsWith("Sticky Fingers".ToUpper()))
            {
                var jumpArg = s.Substring("Sticky Fingers".Length).Trim().Split(' ');
                switch (jumpArg[0].ToUpper())
                {
                    case "NONE":
                        GameInfo.CurrentGame.EnterSource = GameInfo.EnterSourceType.None;
                        break;
                    case "LIFT":
                        GameInfo.CurrentGame.EnterSource = GameInfo.EnterSourceType.Lift;
                        break;
                    case "STAIR1":
                        GameInfo.CurrentGame.EnterSource = GameInfo.EnterSourceType.Stair1;
                        break;
                    case "STAIR2":
                        GameInfo.CurrentGame.EnterSource = GameInfo.EnterSourceType.Stair2;
                        break;
                    case "TUNNEL1":
                        GameInfo.CurrentGame.EnterSource = GameInfo.EnterSourceType.Tunnel1;
                        break;
                    default:
                        break;
                }
                GameInfo.CurrentGame.NextScene = int.Parse(jumpArg[1]);
                GameInfo.CurrentGame.saveControlProtocolMode = GameInfo.SaveControlProtocolMode.Enter;
                try
                {
                    switch (jumpArg[2].ToUpper())
                    {
                        case "ENTER":
                            GameInfo.CurrentGame.saveControlProtocolMode = GameInfo.SaveControlProtocolMode.Enter;
                            break;
                        case "LOAD":
                            GameInfo.CurrentGame.saveControlProtocolMode = GameInfo.SaveControlProtocolMode.Load;
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception)
                {
                }
                GameInfo.CurrentGame.isPaused = false;
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                GameInfo.CurrentGame.FirstPerson.enabled = true;
                AudioListener.pause = false;
                SceneManager.LoadScene(2);
            }
            ConsoleInput.text = "";
        }
    }

}