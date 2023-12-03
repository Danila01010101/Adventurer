using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(MageAnimationPlayer))]
public class WizardController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera mainCamera;
    [SerializeField] private CinemachineBrain virtualCamera;
    [SerializeField] private float speed;

    private MageAnimationPlayer animationPlayer;
    private CharacterController characterController;
    private Vector2 moveDirection;
    private bool isMoving = true;

    private void Awake()
    {
        animationPlayer = GetComponent<MageAnimationPlayer>();
        characterController = GetComponent<CharacterController>();
        mainCamera.transform.SetParent(null);
        virtualCamera.transform.SetParent(null);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    private void CheckMoveInput()
    {
        Vector3 direction = new Vector3(-moveDirection.y, -9.1f, moveDirection.x);
        characterController.Move(direction * Time.deltaTime * speed);
    }

    private void Update()
    {
        CheckMoveInput();
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