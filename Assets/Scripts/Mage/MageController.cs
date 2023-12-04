using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(MageAnimationPlayer))]
public class MageController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera mainCamera;
    [SerializeField] private CinemachineBrain virtualCamera;
    [Header("Character scripts")]
    [SerializeField] private MageAnimationPlayer animationPlayer;
    [SerializeField] private CharacterController characterController;
    [Header("Character stats")]
    [SerializeField] private float speed;

    private Transform cameraFollowPoint;
    private Vector3 gravityVector = Vector3.up;
    private Vector2 moveDirection;
    private bool isMoving = true;

    private void OnValidate()
    {
        animationPlayer ??= GetComponent<MageAnimationPlayer>();
        characterController ??= GetComponent<CharacterController>();
    }

    private void Awake()
    {
        cameraFollowPoint = new GameObject("Camera Follow Point").transform;
        mainCamera.transform.SetParent(null);
        virtualCamera.transform.SetParent(null);
        mainCamera.Follow = cameraFollowPoint;
        mainCamera.LookAt = cameraFollowPoint;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    private void CheckMoveInput()
    {
        //virtualCamera.transform.rotation.y
        Vector3 direction = (moveDirection.y * transform.forward) + (gravityVector * -9.1f) + (moveDirection.x * transform.right);
        if (direction.x != 0 && direction.z != 0)
        {
            transform.eulerAngles = new Vector3(0, virtualCamera.transform.eulerAngles.y, 0);
        }
        characterController.Move((direction) * Time.deltaTime * speed);
    }

    private void Update()
    {
        CheckMoveInput();
        cameraFollowPoint.transform.position = transform.position;
    }

    private void FixedUpdate()
    {
        var xzDirection = new Vector3(characterController.velocity.x, 0, characterController.velocity.z);
        if (xzDirection.magnitude < 0.1f)
        {
            if (isMoving)
            {
                PlayRandomIdleAnimation();
                isMoving = false;
            }
        }
        else if (!isMoving)
        {
            isMoving = true;
            animationPlayer.ChangeAnimationState(MageAnimationNames.BattleRunState);
        }
    }

    private void PlayRandomIdleAnimation()
    {
        int randomIdleIndex = Random.Range(0, 3);
        string animationName = MageAnimationNames.IdleState1;
        switch (randomIdleIndex)
        {
            case 1:
                animationName = MageAnimationNames.IdleState2;
                break;
            case 2:
                animationName = MageAnimationNames.IdleState3;
                break;
        }
        animationPlayer.ChangeAnimationState(animationName);
    }
}