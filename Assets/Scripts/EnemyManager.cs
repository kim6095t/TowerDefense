using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] Transform waypointParent;  // 웨이 포인트의 부모 오브젝트.
    [SerializeField] EnemyTile enemyPrefab;     // 적의 프리팹.
    [SerializeField] int spawnCount;            // 적의 생성 수.
    [SerializeField] float spawnRate;           // 적의 생성 빈도.

    Transform[] waypoints;

    void Start()
    {
        // waypointParent 하위의 자식들을 이용해 배열 할당.
        waypoints = new Transform[waypointParent.childCount];
        for (int i = 0; i < waypoints.Length; i++)
            waypoints[i] = waypointParent.GetChild(i);

        StartCoroutine(SpawnProcess());
    }

    IEnumerator SpawnProcess()
    {
        int remainingCount = spawnCount;        // 남은 생성 수.
        while((remainingCount -= 1) >= 0)       // 적 생성 시 남은 수가 0이상일 경우.
        {
            yield return new WaitForSeconds(spawnRate);                 // spawnRate만큼 대기.
            EnemyTile newEnemy = Instantiate(enemyPrefab, transform);   // 적 프리팹 생성. (나의 하위)
            newEnemy.transform.position = waypoints[0].position;        // 생성 위치는 0번째 웨이 포인트.

            newEnemy.SetDestination(waypoints);                         // 적에게 목적지 설정.
        }
    }

}
