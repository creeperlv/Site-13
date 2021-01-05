/// Original Code written and designed by Aeden C Graves.
///
///
///CHANGE LOG:
///
/// DATE || msg: "" || Author Signature: SNG || version VERSION
///
/// 06/07/19 || msg: "Added ability to toggle the ability to jump from the editor." || Author Signature: Adam Worrell || 19.5.12feu >> version 19.6.7cu
/// 05/12/19 || msg: "Fixed non dymanic footsteping. Remade crouching system to be more efficiant and added an input over ride. || Author Signature: Aedan Graves || version 19.3.22 cl >> 19.5.12feu
/// 03/22/19 || msg: "Cleaned up code" || Author Signature: Aedan Graves || version 19.3.19cu >> 19.3.22cl
/// 03/19/19 || msg: "Added a rudimentary slope detection system." || Author Signature: Aedan Graves || version 19.3.18a >> 19.3.19cu
/// 03/18/19 || msg: "Fixed Stamina" || Author Signature: Aedan Graves || version 19.3.11p >> 19.3.18a
/// 03/02/19 || msg: "Improved camera System" || Author Signature: Aecan Graves || version 19.3.2 >> 19.3.11p
/// 03/02/19 || msg: "Lowered maximum walk, sprint, and jump values" || Author Signature: Aedan Graves || version: 19.2.21 >> 19.3.2
/// 02/21/19 || msg: "Removed dynamic speed curve. Modified headbob logic || Author Signature: Aedan C Graves || version: 19.2.15 >> 19.2.21
/// 02/15/19 || msg: "Added Camera shake. Made it possable to disable camera movement when jumping and landing." || Author Signature: Aedan C Graves || version: 19.2.12 >> 19.2.15
/// 02/12/19 || msg: "Seperated Dynamic Footsteps from the Headbob calculations." || Author Signature: Aedan C Graves || version: 1.6b >> 19.2.12
/// 02/08/19 || msg "Added some more tooltips." || Author Signature: Aedan C Graves || version 1.6a >> 1.6b
/// 02/04/19 || msg "Changed crouch funtion to use an In Editor defined input axis" || Author Signature: Aedan C Graves || version 1.6 >> 1.6a
/// 12/13/18 || msg: "Added 'Custom' entry for Dynamic footstep system" || Author Signature: Aedan C Graves || version 1.5b >> 1.6
/// 12/11/18 || msg: "Added Volume control to Footstep and Jump/land SFX." || Author Signature: Aedan C Graves || version 1.5a >> 1.5b
/// 02/18/18 || msg: "Updated mouse rotation to allow pre-play rotiation." || Author Signature: Aedan C Graves || version 1.5 >> 1.5a
/// 01/31/18 || msg: "Changed Dynamic footstep system to use physics materials." || Author Signature: Aedan C Graves || version 1.4c >> 1.5
/// 12/19/17 || msg: "Added headbob passthrough variables" || Auther Signature: Aeden C Graves || version 1.4b >> 1.4c
/// 12/02/17 || msg: "Made camera movement toggleable" || Auther Signature: Aeden C Graves || version 1.4a >> 1.4b
/// 10/16/17 || msg: "Made all sounds optional." || Author Signature: Aedan C Graves || version 1.4 >> 1.4a
/// 10/09/17 || msg: "Added Optional FOV Kick" || Author Signature: Aedan C Graves || version 1.3b >> 1.4
/// 10/08/17 || msg: "Improved Dynamic Footsteps." || Author Signature: Aedan C Graves || version 1.3a >> 1.3b
/// 10/07/17 || msg: "BetaTesting Class" || Author Signature: Aedan C Graves || version 1.3 >> 1.3a
/// 10/07/17 || msg: "Added Optional Dynamic Footsteps. Added optional Dynamic Speed Curve." || Author Signature: Aedan C Graves || version 1.2 >> 1.3
/// 10/03/17 || msg: "Added optional Crouch." || Author Signature: Aedan C Graves || version v1.1 >> v1.2
/// 09/26/17 || msg: "Fixed Headbobbing in mid air. Added a option for head bobbing, Added optional Stamina. Added Auto Crosshair Feature." || Author Signature: Aedan C Graves|| version v1.0 >> v1.1
/// 09/21/17 || msg: "Finished SMB FPS Logic." || Author Signature: Aedan C Graves || version v0.0 >> v1.0
///
/// 
/// 
/// Made changes that you think should come "Out of the box"? E-mail the modified Script with A new entry on the top of the Change log to: modifiedassets@aedangraves.info

using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using System.Collections.Generic;
using System.Collections;
using Site13Kernel;

[AddComponentMenu("First Person AIO")]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class FirstPersonAIO : MonoBehaviour
{

    #region Script Header and Cosmetics
    [Header("                       First Person All-in-One v19.6.7cu", order = 0)]
    [Space(30, order = 1)]
    #endregion

    #region Variables

    #region Input Settings

    #endregion

    #region Look Settings
    [Header("Mouse Rotation Settings", order = 2)]

    [Space(8, order = 3)]
    [Tooltip("Determines whether the player can move camera or not.")] public bool enableCameraMovement;
    [Tooltip("from center, how much - range in degrees - does the camera have to move up and down.")] [Range(0, 180)] public float rotationRange = 170;
    [Tooltip("Determines how sensitive the mouse is.")] [Range(0.01f, 35)] public float mouseSensitivity = 10f;
    [Tooltip("Mouse Smoothness.")] [Range(1, 100)] public float cameraSmoothing = 5f;
    [Tooltip("For Debuging or if You don't plan on having a pause menu or quit button.")] public bool lockAndHideCursor = false;
    [Tooltip("Camera that you wish to rotate.")] public Transform playerCamera;
    [Tooltip("Call this Coroutine externaly with duration ranging from 0.01 to 1, and a magnitude of 0.01 to 0.5.")] public bool enableCameraShake = false;
    internal Vector3 cameraStartingPosition;


    [SerializeField] [Tooltip("Automatically Create Crosshair")] private bool autoCrosshair;
    public Sprite Crosshair;

    [HideInInspector]
    public Vector3 targetAngles;
    private Vector3 followAngles;
    private Vector3 followVelocity;
    private Vector3 originalRotation;
    [Space(15, order = 4)]

    #endregion

    #region Movement Settings
    [Header("Movement Settings", order = 5)]
    [Space(8, order = 6)]

    [Tooltip("Determines whether the player can move.")] public bool playerCanMove = true;
    [Tooltip("If true; Left shift = Sprint. If false; Left Shift = Walk.")] [SerializeField] private bool walkByDefault = true;
    [Tooltip("Determines how fast Player walks.")] [Range(0.1f, 10)] public float walkSpeed = 4f;
    [Tooltip("Determines how fast Player Sprints.")] [Range(0.1f, 20)] public float sprintSpeed = 8f;
    [Tooltip("Determines how high Player Jumps.")] [Range(0.1f, 15)] public float jumpPower = 5f;
    [Tooltip("Determines if the player can jump.")] public bool canJump = true;
    [Tooltip("Determines if the jump button needs to be pressed down to jump, or if the player can hold the jump button to automaticly jump every time the it hits the ground.")] public bool canHoldJump;
    [Tooltip("Determines whether to use Stamina or not.")] [SerializeField] private bool useStamina = true;
    [Tooltip("Determines how quickly the players stamina runs out")] [SerializeField] [Range(0.1f, 9)] private float staminaDepletionSpeed = 2f;
    [Tooltip("Determines how much stamina the player has")] [SerializeField] [Range(0, 100)] public float Stamina = 50;
    [HideInInspector] public float speed;
    [HideInInspector] public float staminaInternal;
    [HideInInspector] public float walkSpeedInternal;
    [HideInInspector] public float sprintSpeedInternal;
    [HideInInspector] public float jumpPowerInternal;

    [System.Serializable]
    public class CrouchModifiers
    {
        [Tooltip("Determines whether to use Crouch or not.")] public bool useCrouch = true;
        [Tooltip("Name of the Input Axis you wish to use for crouching, this must be set up in the InputManager.")] public string CrouchInputAxis;
        [Tooltip("How much should the players walking speed be reduced while in the crouching state")] [Range(0.01f, 1.5f)] public float crouchWalkSpeedMultiplier = 0.5f;
        [Tooltip("How much should the players sprinting speed be reduced while in the crouching state")] [Range(0.01f, 1.5f)] public float crouchSprintSpeedMultiplier = 0.25f;
        [Tooltip("How much should the players jumping power be reduced while in the crouching state")] [Range(0f, 1.5f)] public float crouchJumpPowerMultiplier = 0f;
        [Tooltip("Toggle this to override the crouch input axis from another script.")] public bool crouchOverride;

        internal float colliderHeight;

    }
    public CrouchModifiers _crouchModifiers = new CrouchModifiers();
    [System.Serializable]
    public class FOV_Kick
    {
        [SerializeField] internal bool useFOVKick = false;
        [SerializeField] [Range(0, 10)] internal float FOVKickAmount = 4;
        [SerializeField] [Range(0.01f, 5)] internal float changeTime = 0.1f;
        [SerializeField] internal AnimationCurve KickCurve;
        internal bool fovKickReady = true;
        internal float fovStart;
    }
    public FOV_Kick fOVKick = new FOV_Kick();
    [System.Serializable]
    public class AdvancedSettings
    {
        [Tooltip("Changes the multiplication factor of the Engine's current gravitational force")] [Range(0.01f, 5.0f)] public float gravityMultiplier = 1.0f;
        public PhysicMaterial zeroFrictionMaterial;
        public PhysicMaterial highFrictionMaterial;
        [Space(10)]
        [Tooltip("Currently buggy; Determines if the slope detection/limiting system should be used.")] public bool useSlopeDetection = true;
        [Range(0, 89)] public float maxSlopeAngle = 70;
        [HideInInspector] public bool tooSteep;
        [HideInInspector] public RaycastHit surfaceAngleCheck;
    }
    public AdvancedSettings advanced = new AdvancedSettings();
    private CapsuleCollider capsule;
    private const float jumpRayLength = 0.7f;
    public bool IsGrounded { get; private set; }
    Vector2 inputXY;
    [HideInInspector] public bool isCrouching;
    bool isSprinting = false;

    [HideInInspector] public Rigidbody fps_Rigidbody;
    [Space(15, order = 7)]

    #endregion

    #region Headbobbing Settings
    [Header("Headbobbing Settings", order = 8)]
    [Space(8, order = 9)]

    [Tooltip("Determines Whether to use headbobing or not")] public bool useHeadbob = true;
    [SerializeField] [Tooltip("Parent Of Player Camera")] private Transform head;
    [Tooltip("Overall Speed of Headbob")] [Range(0.1f, 10)] public float headbobFrequency = 1.5f;
    [Tooltip("Headbob Sway Angle")] [Range(0, 10)] public float headbobSwayAngle = 5f;
    [Tooltip("Headbob Height")] [Range(0, 10)] public float headbobHeight = 3f;
    [Tooltip("Headbob Side Movement")] [Range(0, 10)] public float headbobSideMovement = 5f;
    [HideInInspector] public float headbobSpeedMultiplier = 3f;

    [Tooltip("Determines if the headbob system will react to jumping and lading")] public bool useJumdLandMovement = true;
    [Tooltip("Determines how much the head jolts when Jumping")] [Range(0, 10)] public float jumpAngle = 3f;
    [Tooltip("Determines how much the head jolts when landing")] public float landAngle = 60;
    private Vector3 originalLocalPosition;
    private float nextStepTime = 0.5f;
    private float headbobCycle = 0.0f;
    private float headbobFade = 0.0f;
    private float springPosition = 0.0f;
    private float springVelocity = 0.0f;
    private float springElastic = 1.1f;
    private float springDampen = 0.8f;
    private float springVelocityThreshold = 0.05f;
    private float springPositionThreshold = 0.05f;
    Vector3 previousPosition;
    Vector3 previousVelocity = Vector3.zero;
    bool previousGrounded;
    AudioSource audioSource;

    [Space(15, order = 10)]
    #endregion

    #region Audio Settings
    [Header("Audio/SFX Settings", order = 11)]
    [Space(4, order = 12)]

    [SerializeField] [Tooltip("Volume to play the Footsteps with.")] [Range(0, 10)] private float Volume = 5f;
    [Space(4, order = 13)]
    [SerializeField] [Tooltip("The Sound made when jumping. Not Used in Dynamic Foot Steps mode.")] private AudioClip jumpSound;
    [SerializeField] [Tooltip("The Sound made when landing from a jump or a fall. Not Used in Dynamic Foot Steps mode.")] private AudioClip landSound;
    [SerializeField] [Tooltip("Determines Whether to use movement Sounds.")] private bool _useFootStepSounds = false;
    [SerializeField] [Tooltip("Foot step Sounds. Will also act as a fall back for the Dynamic Foot Steps.")] public AudioClip[] footStepSounds;
    public bool ignoreFallDamage = false;
    Vector3 Distance = new Vector3(0,-100,0);

    [System.Serializable]
    public class DynamicFootStep
    {

        //Not Easily changeable at the moment
        [Tooltip("Should the controller use dynamic footsteps? For this to work properly, A physics material must be assigned to both this scipt and the collider you wish give sound fx to. I.e: To use the grass fx, assign a physics material to the 'Grass' slot below, as well as the collider you wish to act as a grassy area")] public bool useDynamicFootStepProcess;
        public PhysicMaterial _Wood;
        public PhysicMaterial _metalAndGlass;
        public PhysicMaterial _Grass;
        public PhysicMaterial _dirtAndGravle;
        public PhysicMaterial _rockAndConcrete;
        public PhysicMaterial _Mud;
        public PhysicMaterial _CustomMaterial;
        internal AudioClip[] qikAC;

        [Tooltip("Audio clips to be played while walking on the Wood physics material")] public AudioClip[] _woodFootsteps;
        [Tooltip("Audio clips to be played while walking on the Metal or Glass physics material")] public AudioClip[] _metalAndGlassFootsteps;
        [Tooltip("Audio clips to be played while walking on the Grass physics material")] public AudioClip[] _GrassFootsteps;
        [Tooltip("Audio clips to be played while walking on the Dirt or Gravelphysics material")] public AudioClip[] _dirtAndGravelFootsteps;
        [Tooltip("Audio clips to be played while walking on the Rock or Concrete physics material")] public AudioClip[] _rockAndConcreteFootsteps;
        [Tooltip("Audio clips to be played while walking on the Mud physics material")] public AudioClip[] _MudFootsteps;
        [Tooltip("Audio clips to be played while walking on the Custom physics material")] public AudioClip[] _CustomMaterialFoorsteps;

    }
    public DynamicFootStep dynamicFootstep = new DynamicFootStep();

    #endregion

    #region BETA Settings

    [System.Serializable]
    public class BETA_SETTINGS
    {

    }

    [Space(15)]
    [Tooltip("Settings in this feild are currently in beta testing and can prove to be unstable.")]
    [Space(5)]
    public BETA_SETTINGS betaSettings = new BETA_SETTINGS();


    #endregion

    #endregion

    SCPEntity me;
    private void Awake()
    {
        #region Look Settings - Awake
        originalRotation = transform.localRotation.eulerAngles;

        #endregion 

        #region Movement Settings - Awake
        walkSpeedInternal = walkSpeed;
        sprintSpeedInternal = sprintSpeed;
        jumpPowerInternal = jumpPower;
        capsule = GetComponent<CapsuleCollider>();
        IsGrounded = true;
        isCrouching = false;
        fps_Rigidbody = GetComponent<Rigidbody>();
        _crouchModifiers.colliderHeight = capsule.height;
        #endregion

        #region Headbobbing Settings - Awake

        #endregion

        #region BETA_SETTINGS - Awake

        #endregion

    }

    private void Start()
    {
        me = GetComponent<SCPEntity>();
        #region Look Settings - Start

        if (autoCrosshair)
        {
            GameObject qui = new GameObject("AutoCrosshair");
            qui.AddComponent<RectTransform>();
            qui.AddComponent<Canvas>();
            qui.AddComponent<CanvasScaler>();
            qui.AddComponent<GraphicRaycaster>();
            qui.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            GameObject quic = new GameObject("Crosshair");
            quic.AddComponent<Image>().sprite = Crosshair;

            qui.transform.SetParent(this.transform);
            qui.transform.position = Vector3.zero;
            quic.transform.SetParent(qui.transform);
            quic.transform.position = Vector3.zero;
        }
        cameraStartingPosition = playerCamera.localPosition;
        if (lockAndHideCursor) { Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false; }
        #endregion

        #region Movement Settings - Start
        staminaInternal = Stamina * 10;

        #endregion

        #region Headbobbing Settings - Start
        originalLocalPosition = head.localPosition;
        if (GetComponent<AudioSource>() == null) { gameObject.AddComponent<AudioSource>(); }
        previousPosition = fps_Rigidbody.position;
        audioSource = GetComponent<AudioSource>();
        #endregion

        #region BETA_SETTINGS - Start
        fOVKick.fovStart = playerCamera.GetComponent<Camera>().fieldOfView;
        #endregion

        //GameInfo.CurrentGame.FirstPerson = this;
    }

    private void Update()
    {
        #region Look Settings - Update

        if (enableCameraMovement)
        {
            float mouseXInput;
            float mouseYInput;
            mouseXInput = Input.GetAxis("Mouse Y");
            mouseYInput = Input.GetAxis("Mouse X");
            if (targetAngles.y > 180) { targetAngles.y -= 360; followAngles.y -= 360; } else if (targetAngles.y < -180) { targetAngles.y += 360; followAngles.y += 360; }
            if (targetAngles.x > 180) { targetAngles.x -= 360; followAngles.x -= 360; } else if (targetAngles.x < -180) { targetAngles.x += 360; followAngles.x += 360; }
            targetAngles.y += mouseYInput * GameInfo.CurrentMouseSen;
            targetAngles.x += mouseXInput * GameInfo.CurrentMouseSen;
            targetAngles.y = Mathf.Clamp(targetAngles.y, -0.5f * Mathf.Infinity, 0.5f * Mathf.Infinity);
            targetAngles.x = Mathf.Clamp(targetAngles.x, -0.5f * rotationRange, 0.5f * rotationRange);
            followAngles = Vector3.SmoothDamp(followAngles, targetAngles, ref followVelocity, cameraSmoothing / 100);
            playerCamera.localRotation = Quaternion.Euler(-followAngles.x + originalRotation.x, 0, 0);
            transform.localRotation = Quaternion.Euler(0, followAngles.y + originalRotation.y, 0);
        }

        #endregion

        #region Movement Settings - Update

        #endregion

        #region Headbobbing Settings - Update

        #endregion

        #region BETA_SETTINGS - Update

        #endregion
    }

    private void FixedUpdate()
    {
        #region Look Settings - FixedUpdate

        #endregion

        #region Movement Settings - FixedUpdate

        bool wasWalking = !isSprinting;
        if (useStamina)
        {
            if (staminaInternal > 0) { if (!isCrouching) { isSprinting = Input.GetKey(KeyCode.LeftShift); } } else { isSprinting = false; }
            if (isSprinting == true && staminaInternal > 0) { staminaInternal -= staminaDepletionSpeed; } else if (staminaInternal < (Stamina * 10) && !Input.GetKey(KeyCode.LeftShift)) { staminaInternal += staminaDepletionSpeed / 1.5f; }
        }
        else { isSprinting = Input.GetKey(KeyCode.LeftShift); }

        advanced.tooSteep = false;
        float inrSprintSpeed;
        inrSprintSpeed = sprintSpeedInternal;
        Vector3 dMove = Vector3.zero;
        speed = walkByDefault ? isCrouching ? walkSpeedInternal : (isSprinting ? inrSprintSpeed : walkSpeedInternal) : (isSprinting ? walkSpeedInternal : inrSprintSpeed);
        Ray ray = new Ray(transform.position, -transform.up);
        if (IsGrounded || fps_Rigidbody.velocity.y < 0.1)
        {
            RaycastHit[] hits = Physics.RaycastAll(ray, capsule.height * jumpRayLength);
            float nearest = float.PositiveInfinity;
            IsGrounded = false;
            for (int i = 0; i < hits.Length; i++)
            {
                if (!hits[i].collider.isTrigger && hits[i].distance < nearest)
                {
                    IsGrounded = true;
                    nearest = hits[i].distance;
                }
            }
        }




        if (advanced.useSlopeDetection)
        {
            if (Physics.Raycast(new Vector3(transform.position.x, transform.position.y - 0.75f, transform.position.z + 0.1f), Vector3.down, out advanced.surfaceAngleCheck, 1f))
            {

                if (Vector3.Angle(advanced.surfaceAngleCheck.normal, Vector3.up) < 89)
                {
                    advanced.tooSteep = false;
                    dMove = transform.forward * inputXY.y * speed + transform.right * inputXY.x * walkSpeedInternal;
                    if (Vector3.Angle(advanced.surfaceAngleCheck.normal, Vector3.up) > advanced.maxSlopeAngle)
                    {
                        advanced.tooSteep = true;
                        isSprinting = false;
                        dMove = new Vector3(0, -4, 0);

                    }
                    else if (Vector3.Angle(advanced.surfaceAngleCheck.normal, Vector3.up) > 44)
                    {
                        advanced.tooSteep = true;
                        isSprinting = false;
                        dMove = (transform.forward * inputXY.y * speed + transform.right * inputXY.x) + new Vector3(0, -4, 0);
                    }
                }
            }

            else if (Physics.Raycast(new Vector3(transform.position.x - 0.086f, transform.position.y - 0.75f, transform.position.z - 0.05f), Vector3.down, out advanced.surfaceAngleCheck, 1f))
            {

                if (Vector3.Angle(advanced.surfaceAngleCheck.normal, Vector3.up) < 89)
                {
                    advanced.tooSteep = false;
                    dMove = transform.forward * inputXY.y * speed + transform.right * inputXY.x * walkSpeedInternal;
                    if (Vector3.Angle(advanced.surfaceAngleCheck.normal, Vector3.up) > 70)
                    {
                        advanced.tooSteep = true;
                        isSprinting = false;
                        dMove = new Vector3(0, -4, 0);

                    }
                    else if (Vector3.Angle(advanced.surfaceAngleCheck.normal, Vector3.up) > 45)
                    {
                        advanced.tooSteep = true;
                        isSprinting = false;
                        dMove = (transform.forward * inputXY.y * speed + transform.right * inputXY.x) + new Vector3(0, -4, 0);

                    }
                }
                else if (Physics.Raycast(new Vector3(transform.position.x + 0.086f, transform.position.y - 0.75f, transform.position.z - 0.05f), Vector3.down, out advanced.surfaceAngleCheck, 1f))
                {

                    if (Vector3.Angle(advanced.surfaceAngleCheck.normal, Vector3.up) < 89)
                    {
                        advanced.tooSteep = false;
                        dMove = transform.forward * inputXY.y * speed + transform.right * inputXY.x * walkSpeedInternal;
                        if (Vector3.Angle(advanced.surfaceAngleCheck.normal, Vector3.up) > 70)
                        {
                            advanced.tooSteep = true;
                            isSprinting = false;
                            dMove = new Vector3(0, -4, 0);

                        }
                        else if (Vector3.Angle(advanced.surfaceAngleCheck.normal, Vector3.up) > 45)
                        {
                            advanced.tooSteep = true;
                            isSprinting = false;
                            dMove = (transform.forward * inputXY.y * speed + transform.right * inputXY.x) + new Vector3(0, -4, 0);
                        }
                    }
                }
            }
            else
            {
                advanced.tooSteep = false;
                dMove = transform.forward * inputXY.y * speed + transform.right * inputXY.x * walkSpeedInternal;
            }
        }
        else
        {
            advanced.tooSteep = false;
            dMove = transform.forward * inputXY.y * speed + transform.right * inputXY.x * walkSpeedInternal;
        }


        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        inputXY = new Vector2(horizontalInput, verticalInput);
        if (inputXY.magnitude > 1) { inputXY.Normalize(); }

        float yv = fps_Rigidbody.velocity.y;
        bool didJump = canHoldJump ? Input.GetButton("Jump") : Input.GetButtonDown("Jump");

        if (!canJump) didJump = false;

        if (IsGrounded && didJump && jumpPowerInternal > 0)
        {
            yv += jumpPowerInternal;
            IsGrounded = false;
            didJump = false;
        }
        if (IsGrounded == false)
        {
            //Count Distance.

            Ray ray2 = new Ray(transform.position, -transform.up); 
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 20f))
            {
                //float tmp = Vector3.Distance(new Vector3(0, hit.transform.position.y,0), new Vector3(0, transform.position.y,0));
                //Debug.Log("Jump:" + Distance.y);
                if (transform.position.y > Distance.y)
                {
                    Distance = transform.position;
                    //Debug.Log("Jump:" + Distance.y);
                }
            }
        }
        else if(Distance.y!=-100)
        {
            if (previousGrounded == false&&IsGrounded==true)
            {
                float tmp = Vector3.Distance(new Vector3(0, Distance.y, 0), new Vector3(0, transform.position.y, 0));

                if (tmp > 5f && Distance.y > transform.position.y&& ignoreFallDamage==false)
                {
                    Debug.Log("Fall:" + ((tmp - 4f) / 4f) * 45f + " From:" + Distance.y);
                    if ((GameInfo.CurrentGame.PlayerHealth.CurrentHealth - (-(tmp - 2f) / 3f) * 45f) < 0)
                    {
                        GameInfo.CurrentGame.DeathText = "You are fallen to death.";
                    }
                    GameInfo.CurrentGame.PlayerHealth.ChangeHealth((-(tmp - 2f) / 3f) * 45f);
                    Distance = new Vector3(0, -150, 0);
                }
            }
        }

        if (playerCanMove)
        {
            fps_Rigidbody.velocity = dMove + Vector3.up * yv;
        }
        else { fps_Rigidbody.velocity = Vector3.zero; }

        if (dMove.magnitude > 0 || !IsGrounded || advanced.tooSteep)
        {
            GetComponent<Collider>().material = advanced.zeroFrictionMaterial;
        }
        else { GetComponent<Collider>().material = advanced.highFrictionMaterial; }

        fps_Rigidbody.AddForce(Physics.gravity * (advanced.gravityMultiplier - 1));
        if (fOVKick.useFOVKick && wasWalking == isSprinting && fps_Rigidbody.velocity.magnitude > 0.1f && !isCrouching)
        {
            StopAllCoroutines();
            StartCoroutine(wasWalking ? FOVKickOut() : FOVKickIn());
        }

        if (_crouchModifiers.useCrouch && _crouchModifiers.CrouchInputAxis != string.Empty)
        {
            isCrouching = _crouchModifiers.crouchOverride ? true : Input.GetAxis(_crouchModifiers.CrouchInputAxis) > 0;

            if (isCrouching)
            {
                capsule.height = Mathf.MoveTowards(capsule.height, _crouchModifiers.colliderHeight / 2, 5 * Time.deltaTime);
                walkSpeedInternal = walkSpeed * _crouchModifiers.crouchWalkSpeedMultiplier;
                sprintSpeedInternal = sprintSpeed * _crouchModifiers.crouchSprintSpeedMultiplier;
                jumpPowerInternal = jumpPower * _crouchModifiers.crouchJumpPowerMultiplier;
            }
            else
            {
                capsule.height = Mathf.MoveTowards(capsule.height, _crouchModifiers.colliderHeight, 5 * Time.deltaTime);
                walkSpeedInternal = walkSpeed;
                sprintSpeedInternal = sprintSpeed;
                jumpPowerInternal = jumpPower;
            }
        }

        #endregion

        #region BETA_SETTINGS - FixedUpdate

        #endregion

        #region Headbobbing Settings - FixedUpdate
        float yPos = 0;
        float xPos = 0;
        float zTilt = 0;
        float xTilt = 0;
        float bobSwayFactor;
        float bobFactor;
        float strideLangthen;
        float flatVel;

        if (useHeadbob == true || dynamicFootstep.useDynamicFootStepProcess == true || _useFootStepSounds == true)
        {
            Vector3 vel = (fps_Rigidbody.position - previousPosition) / Time.deltaTime;
            Vector3 velChange = vel - previousVelocity;
            previousPosition = fps_Rigidbody.position;
            previousVelocity = vel;
            springVelocity -= velChange.y;
            springVelocity -= springPosition * springElastic;
            springVelocity *= springDampen;
            springPosition += springVelocity * Time.deltaTime;
            springPosition = Mathf.Clamp(springPosition, -0.3f, 0.3f);

            if (Mathf.Abs(springVelocity) < springVelocityThreshold && Mathf.Abs(springPosition) < springPositionThreshold) { springPosition = 0; springVelocity = 0; }
            flatVel = new Vector3(vel.x, 0.0f, vel.z).magnitude;
            strideLangthen = 1 + (flatVel * ((headbobFrequency * 2) / 10));
            headbobCycle += (flatVel / strideLangthen) * (Time.deltaTime / headbobFrequency);
            bobFactor = Mathf.Sin(headbobCycle * Mathf.PI * 2);
            bobSwayFactor = Mathf.Sin(Mathf.PI * (2 * headbobCycle + 0.5f));
            bobFactor = 1 - (bobFactor * 0.5f + 1);
            bobFactor *= bobFactor;

            yPos = 0;
            xPos = 0;
            zTilt = 0;
            if (useJumdLandMovement) { xTilt = -springPosition * landAngle; }
            else { xTilt = -springPosition; }

            if (IsGrounded)
            {
                if (new Vector3(vel.x, 0.0f, vel.z).magnitude < 0.1f) { headbobFade = Mathf.MoveTowards(headbobFade, 0.0f, Time.deltaTime); } else { headbobFade = Mathf.MoveTowards(headbobFade, 1.0f, Time.deltaTime); }
                float speedHeightFactor = 1 + (flatVel * (headbobSpeedMultiplier / 10));
                xPos = -(headbobSideMovement / 10) * bobSwayFactor;
                yPos = springPosition * (jumpAngle / 10) + bobFactor * (headbobHeight / 10) * headbobFade * speedHeightFactor;
                zTilt = bobSwayFactor * (headbobSwayAngle / 10) * headbobFade;
            }
        }

        if (useHeadbob == true)
        {
            head.localPosition = originalLocalPosition + new Vector3(xPos, yPos, 0);
            head.localRotation = Quaternion.Euler(xTilt, 0, zTilt);
        }

        if (dynamicFootstep.useDynamicFootStepProcess)
        {
            Vector3 dwn = Vector3.down;
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(transform.position, dwn, out hit))
            {
                dynamicFootstep.qikAC = (hit.collider.sharedMaterial == dynamicFootstep._Wood) ?
                dynamicFootstep._woodFootsteps : ((hit.collider.sharedMaterial == dynamicFootstep._Grass) ?
                dynamicFootstep._GrassFootsteps : ((hit.collider.sharedMaterial == dynamicFootstep._metalAndGlass) ?
                dynamicFootstep._metalAndGlassFootsteps : ((hit.collider.sharedMaterial == dynamicFootstep._rockAndConcrete) ?
                dynamicFootstep._rockAndConcreteFootsteps : ((hit.collider.sharedMaterial == dynamicFootstep._dirtAndGravle) ?
                dynamicFootstep._dirtAndGravelFootsteps : ((hit.collider.sharedMaterial == dynamicFootstep._Mud) ?
                dynamicFootstep._MudFootsteps : ((hit.collider.sharedMaterial == dynamicFootstep._CustomMaterial) ?
                dynamicFootstep._CustomMaterialFoorsteps : footStepSounds))))));

                if (IsGrounded)
                {
                    if (!previousGrounded)
                    {
                        if (_useFootStepSounds) { audioSource.PlayOneShot(dynamicFootstep.qikAC[Random.Range(0, dynamicFootstep.qikAC.Length)], Volume / 10); }
                        nextStepTime = headbobCycle + 0.5f;
                    }
                    else
                    {
                        if (headbobCycle > nextStepTime)
                        {
                            nextStepTime = headbobCycle + 0.5f;
                            if (_useFootStepSounds) { audioSource.PlayOneShot(dynamicFootstep.qikAC[Random.Range(0, dynamicFootstep.qikAC.Length)], Volume / 10); }
                        }
                    }
                    previousGrounded = true;
                }
                else
                {
                    if (previousGrounded)
                    {
                        if (_useFootStepSounds) { audioSource.PlayOneShot(dynamicFootstep.qikAC[Random.Range(0, dynamicFootstep.qikAC.Length)], Volume / 10); }
                    }
                    previousGrounded = false;
                }

            }
            else
            {
                dynamicFootstep.qikAC = footStepSounds;
                if (IsGrounded)
                {
                    if (!previousGrounded)
                    {
                        if (_useFootStepSounds) { audioSource.PlayOneShot(landSound, Volume / 10); }
                        nextStepTime = headbobCycle + 0.5f;
                    }
                    else
                    {
                        if (headbobCycle > nextStepTime)
                        {
                            nextStepTime = headbobCycle + 0.5f;
                            int n = Random.Range(0, footStepSounds.Length);
                            if (_useFootStepSounds) { audioSource.PlayOneShot(footStepSounds[n], Volume / 10); }
                            footStepSounds[n] = footStepSounds[0];
                        }
                    }
                    previousGrounded = true;
                }
                else
                {
                    if (previousGrounded)
                    {
                        if (_useFootStepSounds) { audioSource.PlayOneShot(jumpSound, Volume / 10); }
                    }
                    previousGrounded = false;
                }
            }

        }
        else
        {
            if (IsGrounded)
            {
                if (!previousGrounded)
                {
                    if (_useFootStepSounds) { audioSource.PlayOneShot(landSound, Volume / 10); }
                    nextStepTime = headbobCycle + 0.5f;
                }
                else
                {
                    if (headbobCycle > nextStepTime)
                    {
                        nextStepTime = headbobCycle + 0.5f;
                        int n = Random.Range(0, footStepSounds.Length);
                        if (_useFootStepSounds) { audioSource.PlayOneShot(footStepSounds[n], Volume / 10); }

                    }
                }
                previousGrounded = true;
            }
            else
            {
                if (previousGrounded)
                {
                    if (_useFootStepSounds) { audioSource.PlayOneShot(jumpSound, Volume / 10); }
                }
                previousGrounded = false;
            }
        }


        #endregion

        #region SCPEntity Related
        if (GameInfo.CurrentGame.isUltraState == false&&GameInfo.CurrentGame.isHealthSpeedDisabled==false)
        {
            walkSpeedInternal = 4 - 4 * ((me.MaxHealth - me.CurrentHealth) / me.MaxHealth);
            sprintSpeedInternal = 8 - 8 * ((me.MaxHealth - me.CurrentHealth) / me.MaxHealth);
            headbobSwayAngle = 5 + 10 * ((me.MaxHealth - me.CurrentHealth) / me.MaxHealth);
        }
        else if(GameInfo.CurrentGame.isUltraState==true)
        {
            walkSpeedInternal = 16;
            sprintSpeedInternal = 25;
            headbobSwayAngle = 1;
        }
        #endregion
    }

    public IEnumerator FOVKickOut()
    {
        float t = Mathf.Abs((playerCamera.GetComponent<Camera>().fieldOfView - fOVKick.fovStart) / fOVKick.FOVKickAmount);
        while (t < fOVKick.changeTime)
        {
            playerCamera.GetComponent<Camera>().fieldOfView = fOVKick.fovStart + (fOVKick.KickCurve.Evaluate(t / fOVKick.changeTime) * fOVKick.FOVKickAmount);
            t += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }

    public IEnumerator FOVKickIn()
    {
        float t = Mathf.Abs((playerCamera.GetComponent<Camera>().fieldOfView - fOVKick.fovStart) / fOVKick.FOVKickAmount);
        while (t > 0)
        {
            playerCamera.GetComponent<Camera>().fieldOfView = fOVKick.fovStart + (fOVKick.KickCurve.Evaluate(t / fOVKick.changeTime) * fOVKick.FOVKickAmount);
            t -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        playerCamera.GetComponent<Camera>().fieldOfView = fOVKick.fovStart;
    }

    public IEnumerator CameraShake(float Duration, float Magnitude)
    {
        float elapsed = 0;
        //while (elapsed < Duration && enableCameraShake)
        //{
        //    playerCamera.transform.localPosition = Vector3.MoveTowards(playerCamera.transform.localPosition, new Vector3(cameraStartingPosition.x + Random.Range(-1, 1) * Magnitude, cameraStartingPosition.y + Random.Range(-1, 1) * Magnitude, cameraStartingPosition.z), Magnitude * 2);
        //    yield return new WaitForSecondsRealtime(0.001f);
        //    elapsed += Time.deltaTime;
        //    yield return null;
        //}
        while (elapsed < Duration)
        {

            yield return null;
        }
        playerCamera.transform.localPosition = cameraStartingPosition;
    }
}




