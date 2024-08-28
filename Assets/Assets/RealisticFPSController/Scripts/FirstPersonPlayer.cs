using Adventurer;
using GenshinImpactMovementSystem;
using System;
using UnityEngine;
using Zenject;

namespace EvolveGames
{
    public class FirstPersonPlayer : MonoBehaviour, IHandAnimatable
    {
        [Header("Parameters")]
        [SerializeField] private CharacterData characterData;

        [Header("PlayerController")]
        [SerializeField] private Transform characterCamera;
        [SerializeField] private Animator animator;
        [SerializeField] private Camera handsCamera;

        private PlayerInput input;
        private bool canMove = true;
        private bool isClimbing = false;
        private bool isCrough = false;
        private float InstallCroughHeight;
        private Vector3 moveDirection = Vector3.zero;
        private Vector3 InstallCameraMovement;
        private float rotationX = 0;
        private bool canRun = false;
        private bool isRunning = false;
        private float InstallFOV;
        private Camera cam;
        private bool Moving;
        private float vertical;
        private float horizontal;
        private float Lookvertical;
        private float Lookhorizontal;
        private float RunningValue;
        private float installGravity;
        private bool WallDistance;
        private float WalkingValue;

        public Action<bool> ItemHide;
        public float Vertical => vertical;
        public float Horizontal => horizontal;
        public float CroughtSpeed => characterData.CroughSpeed;
        public bool IsControllingItem => isRunning || WallDistance;

        [Inject]
        private void Construct(PlayerInput playerInput) => input = playerInput;

        private void Awake()
        {
            cam = GetComponentInChildren<Camera>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            InstallCameraMovement = characterCamera.localPosition;
            InstallFOV = cam.fieldOfView;
        }

        public Animator GetHandsAnimator() => animator;

        void Update()
        {
            if (Cursor.lockState == CursorLockMode.Locked && canMove)
            {
                Lookvertical = -Input.GetAxis("Mouse Y");
                Lookhorizontal = Input.GetAxis("Mouse X");

                rotationX += Lookvertical * characterData.LookSpeed;
                rotationX = Mathf.Clamp(rotationX, -characterData.LookXLimit, characterData.LookXLimit);
                characterCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Lookhorizontal * characterData.LookSpeed, 0);

                if (isRunning && Moving)
                    cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, characterData.RunningFOV, characterData.SpeedToFOV * Time.deltaTime);
                else
                    cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, InstallFOV, characterData.SpeedToFOV * Time.deltaTime);
            }

            //if (!Physics.Raycast(GetComponentInChildren<Camera>().transform.position, transform.TransformDirection(Vector3.up), out CroughCheck, 0.8f, 1))
            //{
            //    if (characterController.height != InstallCroughHeight)
            //    {
            //        isCrough = false;
            //        float Height = Mathf.Lerp(characterController.height, InstallCroughHeight, 6 * Time.deltaTime);
            //        characterController.height = Height;
            //        WalkingValue = Mathf.Lerp(WalkingValue, characterData.WalkingSpeed, 4 * Time.deltaTime);
            //    }
            //}

            //if(WallDistance != Physics.Raycast(
            //    GetComponentInChildren<Camera>().transform.position, 
            //    transform.TransformDirection(Vector3.forward), out ObjectCheck, characterData.HideDistance, characterData.LayerMaskInt) && 
            //    characterData.CanHideDistanceWall)
            //{
            //    WallDistance = Physics.Raycast(GetComponentInChildren<Camera>().transform.position, transform.TransformDirection(Vector3.forward), out ObjectCheck, characterData.HideDistance, characterData.LayerMaskInt);
            //    ItemHide?.Invoke(WallDistance);
            //}
        }

        public void EnterPause() => canMove = false;
        public void ExitPause() => canMove = true;

        private void OnTriggerEnter(Collider other)
        {
            //if (other.tag == "Ladder" && characterData.CanClimbing)
            //{
            //    canRun = false;
            //    isClimbing = true;
            //    WalkingValue /= 2;
            //    ItemHide?.Invoke(true);
            //}
        }

        private void OnTriggerStay(Collider other)
        {
            //if (other.tag == "Ladder" && characterData.CanClimbing)
            //{
            //    moveDirection = new Vector3(0, Input.GetAxis("Vertical") * characterData.Speed * (-characterCamera.localRotation.x / 1.7f), 0);
            //}
        }

        private void OnTriggerExit(Collider other)
        {
            //if (other.tag == "Ladder" && characterData.CanClimbing)
            //{
            //    canRun = true;
            //    isClimbing = false;
            //    WalkingValue *= 2;
            //    ItemHide?.Invoke(false);
            //}
        }

        private void OnValidate()
        {
            //if (animator == null)
            //    throw new NullReferenceException("Player has to have animator assigned!");
        }

        public void ChangeToThirdPersonView()
        {
            //cam.gameObject.SetActive(true);
            //handsCamera.gameObject.SetActive(true);
            //this.enabled = true;
            //Debug.Log($"{nameof(FirstPersonPlayer)} is activated");
        }

        public void ChangeToFirstPersonView()
        {
            //cam.gameObject.SetActive(false);
            //handsCamera.gameObject.SetActive(false);
            //this.enabled = false;
            //Debug.Log($"{nameof(FirstPersonPlayer)} is deactivated");
        }
    }
}