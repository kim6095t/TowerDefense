using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class EmissionTower : Tower
{
    [SerializeField] Transform beamPivot;       // 광선이 나오는 지점.
    [SerializeField] float chargingTime;        // 에너지를 모우는 시간.

    LineRenderer line;

    float lookTime = 0.0f;              // 다음 방사 시간.
    bool isLock;                        // 회전 방향 고정.

    private void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    protected override void OnUpdate()
    {
        if (!isLock)
        {
            LookTarget();
            SearchEnemy();

            // 에너지를 다 모았고 적을 발견했을 때.
            if (target != null)
            {
                if ((lookTime += Time.deltaTime) >= 3f)
                {
                    // 빔 발사.
                    isLock = true;
                    CreateBeam();
                }                                                
            }
        }
        else
        {

        }
    }

    private void CreateBeam()
    {
        line.positionCount = 2;
        line.SetPosition(0, beamPivot.position);
        line.SetPosition(1, beamPivot.position + (pivot.forward * attackRadius));
    }      

    protected override void AttackEnemy()
    {
        
    }
}
