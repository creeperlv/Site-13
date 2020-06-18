using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopSounds : MonoBehaviour
{
    public List<AudioClip> Radios;
    // Start is called before the first frame update
    void Start()
    {

    }
    int selection;
    // Update is called once per frame
    void Update()
    {
        if (GetComponent<AudioSource>().isPlaying == false)
        {
            if (selection == Radios.Count-1)
            {
                selection = 0;
            }
            GetComponent<AudioSource>().clip = Radios[selection];
            GetComponent<AudioSource>().Play();
            selection++;
        }
    }
}
