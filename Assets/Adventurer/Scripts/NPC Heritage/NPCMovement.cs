using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class NPCMovement : MonoBehaviour
{
    public Transform[] points;  // Точки, куда NPC может перемещаться
    public RoadCrossing[] roadCrossings;  // Переходы через дорогу (с началом, серединой и концом)
    public float speed = 3.0f;  // Скорость перемещения NPC
    public Transform target;  // Целевая точка для NPC
    private Queue<Transform> path = new Queue<Transform>();  // Путь NPC
    private Transform currentTarget;  // Текущая точка, к которой идет NPC
    private Rigidbody npcRigidbody;  // Rigidbody для NPC

    // Условное разделение сторон дороги (например, по оси X)
    private float roadXPosition = 0f;

    // Флаги для отслеживания состояния дороги и перехода
    private bool isOnRoad = false;
    private bool isCrossingRoad = false;
    private bool hasPassedCrossing = false;

    // LayerMask для определения дорог
    public LayerMask roadLayerMask;

    void Start()
    {
        npcRigidbody = GetComponent<Rigidbody>();
        npcRigidbody.isKinematic = true;  // Отключаем гравитацию, NPC двигается вручную
        SetNewTarget();  // Выбор новой цели
    }

    void Update()
    {
        if (points.Length!=0) 
        {
            if (currentTarget != null)
            {
                MoveTowards(currentTarget);  // Движение к текущей точке
            }
            else if (path.Count > 0)
            {
                currentTarget = path.Dequeue();  // Получаем следующую точку пути
            }
            else
            {
                SetNewTarget();  // Ставим новую цель после достижения предыдущей
            }

            // Проверяем, если NPC находится на дороге
            if (isOnRoad && !isCrossingRoad && !hasPassedCrossing)
            {
                // Если NPC не находится в переходе, то ищем переход и добавляем его в путь
                if (path.Count == 0)
                {
                    RoadCrossing crossing = FindClosestCrossing(transform.position, target.position);
                    Transform start = IsOnSameSide(transform.position, crossing.startPoint.position) ? crossing.startPoint : crossing.endPoint;
                    Transform end = IsOnSameSide(target.position, crossing.startPoint.position) ? crossing.startPoint : crossing.endPoint;

                    path.Enqueue(start);
                    path.Enqueue(crossing.middlePoint);
                    path.Enqueue(end);
                    path.Enqueue(target);  // Добавляем финальную цель к пути
                }
            }
        }
    }

    // Метод для задания новой цели NPC
    void SetNewTarget()
    {
        target = points[Random.Range(0, points.Length)];  // Случайная точка
        if (!IsOnSameSide(transform.position, target.position))
        {
            // Если NPC и цель на разных сторонах, идем через переход
            RoadCrossing crossing = FindClosestCrossing(transform.position, target.position);

            // Определяем точки начала и конца в зависимости от положения NPC и цели
            Transform start = IsOnSameSide(transform.position, crossing.startPoint.position) ? crossing.startPoint : crossing.endPoint;
            Transform end = IsOnSameSide(target.position, crossing.startPoint.position) ? crossing.startPoint : crossing.endPoint;

            // Добавляем путь через начало, середину и конец перехода
            path.Enqueue(start);
            path.Enqueue(crossing.middlePoint);
            path.Enqueue(end);
        }
        path.Enqueue(target);  // Добавляем финальную цель к пути
        currentTarget = path.Dequeue();  // Устанавливаем первую точку для движения
    }

    // Движение NPC к цели с использованием Rigidbody
    void MoveTowards(Transform destination)
    {
        Vector3 direction = destination.position - transform.position;
        if (direction.magnitude < 0.1f)
        {
            currentTarget = null;  // Достигли текущей цели
            if (isCrossingRoad)
            {
                hasPassedCrossing = true;  // NPC завершил переход через дорогу
                isCrossingRoad = false;  // Сброс состояния перехода
                Debug.Log("Finished crossing the road.");
            }
        }
        else
        {
            Vector3 newPosition = Vector3.MoveTowards(transform.position, destination.position, speed * Time.deltaTime);
            npcRigidbody.MovePosition(newPosition);  // Перемещаем NPC с помощью Rigidbody
        }
    }

    // Проверка, находятся ли точки на одной стороне дороги
    bool IsOnSameSide(Vector3 pos1, Vector3 pos2)
    {
        return (pos1.x < roadXPosition && pos2.x < roadXPosition) || (pos1.x > roadXPosition && pos2.x > roadXPosition);
    }

    // Нахождение ближайшего перехода для NPC, основываясь на положении NPC и цели
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

    // Рассчитываем минимальное расстояние до перехода, учитывая точки начала и конца
    float GetCrossingDistance(Vector3 npcPosition, Vector3 targetPosition, RoadCrossing crossing)
    {
        float distanceToStart = Vector3.Distance(npcPosition, crossing.startPoint.position);
        float distanceToEnd = Vector3.Distance(npcPosition, crossing.endPoint.position);
        float distanceToTargetStart = Vector3.Distance(targetPosition, crossing.startPoint.position);
        float distanceToTargetEnd = Vector3.Distance(targetPosition, crossing.endPoint.position);

        return Mathf.Min(distanceToStart + distanceToTargetStart, distanceToEnd + distanceToTargetEnd);
    }

    // Обработка входа в триггер дороги
    private void OnTriggerEnter(Collider other)
    {
        if ((roadLayerMask & (1 << other.gameObject.layer)) != 0)
        {
            // Устанавливаем новый roadXPosition в зависимости от текущей дороги
            roadXPosition = other.bounds.center.x;
            isOnRoad = true;  // NPC на дороге
            Debug.Log("Entered road trigger: " + other.name);
        }
    }

    // Обработка выхода из триггера дороги
    private void OnTriggerExit(Collider other)
    {
        if ((roadLayerMask & (1 << other.gameObject.layer)) != 0)
        {
            isOnRoad = false;  // NPC больше не на дороге
            Debug.Log("Exited road trigger: " + other.name);
        }
    }

    // Обработка входа в триггер перехода через дорогу
    private void OnTriggerEnterRoadCrossing(Collider other)
    {
        if (other.CompareTag("RoadCrossing"))
        {
            isCrossingRoad = true;  // NPC начинает переход через дорогу
            Debug.Log("Started crossing the road.");
        }
    }

    // Обработка выхода из триггера перехода через дорогу
    private void OnTriggerExitRoadCrossing(Collider other)
    {
        if (other.CompareTag("RoadCrossing"))
        {
            hasPassedCrossing = true;  // NPC завершил переход через дорогу
            isCrossingRoad = false;  // Сброс состояния перехода
            Debug.Log("Finished crossing the road.");
        }
    }
}

// Класс для описания перехода через дорогу
[System.Serializable]
public class RoadCrossing
{
    public Transform startPoint;  // Начальная точка перехода
    public Transform middlePoint; // Средняя точка перехода (в середине дороги)
    public Transform endPoint;    // Конечная точка перехода
}