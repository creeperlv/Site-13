using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetUp : MonoBehaviour
{
    public GameObject FPS;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(story());
    }
    IEnumerator story()
    {
        yield return new WaitForSeconds(20.9f);
        FPS.SetActive(true);
        GameObject.Destroy(this.gameObject);
    }
}
