using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [Header("Search")]
    [SerializeField] LayerMask searchMask;    

    [Header("Bullet")]
    [SerializeField] Bullet bullet;
    [SerializeField] Transform bulletPivot;
    [SerializeField] float moveSpeed;

    [Header("Combat")]    
    [SerializeField] float attackPower;
    [SerializeField] float attackRate;
    [SerializeField] float attackRadius;

    private Transform pivot;
    private Enemy target = null;
    private float nextAttackTime = 0.0f;

    private void Start()
    {
        pivot = transform;        
    }

    void Update()
    {
        if (target == null)
            SearchEnemy();
        else
            AttackEnemy();
    }

    private void SearchEnemy()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, attackRadius, searchMask);
        if(targets.Length > 0)
        {
            Collider pick = targets[Random.Range(0, targets.Length - 1)];
            Enemy enemy = pick.GetComponent<Enemy>();

            if(enemy != null)
                target = enemy;
        }
    }
    private void AttackEnemy()
    {
        // 공격 직전에 타겟이 없을(죽었을) 경우.
        if (target == null)             
            return;

        // 공격 직전 적과 나의 거리가 공격 사거리보다 길 경우.
        float distance = Vector3.Distance(transform.position, target.transform.position);
        if (distance > attackRadius)
        {
            target = null;
            return;
        }

        Vector3 direction = target.transform.position - pivot.position;     // (회전) 오일러 방향
        direction.Normalize();                                              // 0.0 ~ 1.0f 사이 값으로 정규화.
        Quaternion lookAt = Quaternion.LookRotation(direction);             // (회전) 오일러 -> 쿼터니언
        //pivot.rotation = lookAt;

        // Lerp : 현재 -> 목적지 값까지 시간의 경과에 따른 사이 값을 준다.
        // Smooth rotation.
        pivot.rotation = Quaternion.Lerp(pivot.rotation, lookAt, 10f * Time.deltaTime);

        //transform.position = new Vector3(100, 100, 100);
        //transform.rotation = Quaternion.Euler(90, 90, 90);                // 오일러 -> 쿼터니언.

        // 타워의 공격.
        if (nextAttackTime <= Time.time)
        {
            nextAttackTime = Time.time + attackRate;
            Bullet newBullet = Instantiate(bullet);
            newBullet.transform.position = bulletPivot.position;
            newBullet.transform.rotation = bulletPivot.rotation;
            newBullet.Shoot(target, moveSpeed, attackPower);
        }
    }

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, searchRadius);

        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, attackRadius);
    }

#endif
}
