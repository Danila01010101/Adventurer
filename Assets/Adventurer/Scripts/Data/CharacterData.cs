using UnityEngine;

[CreateAssetMenu(fileName = "StartCharacterData", menuName = "ScriptableObjects/StartCharacterData")]
public class CharacterData : ScriptableObject
{
    [Header("PlayerController")]
    [SerializeField, Range(1, 10)] private float walkingSpeed = 3.0f;
    [SerializeField, Range(0.1f, 5)] private float croughSpeed = 1.0f;
    [SerializeField, Range(2, 20)] private float runningSpeed = 4.0f;
    [SerializeField, Range(0, 20)] private float jumpSpeed = 6.0f;
    [SerializeField, Range(0.5f, 10)] private float lookSpeed = 2.0f;
    [SerializeField, Range(10, 120)] private float lookXLimit = 80.0f;
    [Space(20)]

    [Header("Advance")]
    [SerializeField] private bool canRun = true;
    [SerializeField] private float runningFOV = 65.0f;
    [SerializeField] private float speedToFOV = 4.0f;
    [SerializeField] private float croughHeight = 1.0f;
    [SerializeField] private float gravity = 20.0f;
    [SerializeField] private float timeToRunning = 2.0f;
    [Space(20)]

    [Header("Climbing")]
    [SerializeField] private bool canClimbing = true;
    [SerializeField, Range(1, 25)] private float speed = 2f;
    [Space(20)]

    [Header("HandsHide")]
    [SerializeField] private bool canHideDistanceWall = true;
    [SerializeField, Range(0.1f, 5)] private float hideDistance = 1.5f;
    [SerializeField] private int layerMaskInt = 1;
    [Space(20)]

    [Header("Input")]
    [SerializeField] private KeyCode croughKey = KeyCode.LeftControl;

    #region PublicProperties
    public float WalkingSpeed => walkingSpeed;
    public float CroughSpeed => croughSpeed;
    public float RunningSpeed => runningSpeed;
    public float JumpSpeed => jumpSpeed;
    public float LookSpeed => lookSpeed;
    public float LookXLimit => lookXLimit;
    public bool CanRun => canRun;
    public float RunningFOV => runningFOV;
    public float SpeedToFOV => speedToFOV;
    public float CroughHeight => croughHeight;
    public float Gravity => gravity;
    public float TimeToRunning => timeToRunning;
    public bool CanClimbing => canClimbing;
    public float Speed => speed;
    public bool CanHideDistanceWall => canHideDistanceWall;
    public float HideDistance => hideDistance;
    public int LayerMaskInt => layerMaskInt;
    public KeyCode CroughKey => croughKey;
    #endregion
}