using Adventurer;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace GenshinImpactMovementSystem
{
    [RequireComponent(typeof(PlayerResizableCapsuleCollider))]
    public class ThirdViewPlayer : MonoBehaviour, IHumanAnimatable, IPlayerView
    {
        [field: Header("References")]
        [field: SerializeField] public PlayerSO Data { get; private set; }

        [field: Header("Collisions")]
        [field: SerializeField] public PlayerLayerData LayerData { get; private set; }

        [field: Header("Cameras")]
        [field: SerializeField] public PlayerCameraRecenteringUtility CameraRecenteringUtility { get; private set; }
        [field: SerializeField] public Camera HandsCamera { get; private set; }
        [field: SerializeField] public Camera FirstPersonCamera { get; private set; }

        [field: Header("Animations")]
        [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

        public Rigidbody Rigidbody { get; private set; }
        public Animator Animator { get; private set; }
        public PlayerInput Input { get; private set; }
        public PlayerResizableCapsuleCollider ResizableCapsuleCollider { get; private set; }

        public Transform MainCameraTransform { get; private set; }
        public CinemachineVirtualCamera virtualCamera;
        public CinemachineInputProvider cinemachineInput;
        public bool IsThirdViewActive => isThirdViewActive;

        private PlayerMovementStateMachine movementStateMachine;

        private bool isThirdViewActive = true;

        [Inject]
        private void Construct(PlayerInput playerInput) 
        {
            Input = playerInput;

            movementStateMachine = new PlayerMovementStateMachine(this);
        }

        public void OnMovementStateAnimationEnterEvent()
        {
            movementStateMachine.OnAnimationEnterEvent();
        }

        public void OnMovementStateAnimationExitEvent()
        {
            movementStateMachine.OnAnimationExitEvent();
        }

        public void OnMovementStateAnimationTransitionEvent()
        {
            movementStateMachine.OnAnimationTransitionEvent();
        }

        public Animator GetHumanAnimator() => Animator;

        private void Start()
        {
            CameraRecenteringUtility.Initialize();
            AnimationData.Initialize();

            Rigidbody = GetComponent<Rigidbody>();
            Animator = GetComponentInChildren<Animator>();

            ResizableCapsuleCollider = GetComponent<PlayerResizableCapsuleCollider>();

            MainCameraTransform = Camera.main.transform;

            movementStateMachine.ChangeState(movementStateMachine.IdlingState);
        }

        private void Update()
        {
            movementStateMachine.HandleInput();

            movementStateMachine.Update();
        }

        private void FixedUpdate()
        {
            movementStateMachine.PhysicsUpdate();
        }

        private void OnTriggerEnter(Collider collider)
        {
            if (isThirdViewActive == false)
                return;

            movementStateMachine.OnTriggerEnter(collider);
        }

        private void OnTriggerExit(Collider collider)
        {
            if (isThirdViewActive == false)
                return;

            movementStateMachine.OnTriggerExit(collider);
        }

        public void ChangeToThirdPersonView()
        {
            if (isThirdViewActive == true)
                return;

            isThirdViewActive = true;
            HandsCamera.gameObject.SetActive(false);
            FirstPersonCamera.gameObject.SetActive(true);
            virtualCamera.gameObject.SetActive(true);
            cinemachineInput.enabled = true;
            movementStateMachine.ReusableData.isThirdView = true;
            Debug.Log($"Third person view is activated");
        }

        public void ChangeToFirstPersonView()
        {
            if (isThirdViewActive == false)
                return;

            isThirdViewActive = false;
            HandsCamera.gameObject.SetActive(true);
            FirstPersonCamera.gameObject.SetActive(false);
            virtualCamera.gameObject.SetActive(false);
            cinemachineInput.enabled = false;
            movementStateMachine.ReusableData.isThirdView = false;
            Debug.Log($"First person view is activated");
        }
    }
}