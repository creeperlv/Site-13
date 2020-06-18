using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Site13Kernel
{
    public class Blinker : MonoBehaviour
    {
        bool willBlink=false;
        // Start is called before the first frame update
        void Start()
        {
            GameInfo.CurrentGame.PlayerBlink=this;
        }
        public void Blink()
        {
            willBlink = true;
        }
        float BlinkTime = 1.5f;
        public void Update()
        {
            if (willBlink)
            {

                BlinkTime -= Time.deltaTime;
                if (BlinkTime <= 0)
                {
                    {
                        //Fade blink
                        if (BlinkTime >= -0.25f)
                        {
                            try
                            {
                                var c = this.GetComponent<Image>().color;
                                c.a = (Math.Abs(BlinkTime) * 4);
                                this.GetComponent<Image>().color = c;
                            }
                            catch (System.Exception)
                            {
                            }
                        }
                        else if (BlinkTime > -1.5f)
                        {
                            try
                            {

                                var c = this.GetComponent<Image>().color;
                                c.a = 1 - (Math.Abs(BlinkTime) - 1.25f) * 4;
                                this.GetComponent<Image>().color = c;
                            }
                            catch (System.Exception)
                            {
                            }
                        }
                        else
                        {

                            var c = this.GetComponent<Image>().color;
                            c.a = 0;
                            this.GetComponent<Image>().color = c;
                            BlinkTime = 1.5f;
                            willBlink = false;
                        }
                    }
                }
            }
        }
    }
}
