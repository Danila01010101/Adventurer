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

        [field: Header("Camera")]
        [field: SerializeField] public PlayerCameraRecenteringUtility CameraRecenteringUtility { get; private set; }

        [field: Header("Animations")]
        [field: SerializeField] public PlayerAnimationData AnimationData { get; private set; }

        public Rigidbody Rigidbody { get; private set; }
        public Animator Animator { get; private set; }
        public PlayerInput Input { get; private set; }
        public PlayerResizableCapsuleCollider ResizableCapsuleCollider { get; private set; }

        public Transform MainCameraTransform { get; private set; }
        public CinemachineVirtualCamera virtualCamera;
        public CinemachineInputProvider cinemachineInput;
        public bool IsActive => isActive;

        private PlayerMovementStateMachine movementStateMachine;

        private bool isActive = true;

        [Inject]
        private void Construct(PlayerInput playerInput) => Input = playerInput;

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

            movementStateMachine = new PlayerMovementStateMachine(this);

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
            if (isActive == false)
                return;

            movementStateMachine.OnTriggerEnter(collider);
        }

        private void OnTriggerExit(Collider collider)
        {
            if (isActive == false)
                return;

            movementStateMachine.OnTriggerExit(collider);
        }

        public void Activate()
        {
            if (isActive == true)
                return;

            isActive = true;
            virtualCamera.gameObject.SetActive(true);
            cinemachineInput.enabled = true;
            this.enabled = true;
            Debug.Log($"{nameof(ThirdViewPlayer)} is activated");
        }

        public void Deactivate()
        {
            if (isActive == false)
                return;

            isActive = false;
            virtualCamera.gameObject.SetActive(false);
            cinemachineInput.enabled = false;
            this.enabled = false;
            Debug.Log($"{nameof(ThirdViewPlayer)} is deactivated");
        }
    }
}