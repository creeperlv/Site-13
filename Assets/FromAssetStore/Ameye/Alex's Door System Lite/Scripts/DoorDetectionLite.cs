using Site13Kernel;
using UnityEngine;
using UnityEngine.UI;
[HelpURL("https://alexdoorsystem.github.io/liteversion/")]
public class DoorDetectionLite : MonoBehaviour
{
    public float Reach = 4.0F;
    [HideInInspector] public bool InReach;
    public string Character = "e";
    public GameObject ReadingPanel;
    public Image Reading;
    public GameObject RealReading;
    public bool IsTemporaryDisabled = false;
    //public Color DebugRayColor;
    SCPFirstController aIO;
    void Start()
    {
        gameObject.name = "Player";
        gameObject.tag = "Player";
        aIO = GetComponent<SCPFirstController>();
    }

    void Update()
    {
        if (IsTemporaryDisabled == false)
            if (Input.GetKey(Character))
            {
                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0F));
                Debug.DrawRay(ray.origin, ray.direction, Color.green, 10f);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, Reach, 1 << 0, QueryTriggerInteraction.Ignore))
                {
                    if (hit.collider.tag == "Interactive")
                    {
                        InReach = true;

                        GameObject Prop = hit.transform.gameObject;

                        SCPInteractive PropCode = Prop.GetComponent<SCPInteractive>();

                        if (PropCode.isOperating == false)
                        {
                            var code = hit.collider.GetComponent<SCPInteractive>();
                            if (code.isRequireLockCamera == true)
                            {
                                ReadingPanel.SetActive(true);
                                RealReading.SetActive(true);
                                aIO.enabled = false;
                                Cursor.lockState = CursorLockMode.None;
                                Cursor.visible = true;
                                var c = RealReading.transform.childCount;
                                for (int i = 0; i < c; i++)
                                {
                                    RealReading.transform.GetChild(i).gameObject.SetActive(false);
                                }
                            }
                            StartCoroutine(code.Move());
                            StartCoroutine(code.Move(this.gameObject));
                        }
                    }

                    else
                    {
                        InReach = false;
                    }
                }
                else
                {
                    InReach = false;
                }
            }
    }
}
