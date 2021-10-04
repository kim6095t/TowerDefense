using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Bullet
{
    [Header("Explode")]
    [SerializeField] LayerMask explodeMask;     // 폭발 마스크.
    [SerializeField] float explodRadius;        // 폭발 반경.

    protected override void HitTarget()
    {
        // base.HitTarget();       // Bullet의 기존 HitTarget을 먼저 실행.
        Collider[] targets = Physics.OverlapSphere(transform.position, explodRadius, explodeMask);
        foreach (Collider target in targets)
        {
            IDamaged enemy = target.GetComponent<IDamaged>();
            if (enemy != null)
                enemy.OnDamaged(power);
        }

        CreateVFX();
    }

#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, searchRadius);

        UnityEditor.Handles.color = Color.green;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, explodRadius);
    }

#endif
}
