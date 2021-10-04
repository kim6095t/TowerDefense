using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] ParticleSystem vfx;

    private Transform pivot;
    private Enemy target;
    private float moveSpeed;
    protected float power;

    public void Shoot(Enemy target, float moveSpeed, float power)
    {
        pivot = transform;

        this.target = target;
        this.moveSpeed = moveSpeed;
        this.power = power;
    }

    private void Update()
    {
        if(target == null)
        {
            Crushed();
            return;
        }

        MoveTo();
    }

    void MoveTo()
    {
        Vector3 direction = target.transform.position - pivot.position;
        Quaternion lookAt = Quaternion.LookRotation(direction);

        pivot.position = Vector3.MoveTowards(pivot.position, target.transform.position, moveSpeed * Time.deltaTime);
        pivot.rotation = Quaternion.Lerp(pivot.rotation, lookAt, 100f * Time.deltaTime);

        // 타겟과 나의 거리가 (많이) 가까워 졌다면.
        if (Vector3.Distance(pivot.position, target.transform.position) <= float.Epsilon)
        {
            HitTarget();
            Crushed();
        }
    }

    protected virtual void HitTarget()
    {
        target.OnDamaged(power);
        CreateVFX();
    }
    protected void CreateVFX()
    {
        if (vfx == null)
            return;

        ParticleSystem newParticle = Instantiate(vfx);
        newParticle.transform.position = transform.position;
        newParticle.Play();
    }


    private void Crushed()
    {
        Destroy(gameObject);
    }
}
