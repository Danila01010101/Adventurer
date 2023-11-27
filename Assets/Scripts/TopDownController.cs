using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class TopDownController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera mainCamera;
    [SerializeField] private CinemachineBrain virtualCamera;
    [SerializeField] private float speed;

    private CharacterController characterController;
    private Vector2 moveDirection;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera.transform.SetParent(null);
        virtualCamera.transform.SetParent(null);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    private void Move()
    {
        Vector3 direction = new Vector3(-moveDirection.y, -9.1f, moveDirection.x);

        characterController.Move(direction * Time.deltaTime * speed);
    }

    private void Update()
    {
        Move();
    }
}
