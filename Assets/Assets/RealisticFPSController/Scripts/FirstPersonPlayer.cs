using Adventurer;
using System;
using UnityEngine;
using Zenject;

namespace EvolveGames
{
    [RequireComponent(typeof(CharacterController))]
    public class FirstPersonPlayer : MonoBehaviour, IPlayerView, IHandAnimatable
    {
        [Header("Parameters")]
        [SerializeField] private CharacterData characterData;

        [Header("PlayerController")]
        [SerializeField] private Transform characterCamera;
        [SerializeField] private Animator animator;
        
        private CharacterController characterController;
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
        private bool isActive = false;
        private float WalkingValue;

        public Action<bool> ItemHide;
        public float Vertical => vertical;
        public float Horizontal => horizontal;
        public float yVelocity => characterController.velocity.y;
        public float CroughtSpeed => characterData.CroughSpeed;
        public bool IsControllingItem => isRunning || WallDistance;
        public bool IsActive => isActive;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            cam = GetComponentInChildren<Camera>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            InstallCroughHeight = characterController.height;
            InstallCameraMovement = characterCamera.localPosition;
            InstallFOV = cam.fieldOfView;
            RunningValue = characterData.RunningSpeed;
            installGravity = characterData.Gravity;
            WalkingValue = characterData.WalkingSpeed;
            canRun = characterData.CanRun;
        }

        public Animator GetHandsAnimator() => animator;

        void Update()
        {
            RaycastHit CroughCheck;
            RaycastHit ObjectCheck;

            if (!characterController.isGrounded && !isClimbing)
            {
                moveDirection.y -= characterData.Gravity * Time.deltaTime;
            }

            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);
            isRunning = !isCrough ? canRun ? Input.GetKey(KeyCode.LeftShift) : false : false;
            vertical = canMove ? (isRunning ? RunningValue : WalkingValue) * Input.GetAxis("Vertical") : 0;
            horizontal = canMove ? (isRunning ? RunningValue : WalkingValue) * Input.GetAxis("Horizontal") : 0;

            if (isRunning) 
                RunningValue = Mathf.Lerp(RunningValue, characterData.RunningSpeed, characterData.TimeToRunning * Time.deltaTime);
            else 
                RunningValue = WalkingValue;

            float movementDirectionY = moveDirection.y;
            moveDirection = (forward * vertical) + (right * horizontal);

            if (Input.GetButton("Jump") && canMove && characterController.isGrounded && !isClimbing)
            {
                moveDirection.y = characterData.JumpSpeed;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            characterController.Move(moveDirection * Time.deltaTime);
            Moving = horizontal < 0 || vertical < 0 || horizontal > 0 || vertical > 0 ? true : false;

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

            if (Input.GetKey(characterData.CroughKey))
            {
                isCrough = true;
                float Height = Mathf.Lerp(characterController.height, characterData.CroughHeight, 5 * Time.deltaTime);
                characterController.height = Height;
                WalkingValue = Mathf.Lerp(WalkingValue, characterData.CroughSpeed, 6 * Time.deltaTime);
            }
            else if (!Physics.Raycast(GetComponentInChildren<Camera>().transform.position, transform.TransformDirection(Vector3.up), out CroughCheck, 0.8f, 1))
            {
                if (characterController.height != InstallCroughHeight)
                {
                    isCrough = false;
                    float Height = Mathf.Lerp(characterController.height, InstallCroughHeight, 6 * Time.deltaTime);
                    characterController.height = Height;
                    WalkingValue = Mathf.Lerp(WalkingValue, characterData.WalkingSpeed, 4 * Time.deltaTime);
                }
            }

            if(WallDistance != Physics.Raycast(
                GetComponentInChildren<Camera>().transform.position, 
                transform.TransformDirection(Vector3.forward), out ObjectCheck, characterData.HideDistance, characterData.LayerMaskInt) && 
                characterData.CanHideDistanceWall)
            {
                WallDistance = Physics.Raycast(GetComponentInChildren<Camera>().transform.position, transform.TransformDirection(Vector3.forward), out ObjectCheck, characterData.HideDistance, characterData.LayerMaskInt);
                ItemHide?.Invoke(WallDistance);
            }
        }

        public void EnterPause() => canMove = false;
        public void ExitPause() => canMove = true;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Ladder" && characterData.CanClimbing)
            {
                canRun = false;
                isClimbing = true;
                WalkingValue /= 2;
                ItemHide?.Invoke(true);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "Ladder" && characterData.CanClimbing)
            {
                moveDirection = new Vector3(0, Input.GetAxis("Vertical") * characterData.Speed * (-characterCamera.localRotation.x / 1.7f), 0);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Ladder" && characterData.CanClimbing)
            {
                canRun = true;
                isClimbing = false;
                WalkingValue *= 2;
                ItemHide?.Invoke(false);
            }
        }

        private void OnValidate()
        {
            if (animator == null)
                throw new NullReferenceException("Player has to have animator assigned!");
        }

        public void Activate()
        {
            if (isActive == true)
                return;

            Debug.Log($"{nameof(FirstPersonPlayer)} is activated");
        }

        public void Deactivate()
        {
            if (isActive == false)
                return;

            Debug.Log($"{nameof(FirstPersonPlayer)} is deactivated");
        }
    }
}