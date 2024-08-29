using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float turnSpeed = 200f;
    public float acceleration = 8f;
    public float deceleration = 6f;
    public float gravity = 20f;
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer;
    public Transform[] waypoints;
    public float waypointThreshold = 1f;

    public float obstacleAvoidanceDistance = 2f; // Distance to detect obstacles
    public float obstacleAvoidanceStrength = 1f; // Strength of obstacle avoidance steering
    public LayerMask obstacleLayer; // Layer for obstacles

    private Rigidbody rb;
    private Animator animator;
    private Vector3 moveDirection;
    private Vector3 velocity;
    private bool isGrounded;
    private int currentWaypointIndex;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        rb.freezeRotation = true;
        currentWaypointIndex = 0;
    }

    void Update()
    {
        //HandleMovement();
        //HandleGroundCheck();
        //HandleGravity();
        //HandleAnimation();
    }

    public void HandleMovement()
    {
        if (waypoints.Length == 0) return;

        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        moveDirection = (targetPosition - transform.position).normalized;

        // Check if the NPC is close to the current waypoint
        if (Vector3.Distance(transform.position, targetPosition) < waypointThreshold)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        // Apply obstacle avoidance
        moveDirection = AvoidObstacles(moveDirection);

        if (moveDirection.magnitude > 0)
        {
            Vector3 targetVelocity = moveDirection * moveSpeed;
            velocity = Vector3.Lerp(velocity, targetVelocity, acceleration * Time.deltaTime);

            // Smoothly rotate towards the target direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
        }
        else
        {
            velocity = Vector3.Lerp(velocity, Vector3.zero, deceleration * Time.deltaTime);
        }

        Vector3 movement = velocity * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    Vector3 AvoidObstacles(Vector3 direction)
    {
        RaycastHit hit;

        // Check if there's an obstacle in front
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, transform.forward, out hit, obstacleAvoidanceDistance, obstacleLayer))
        {
            // Calculate a new direction to avoid the obstacle
            Vector3 avoidDirection = Vector3.Cross(hit.normal, Vector3.up).normalized;
            direction += avoidDirection * obstacleAvoidanceStrength;
        }

        return direction.normalized;
    }

    public void HandleGroundCheck()
    {
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out hit, groundCheckDistance, groundLayer);
    }

    public void HandleGravity()
    {
        if (!isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = 0f;
        }
    }

    public void HandleAnimation()
    {
        animator.SetFloat("Speed", velocity.magnitude);
        animator.SetBool("IsGrounded", isGrounded);
    }

    void OnDrawGizmos()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        Gizmos.color = Color.yellow;
        for (int i = 0; i < waypoints.Length; i++)
        {
            Gizmos.DrawSphere(waypoints[i].position, 0.5f);
            if (i < waypoints.Length - 1)
            {
                Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
            }
        }

        // Draw ray for obstacle detection
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position + Vector3.up * 0.1f, transform.forward * obstacleAvoidanceDistance);
    }
}