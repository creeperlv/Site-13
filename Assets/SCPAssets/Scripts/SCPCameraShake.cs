using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Site13Kernel
{
    public class SCPCameraShake : MonoBehaviour
    {
        public bool vibrateOnAwake = true;                  // Should this GameObject vibrate on Awake()?

        public Vector3 startingShakeDistance;               // The distance the GameObject will shake
        public Quaternion startingRotationAmount;           // The amount the GameObject will rotate by
        public float shakeSpeed = 60.0f;                    // How fast the GameObject will shake
        public float decreaseMultiplier = 0.5f;             // How fast the shake distance will diminish
        public int numberOfShakes = 8;                      // The number of times this object will shake
        public bool shakeContinuous = false;                // Will this GameObject shake continously, instead of just once?

        private Vector3 actualStartingShakeDistance;        // The shake distance actaully used.  This value may change
        private Quaternion actualStartingRotationAmount;    // The rotation amount actually used.  this value may change.
        private float actualShakeSpeed;                     // The shake speed actually used. This value may change
        private float actualDecreaseMultiplier;             // The decrease multiplier actually used.  This value may change
        private int actualNumberOfShakes;                   // The number of shakes actually used.  This value may change

        private Vector3 originalPosition;                   // Keep track of the position this GameObject was at before shaking (used for resetting when the vibration is over)
        private Quaternion originalRotation;                // Keep track of the rotation this GameObject was at before shaking (used for resetting when the vibration is over)
        private void Start()
        {
            GameInfo.CurrentGame.cameraShake = this;
        }
        void Awake()
        {
            // Initialize the original position to wherever the GameObject is at on Awake
            originalPosition = transform.localPosition;
            originalRotation = transform.localRotation;

            if (vibrateOnAwake)
            {
                StartShaking();
            }
        }

        // This function will cause the GameObject to start shaking with its own default values
        public void StartShaking()
        {
            actualStartingShakeDistance = startingShakeDistance;
            actualStartingRotationAmount = startingRotationAmount;
            actualShakeSpeed = shakeSpeed;
            actualDecreaseMultiplier = decreaseMultiplier;
            actualNumberOfShakes = numberOfShakes;
            StopShaking();
            StartCoroutine("Shake");
        }

        // This function will cause the GameObject to start shaking with the values passed to it
        public void StartShaking(Vector3 shakeDistance, Quaternion rotationAmount, float speed, float diminish, int numOfShakes)
        {
            actualStartingShakeDistance = shakeDistance;
            actualStartingRotationAmount = rotationAmount;
            actualShakeSpeed = speed;
            actualDecreaseMultiplier = diminish;
            actualNumberOfShakes = numOfShakes;
            StopShaking();
            StartCoroutine("Shake");
        }

        // This function will cause the GameObject to start shaking with random values
        public void StartShakingRandom(float minDistance, float maxDistance, float minRotationAmount, float maxRotationAmount)
        {
            actualStartingShakeDistance = new Vector3(Random.Range(minDistance, maxDistance), Random.Range(minDistance, maxDistance), Random.Range(minDistance, maxDistance));
            actualStartingRotationAmount = new Quaternion(Random.Range(minRotationAmount, maxRotationAmount), Random.Range(minRotationAmount, maxRotationAmount), Random.Range(minRotationAmount, maxRotationAmount), 1);
            actualShakeSpeed = shakeSpeed * Random.Range(0.8f, 1.2f);
            actualDecreaseMultiplier = decreaseMultiplier * Random.Range(0.8f, 1.2f);
            actualNumberOfShakes = numberOfShakes + Random.Range(-2, 2);
            StopShaking();
            StartCoroutine("Shake");
        }

        public void StopShaking()
        {
            // Stop the shake coroutine if its running
            StopCoroutine("Shake");

            // Reset the position of the GameObject to its original position
            transform.localPosition = originalPosition;
            transform.localRotation = originalRotation;
        }


        private IEnumerator Shake()
        {
            originalPosition = transform.localPosition;
            originalRotation = transform.localRotation;

            float hitTime = Time.time;
            float shake = actualNumberOfShakes;

            float shakeDistanceX = actualStartingShakeDistance.x;
            float shakeDistanceY = actualStartingShakeDistance.y;
            float shakeDistanceZ = actualStartingShakeDistance.z;

            float shakeRotationX = actualStartingRotationAmount.x;
            float shakeRotationY = actualStartingRotationAmount.y;
            float shakeRotationZ = actualStartingRotationAmount.z;

            // Shake the number of times specified in actualNumberOfShakes
            while (shake > 0 || shakeContinuous)
            {
                float timer = (Time.time - hitTime) * actualShakeSpeed;
                float x = originalPosition.x + Mathf.Sin(timer) * shakeDistanceX;
                float y = originalPosition.y + Mathf.Sin(timer) * shakeDistanceY;
                float z = originalPosition.z + Mathf.Sin(timer) * shakeDistanceZ;

                float xr = originalRotation.x + Mathf.Sin(timer) * shakeRotationX;
                float yr = originalRotation.y + Mathf.Sin(timer) * shakeRotationY;
                float zr = originalRotation.z + Mathf.Sin(timer) * shakeRotationZ;

                transform.localPosition = new Vector3(x, y, z);
                transform.localRotation = new Quaternion(xr, yr, zr, 1);

                if (timer > Mathf.PI * 2)
                {
                    hitTime = Time.time;
                    shakeDistanceX *= actualDecreaseMultiplier;
                    shakeDistanceY *= actualDecreaseMultiplier;
                    shakeDistanceZ *= actualDecreaseMultiplier;

                    shakeRotationX *= actualDecreaseMultiplier;
                    shakeRotationY *= actualDecreaseMultiplier;
                    shakeRotationZ *= actualDecreaseMultiplier;

                    shake--;
                }
                yield return true;
            }

            // Reset the position of the GameObject to its original position
            transform.localPosition = originalPosition;
            transform.localRotation = originalRotation;
        }
    }

}