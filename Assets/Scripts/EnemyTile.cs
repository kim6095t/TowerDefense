using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTile : MonoBehaviour
{
    [SerializeField] float moveSpeed;       // 이동 속도.

    Transform[] destinations;               // 목적지 위치.
    int currentIndex;                       // 목적지 번호.

    bool isMoving;                          // 움직이고 있는가?

    public void SetDestination(Transform[] destinations)
    {
        this.destinations = destinations;
        currentIndex = 0;
        isMoving = true;
    }

    void Update()
    {
        if (!isMoving)
            return;

        Vector3 destination = destinations[currentIndex].position;  // 목적지 좌표.
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
        if(transform.position == destination)
            InDestination();
    }

    void InDestination()
    {
        currentIndex++;
        if(currentIndex >= destinations.Length)     // 최종 목적지까지 도달했다.
        {
            OnGoal();
        }
    }
    void OnGoal()
    {
        Destroy(gameObject);
    }
}
