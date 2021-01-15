using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;
using UnityEngine.UI;

namespace Site13Kernel
{

    public class SCPFirstController : MonoBehaviour
    {
        #region Stamina
        public float MaxStamina = 100;
        public float StaminaConsumeSpeed = 10;
        public float StaminaRecoverSpeed = 10;
        public float CurrentStamina = 100;
        #endregion
        public string BagNameAlias;
        public float WalkSpeed, RunSpeed = 0.3f;
        public AudioSource source;
        public AudioClip[] footStepSounds;
        public int footIndex;
        CharacterController controller;
        Transform thisTransform, cameraTransform;
        float rotation;
        float baseHeadHeight = 0.3f;
        public float OriginHeadHeight = 0.8f;
        public float RunningFovDelta = 10;
        float walkCycle = 0.0f;
        bool Crouch = false;
        bool jump = false;
        bool prevGrounded;
        public bool isWalkLogicEnabled = true;
        public bool isRotationLogicEnabled = true;
        public bool ForceMouseCapture = false;
        public bool OverrideMouseSensity = false;
        public float OverridenMouseSen = 5;
        public Vector3 ViewportShakingIntensity = new Vector3(3, 0, 0);
        public RotationRestrictionSettings RotationRestriction;
        public Camera WeaponCam;
        public bool DisableJump = false;
        #region ImprovedFalldown;
        private bool isImprovedFalldownEnabled;
        [Tooltip("Effect when old falldown enabled.")]
        public float ConstantFalldownSpeed = -9.8f;
        public bool isForceOldFalldown;
        public float FalldownSpeed = 3f;
        [Tooltip("Smaller means falldown speed is slower.")]
        public float FalldownSpeedAccelerator = 6f;
        [Tooltip("Smaller holds longer.(Original is 5 in code.)")]
        public float JumpAirStickTime = 4f;
        private float AccumulativeFalldownSpeed;
        public float JumpNormal = 4.5f;
        public float JumpRun = 6f;
        public float JumpCrouch = 8f;
        #endregion

        #region Dynamic Crosshair
        public Image Crosshair;
        public bool ColorizedCross;
        public Sprite DefaultCrosshair;
        #endregion
        [Serializable]
        public class RotationRestrictionSettings
        {
            public bool isEnabled;
            public float MaxY;
            public float MinY;
            public float MaxZ;
            public float MinZ;
            public float MaxX;
            public float MinX;
        }
        float jumpCutdown = 0.0f;
        // Start is called before the first frame update
        void Start()
        {
            isDisabled = false;
            GameInfo.CurrentGame.FirstPerson = this;
            thisTransform = transform;
            cameraTransform = transform.GetChild(0).transform;
            controller = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            baseHeadHeight = OriginHeadHeight;
            if (ForceMouseCapture)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            try
            {
                isImprovedFalldownEnabled = bool.Parse(Site_13ToolLib.Data.Registry.RegistryIO.Site13Settings.Read("HKEY_LOCAL_MACHINE/System/FPSSystem/isImprovedFalldownEnabled"));
            }
            catch (Exception)
            {
                isImprovedFalldownEnabled = true;
            }
            if (isForceOldFalldown == true)
                isImprovedFalldownEnabled = false;
        }
        bool isDisabled = false;
        void Update()
        {
            if (GameInfo.CurrentGame.isPaused == false && isDisabled == false)
                ControlLogic();
        }
        private void OnDisable()
        {
            isDisabled = true;
        }
        private void OnEnable()
        {
            isDisabled = false;
        }
        private void Awake()
        {

            isDisabled = false;
        }
        float SHK_TIME_LEFT = 0;
        float SHK_HOR = 0;//Larger than 0: go right.
        float SHK_VRT = 0;//Larger then 0: go up.
        void SingleCycleShakeMovement(ref float x, ref float y)
        {
            if (SHK_TIME_LEFT > 0)
            {
                var DLT_HOR = SHK_HOR / SHK_TIME_LEFT;
                DLT_HOR *= Time.deltaTime;
                var DLT_VRT = SHK_VRT / SHK_TIME_LEFT;
                DLT_VRT *= Time.deltaTime;
                x += DLT_HOR;
                y += DLT_VRT;
                SHK_TIME_LEFT -= Time.deltaTime;
                SHK_HOR -= DLT_HOR;
                SHK_VRT -= DLT_VRT;
            }
        }
        public void ShakeHead(float Horizontal, float Vertical, float Duration)
        {
            SHK_HOR += Horizontal;
            SHK_VRT += Vertical;
            SHK_TIME_LEFT += Duration;
        }
        public void ShakeHeadRandomly(float HorizontalMax, float HorizontalMin, float VerticalMax, float VerticalMin, float Duration)
        {
            SHK_HOR += UnityEngine.Random.Range(HorizontalMin, HorizontalMax);
            SHK_VRT += UnityEngine.Random.Range(VerticalMin, VerticalMax);
            SHK_TIME_LEFT = Duration;
        }

        void ControlLogic()
        {
            if (Input.GetButtonDown("Crouch"))
            {
                Crouch = !Crouch;
                if (controller.height == 1.8f)
                {
                    GetComponent<CapsuleCollider>().center = new Vector3(0, -.6f, 0);
                    controller.center = new Vector3(0, -.6f, 0);
                    GetComponent<CapsuleCollider>().height = 0.6f;
                    controller.height = 0.6f;
                }
                else
                {
                    GetComponent<CapsuleCollider>().center = new Vector3(0, 0, 0);
                    GetComponent<CapsuleCollider>().height = 1.8f;
                    controller.center = new Vector3(0, 0, 0);
                    controller.height = 1.8f;
                }
            }
            if (DisableJump == false)
                if (Input.GetButtonDown("Jump"))
                {
                    if (prevGrounded)
                    {

                        jump = true;

                    }
                }
            if (Crouch == true)
            {
                //if (baseHeadHeight > (OriginHeadHeight / 20) * 1)
                //{
                //    baseHeadHeight -= Time.deltaTime * 0.7f;
                //    if (baseHeadHeight <= (OriginHeadHeight / 20) * 1)
                //    {
                //        baseHeadHeight = (OriginHeadHeight / 20) * 1;
                //    }
                //}
                float NF = -(OriginHeadHeight / 5f);
                if (baseHeadHeight > NF)
                {
                    baseHeadHeight -= Time.deltaTime * 1.2f;
                    if (baseHeadHeight <= NF)
                    {
                        baseHeadHeight = NF;
                    }
                }
            }
            else
            {
                if (baseHeadHeight < OriginHeadHeight)
                {
                    baseHeadHeight += Time.deltaTime * 3;
                    if (baseHeadHeight >= OriginHeadHeight)
                    {
                        baseHeadHeight = OriginHeadHeight;
                    }
                }
            }
            if (isWalkLogicEnabled)
            {
                ////////////////////////////////
                //Viewport Shaking when moving//
                ////////////////////////////////

                var lp = cameraTransform.localPosition;

                if (Crouch == false)
                    lp.y = baseHeadHeight - ((float)Math.Pow(walkCycle - 0.5, 2) - 1f) / 3f;
                else
                    //lp.y = baseHeadHeight - ((float)Math.Pow(walkCycle - 0.5, 2) - .5f) / 8f;
                    lp.y = baseHeadHeight - ((float)Math.Pow(walkCycle - 0.5, 2) - .5f) / 6f;
                cameraTransform.localPosition = lp;
                var v3Angle = WeaponCam.transform.localRotation.eulerAngles;
                float ShakeValue0 = (Math.Abs(walkCycle - 0.5f) - 0.25f) * 4 * (GameInfo.CurrentGame.isRunning ? 1.2f : 1);
                v3Angle.x = ShakeValue0 * ViewportShakingIntensity.x;
                v3Angle.y = ShakeValue0 * ViewportShakingIntensity.y;
                v3Angle.z = ShakeValue0 * ViewportShakingIntensity.z;
                //v3Angle.y = -((float)Math.Pow(walkCycle - 0.5, 2) - 1f)*2;
                WeaponCam.transform.localRotation = Quaternion.Euler(v3Angle);
                if (walkCycle >= 1.0f)
                {
                    if (footIndex > footStepSounds.Length)
                    {
                        footIndex = 0;
                    }
                    walkCycle = 0;
                    source.clip = footStepSounds[footIndex];
                    if (controller.isGrounded)
                        source.Play();
                    footIndex++;
                    if (footIndex >= footStepSounds.Length)
                    {
                        footIndex = 0;
                    }
                }
            }

            if (GameInfo.CurrentGame.isAiming == false)
            {

                if (Input.GetButton("Run"))
                {
                    //GameInfo.
                    GameInfo.CurrentGame.isRunning = true;
                    GameInfo.CurrentGame.UsingFOV = GameInfo.CurrentGame.DefaultFOV - RunningFovDelta;
                    if (Camera.main.fieldOfView > GameInfo.CurrentGame.UsingFOV)
                    {
                        Camera.main.fieldOfView -= Time.deltaTime * (GameInfo.CurrentGame.DefaultFOV - GameInfo.CurrentGame.UsingFOV) / (1f / .4f);
                    }
                    else
                        Camera.main.fieldOfView = GameInfo.CurrentGame.UsingFOV;
                }
                else
                {
                    GameInfo.CurrentGame.isRunning = false;
                    GameInfo.CurrentGame.UsingFOV = GameInfo.CurrentGame.DefaultFOV;
                    if (Camera.main.fieldOfView < GameInfo.CurrentGame.UsingFOV)
                    {
                        Camera.main.fieldOfView += Time.deltaTime * 10;
                    }
                    else
                    {

                        Camera.main.fieldOfView = GameInfo.CurrentGame.UsingFOV;
                    }
                }
            }
            else
            {

            }
            float mx = Input.GetAxis("Mouse X");
            if (Math.Abs(mx) < 0.1)
                mx = 0;
            float my = Input.GetAxis("Mouse Y");
            if (Math.Abs(my) < 0.1)
                my = 0;
            if (ColorizedCross)
                ColorizedColor();
            if (isWalkLogicEnabled)
            {
                SingleCycleShakeMovement(ref mx, ref my);
                PlayerMovement((GameInfo.CurrentGame.isRunning ? Input.GetAxis("Horizontal") : Input.GetAxis("Horizontal") / 2), (GameInfo.CurrentGame.isRunning ? Input.GetAxis("Vertical") : Input.GetAxis("Vertical") / 2));
            }
            if (isRotationLogicEnabled)
                PlayerRotation(mx, my);

        }
        //bool Running = false;
        public void ColorizedColor()
        {

            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0F));
            //Debug.DrawRay(ray.origin, ray.direction, Color.green, 10f);
            RaycastHit hit;
            Color c = new Color(1, 1, 1);

            if (Physics.Raycast(ray, out hit, 100f, 1 << 0 | 1 << 13, QueryTriggerInteraction.Ignore))
            {

                var e = hit.collider.gameObject.GetComponent<SCPEntity>();
                if (e != null)
                {
                    GameInfo.CurrentGame.isAimedEntity = true;
                    switch (e.EntiryGroup)
                    {
                        case EntiryGroup.SCP:
                            c = new Color(1, 0, 0);
                            break;
                        case EntiryGroup.Human_GOC:
                            c = new Color(0, 1, 0);
                            break;
                        case EntiryGroup.Human_Foundation:
                            c = new Color(0.1f, 0.5f, 1);
                            break;
                        case EntiryGroup.Generic:
                            c = new Color(1, 1, 1);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    GameInfo.CurrentGame.isAimedEntity = false;
                }
            }
            else
            {
                GameInfo.CurrentGame.isAimedEntity = false;
            }
            Crosshair.color = c;
        }
        public float RetireRealAngle(float eulerAngle)
        {
            if (eulerAngle > 0)
            {
                if (eulerAngle > 180)
                {
                    return eulerAngle - 360;
                }

                return eulerAngle;
            }
            else
            {
                if (eulerAngle < -180)
                {
                    return -360 - eulerAngle;
                }
                return eulerAngle;
            }
        }
        public void PlayerRotation(float horizontal, float vertical)
        {
            if (RotationRestriction.isEnabled)
            {
                float realX = RetireRealAngle(cameraTransform.localRotation.eulerAngles.x);
                if (realX > RotationRestriction.MaxX)
                {
                    if (vertical < 0)
                    {
                        return;
                    }
                }
                else if (realX < RotationRestriction.MinX)
                {
                    if (vertical > 0)
                    {
                        return;
                    }
                }
                float realY = RetireRealAngle(thisTransform.localRotation.eulerAngles.y);
                if (realY > RotationRestriction.MaxY)
                {
                    if (horizontal > 0)
                    {
                        return;
                    }
                }
                else if (realY < RotationRestriction.MinY)
                {
                    if (horizontal < 0)
                    {
                        return;
                    }
                }

            }
            thisTransform.Rotate(0f, horizontal * (OverrideMouseSensity ? OverridenMouseSen : GameInfo.CurrentMouseSen), 0f);
            rotation += vertical * (OverrideMouseSensity ? OverridenMouseSen : GameInfo.CurrentMouseSen);
            rotation = Mathf.Clamp(rotation, -75f, 80f);
            cameraTransform.localEulerAngles = new Vector3(-rotation, cameraTransform.localEulerAngles.y, 0f);
        }
        float MAX_Y = -120;
        //float MAX_Z = -120;
        public float RetireRealFalldown()
        {
            AccumulativeFalldownSpeed -= FalldownSpeedAccelerator * Time.deltaTime;
            return AccumulativeFalldownSpeed;
        }
        private void PlayerMovement(float horizontal, float vertical)
        {
            bool grounded = controller.isGrounded;
            Vector3 moveDirection;
            if (CurrentStamina > 0)
            {

            }
            else
            {

                if (horizontal != 0)
                    horizontal = horizontal < 0 ? -.5f : .5f;
                if (vertical != 0)
                    vertical = vertical < 0 ? -.5f : .5f;

            }
            moveDirection = thisTransform.forward * (vertical == 0 ? 0 : (vertical > 0f ? (vertical <= 0.8f ? WalkSpeed : RunSpeed) : (vertical >= -0.8f ? -WalkSpeed : -RunSpeed)));
            moveDirection += thisTransform.right * (horizontal == 0 ? 0 : (horizontal > 0f ? (horizontal <= 0.8f ? WalkSpeed : RunSpeed) : (horizontal >= -0.8f ? -WalkSpeed : -RunSpeed)));

            if (Crouch)
            {
                moveDirection /= 2;
            }
            else
            {

            }
            if (vertical != 0 | horizontal != 0)
            {
                walkCycle += moveDirection.magnitude / RunSpeed * Time.deltaTime * 3;
                //((float)Math.Sqrt((horizontal * horizontal + vertical * vertical))) * Time.deltaTime * 5 * (Crouch ? 0.5f : 1);
            }
            //Debug.Log($"H:{horizontal},V:{vertical}");
            moveDirection *= 6f;
            moveDirection *= (GameInfo.CurrentGame.PlayerHealth.CurrentHealth / GameInfo.CurrentGame.PlayerHealth.MaxHealth);
            if (jumpCutdown <= 0)
            {
                if (isImprovedFalldownEnabled == false)
                {
                    moveDirection.y = ConstantFalldownSpeed;
                }
                else
                {
                    moveDirection.y = RetireRealFalldown() * FalldownSpeed;
                }
            }
            else
            {

                if (Crouch)
                {

                    moveDirection.y = JumpCrouch * jumpCutdown;
                }
                else
                if (GameInfo.CurrentGame.isRunning)
                {

                    moveDirection.y = JumpRun * jumpCutdown;

                }
                else
                {

                    moveDirection.y = JumpNormal * jumpCutdown;

                }
                jumpCutdown -= Time.deltaTime * JumpAirStickTime;
            }
            //Debug.Log(Vector3.Magnitude(moveDirection) + "");

            if (Math.Abs(vertical) > 0.8f | Math.Abs(horizontal) > 0.8f)
            {
                if (CurrentStamina > -.1f)
                    CurrentStamina -= Time.deltaTime * StaminaConsumeSpeed;
            }
            else
            {
                if (CurrentStamina < MaxStamina)
                    CurrentStamina += Time.deltaTime * StaminaRecoverSpeed;
            }
            if (jump)
            {
                //AccumulativeFalldownSpeed = 1f;
                jumpCutdown = 1f;
                jump = false;
            }

            if (!grounded)
            {
                if (transform.position.y > MAX_Y)
                {
                    MAX_Y = transform.position.y;
                    //Debug.Log("Up to: " + MAX_Y);
                }
            }

            controller.Move(moveDirection * Time.deltaTime);

            if (!prevGrounded && grounded)
            {

                source.Play();
                AccumulativeFalldownSpeed = 0.0f;
                moveDirection.y = 0f;
                float DLT_Y = MAX_Y - transform.position.y;
                float FALL_DMG = DLT_Y < 4f ? 0f : ((DLT_Y - 3) / 10f) * GameInfo.CurrentGame.PlayerHealth.MaxHealth;
                //Debug.Log("Fall From: " + MAX_Y + $" , Delta Height:{DLT_Y} , Suffer damage:" + FALL_DMG);
                GameInfo.CurrentGame.PlayerHealth.ChangeHealth(-FALL_DMG);
                MAX_Y = -120;
            }
            prevGrounded = grounded;
        }
    }

}