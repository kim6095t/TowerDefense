using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTower : Tower
{
    [Header("Bullet")]
    [SerializeField] Bullet bullet;
    [SerializeField] Transform bulletPivot;
    [SerializeField] float moveSpeed;

    protected float nextAttackTime = 0.0f;

    protected override void OnUpdate()
    {
        LookTarget();

        if (target == null)
            SearchEnemy();
        else
            AttackEnemy();
    }

    protected override void AttackEnemy()
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
}
