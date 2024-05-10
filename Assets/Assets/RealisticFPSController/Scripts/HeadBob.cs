using System.Collections;
using UnityEngine;

namespace EvolveGames
{
    [RequireComponent(typeof(Camera))]
    public class HeadBob : MonoBehaviour
    {
        [Header("HeadBob Effect")]
        [SerializeField] private bool Enabled = true;
        [Space, Header("Main")]
        [SerializeField, Range(0.001f, 0.01f)] private float Amount = 0.00484f;
        [SerializeField, Range(10f, 30f)] private float Frequency = 16.0f;
        [SerializeField, Range(100f, 10f)] private float Smooth = 44.7f;
        [Header("RoationMovement")]
        [SerializeField] private bool EnabledRoationMovement = true;
        [SerializeField, Range(1f, 10f)] private float RoationMovementAmount = 3.0f;
        [SerializeField] private Camera playerCamera;

        private float ToggleSpeed = 3.0f;
        private Vector3 StartPos;
        private Vector3 StartRot;
        private Vector3 FinalRot;
        private CharacterController characterController;


        private void Awake()
        {
            characterController = GetComponentInParent<CharacterController>();
            StartPos = transform.localPosition;
            StartRot = transform.localRotation.eulerAngles;
        }

        private void Update()
        {
            if (!Enabled) return;
            CheckMotion();
            ResetPos();
            if (EnabledRoationMovement) transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(FinalRot), 1 * Time.deltaTime);
        }

        private void CheckMotion()
        {
            float speed = new Vector3(characterController.velocity.x, 0, characterController.velocity.z).magnitude;
            if (speed < ToggleSpeed) return;
            if (!characterController.isGrounded) return;
            PlayMotion(HeadBobMotion());
        }

        private void PlayMotion(Vector3 Movement)
        {
            transform.localPosition += Movement;
            FinalRot += new Vector3(-Movement.x, -Movement.y, Movement.x) * RoationMovementAmount;
        }
        private Vector3 HeadBobMotion()
        {
            Vector3 pos = Vector3.zero;
            pos.y += Mathf.Lerp(pos.y, Mathf.Sin(Time.time * Frequency) * Amount * 1.4f, Smooth * Time.deltaTime);
            pos.x += Mathf.Lerp(pos.x, Mathf.Cos(Time.time * Frequency / 2f) * Amount * 1.6f, Smooth * Time.deltaTime);
            return pos;
        }

        private void ResetPos()
        {
            if (transform.localPosition == StartPos) return;
            transform.localPosition = Vector3.Lerp(transform.localPosition, StartPos, 1 * Time.deltaTime);
            FinalRot = Vector3.Lerp(FinalRot, StartRot, 1 * Time.deltaTime);
        }

        private IEnumerator BobPunching(Vector3 direction, float duration)
        {
            float timer = duration;
            while (timer > 0)
            {
                Vector3 directionInFrame = direction * Time.deltaTime;
                FinalRot += directionInFrame;
                timer -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            timer = duration;
            while (timer > 0)
            {
                Vector3 directionInFrame = direction * Time.deltaTime;
                FinalRot -= directionInFrame;
                timer -= Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
        }

        public void ShowHeadPunch(Vector3 direction, float duration)
        {
            StartCoroutine(BobPunching(direction, duration));
        }
    }
}