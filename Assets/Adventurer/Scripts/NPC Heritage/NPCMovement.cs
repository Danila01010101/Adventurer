using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class NPCMovement : MonoBehaviour
{
    public Transform[] points;  // �����, ���� NPC ����� ������������
    public RoadCrossing[] roadCrossings;  // �������� ����� ������ (� �������, ��������� � ������)
    public float speed = 3.0f;  // �������� ����������� NPC
    public Transform target;  // ������� ����� ��� NPC
    private Queue<Transform> path = new Queue<Transform>();  // ���� NPC
    private Transform currentTarget;  // ������� �����, � ������� ���� NPC
    private Rigidbody npcRigidbody;  // Rigidbody ��� NPC

    // �������� ���������� ������ ������ (��������, �� ��� X)
    private float roadXPosition = 0f;

    // ����� ��� ������������ ��������� ������ � ��������
    private bool isOnRoad = false;
    private bool isCrossingRoad = false;
    private bool hasPassedCrossing = false;

    // LayerMask ��� ����������� �����
    public LayerMask roadLayerMask;

    void Start()
    {
        npcRigidbody = GetComponent<Rigidbody>();
        npcRigidbody.isKinematic = true;  // ��������� ����������, NPC ��������� �������
        SetNewTarget();  // ����� ����� ����
    }

    void Update()
    {
        if (points.Length!=0) 
        {
            if (currentTarget != null)
            {
                MoveTowards(currentTarget);  // �������� � ������� �����
            }
            else if (path.Count > 0)
            {
                currentTarget = path.Dequeue();  // �������� ��������� ����� ����
            }
            else
            {
                SetNewTarget();  // ������ ����� ���� ����� ���������� ����������
            }

            // ���������, ���� NPC ��������� �� ������
            if (isOnRoad && !isCrossingRoad && !hasPassedCrossing)
            {
                // ���� NPC �� ��������� � ��������, �� ���� ������� � ��������� ��� � ����
                if (path.Count == 0)
                {
                    RoadCrossing crossing = FindClosestCrossing(transform.position, target.position);
                    Transform start = IsOnSameSide(transform.position, crossing.startPoint.position) ? crossing.startPoint : crossing.endPoint;
                    Transform end = IsOnSameSide(target.position, crossing.startPoint.position) ? crossing.startPoint : crossing.endPoint;

                    path.Enqueue(start);
                    path.Enqueue(crossing.middlePoint);
                    path.Enqueue(end);
                    path.Enqueue(target);  // ��������� ��������� ���� � ����
                }
            }
        }
    }

    // ����� ��� ������� ����� ���� NPC
    void SetNewTarget()
    {
        target = points[Random.Range(0, points.Length)];  // ��������� �����
        if (!IsOnSameSide(transform.position, target.position))
        {
            // ���� NPC � ���� �� ������ ��������, ���� ����� �������
            RoadCrossing crossing = FindClosestCrossing(transform.position, target.position);

            // ���������� ����� ������ � ����� � ����������� �� ��������� NPC � ����
            Transform start = IsOnSameSide(transform.position, crossing.startPoint.position) ? crossing.startPoint : crossing.endPoint;
            Transform end = IsOnSameSide(target.position, crossing.startPoint.position) ? crossing.startPoint : crossing.endPoint;

            // ��������� ���� ����� ������, �������� � ����� ��������
            path.Enqueue(start);
            path.Enqueue(crossing.middlePoint);
            path.Enqueue(end);
        }
        path.Enqueue(target);  // ��������� ��������� ���� � ����
        currentTarget = path.Dequeue();  // ������������� ������ ����� ��� ��������
    }

    // �������� NPC � ���� � �������������� Rigidbody
    void MoveTowards(Transform destination)
    {
        Vector3 direction = destination.position - transform.position;
        if (direction.magnitude < 0.1f)
        {
            currentTarget = null;  // �������� ������� ����
            if (isCrossingRoad)
            {
                hasPassedCrossing = true;  // NPC �������� ������� ����� ������
                isCrossingRoad = false;  // ����� ��������� ��������
                Debug.Log("Finished crossing the road.");
            }
        }
        else
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, destination.position, speed * Time.deltaTime);
            npcRigidbody.MovePosition(newPosition);  // ���������� NPC � ������� Rigidbody
        }
    }

    // ��������, ��������� �� ����� �� ����� ������� ������
    bool IsOnSameSide(Vector3 pos1, Vector3 pos2)
    {
        return (pos1.x < roadXPosition && pos2.x < roadXPosition) || (pos1.x > roadXPosition && pos2.x > roadXPosition);
    }

    // ���������� ���������� �������� ��� NPC, ����������� �� ��������� NPC � ����
    RoadCrossing FindClosestCrossing(Vector3 npcPosition, Vector3 targetPosition)
    {
        RoadCrossing closest = roadCrossings[0];
        float minDistance = GetCrossingDistance(npcPosition, targetPosition, closest);

        foreach (RoadCrossing crossing in roadCrossings)
        {
            float distance = GetCrossingDistance(npcPosition, targetPosition, crossing);
            if (distance < minDistance)
            {
                closest = crossing;
                minDistance = distance;
            }
        }
        return closest;
    }

    // ������������ ����������� ���������� �� ��������, �������� ����� ������ � �����
    float GetCrossingDistance(Vector3 npcPosition, Vector3 targetPosition, RoadCrossing crossing)
    {
        float distanceToStart = Vector3.Distance(npcPosition, crossing.startPoint.position);
        float distanceToEnd = Vector3.Distance(npcPosition, crossing.endPoint.position);
        float distanceToTargetStart = Vector3.Distance(targetPosition, crossing.startPoint.position);
        float distanceToTargetEnd = Vector3.Distance(targetPosition, crossing.endPoint.position);

        return Mathf.Min(distanceToStart + distanceToTargetStart, distanceToEnd + distanceToTargetEnd);
    }

    // ��������� ����� � ������� ������
    private void OnTriggerEnter(Collider other)
    {
        if ((roadLayerMask & (1 << other.gameObject.layer)) != 0)
        {
            // ������������� ����� roadXPosition � ����������� �� ������� ������
            roadXPosition = other.bounds.center.x;
            isOnRoad = true;  // NPC �� ������
            Debug.Log("Entered road trigger: " + other.name);
        }
    }

    // ��������� ������ �� �������� ������
    private void OnTriggerExit(Collider other)
    {
        if ((roadLayerMask & (1 << other.gameObject.layer)) != 0)
        {
            isOnRoad = false;  // NPC ������ �� �� ������
            Debug.Log("Exited road trigger: " + other.name);
        }
    }

    // ��������� ����� � ������� �������� ����� ������
    private void OnTriggerEnterRoadCrossing(Collider other)
    {
        if (other.CompareTag("RoadCrossing"))
        {
            isCrossingRoad = true;  // NPC �������� ������� ����� ������
            Debug.Log("Started crossing the road.");
        }
    }

    // ��������� ������ �� �������� �������� ����� ������
    private void OnTriggerExitRoadCrossing(Collider other)
    {
        if (other.CompareTag("RoadCrossing"))
        {
            hasPassedCrossing = true;  // NPC �������� ������� ����� ������
            isCrossingRoad = false;  // ����� ��������� ��������
            Debug.Log("Finished crossing the road.");
        }
    }
}

// ����� ��� �������� �������� ����� ������
[System.Serializable]
public class RoadCrossing
{
    public Transform startPoint;  // ��������� ����� ��������
    public Transform middlePoint; // ������� ����� �������� (� �������� ������)
    public Transform endPoint;    // �������� ����� ��������
}