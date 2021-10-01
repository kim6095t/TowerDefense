using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum PHASE
    {
        Ready,          // 준비 상태.
        Enemy,          // 적 생성.
        GameClear,      // 게임 클리어.
        GameOver,       // 게임 오버.
    }

    [SerializeField] EnemyManager enemyManager; // 적 생성 매니저.
    [SerializeField] float phaseWaitTime;       // 다음 페이즈까지 기다리는 시간.

    float nextPhaseTime;                        // 다음 페이즈 시간.
    PHASE phase;

    private void Start()
    {
        nextPhaseTime += Time.time + phaseWaitTime;
        phase = PHASE.Ready;
    }

    
    private void Update()
    {
        switch(phase)
        {
            case PHASE.Ready:
                if (nextPhaseTime <= Time.time)
                {
                    Debug.Log("Start Enemy Phase");
                    enemyManager.OnStartEnemy(OnEndEnemyPhase);
                    phase = PHASE.Enemy;
                }
                break;

            case PHASE.Enemy:

                break;
        }
    }

    private void OnEndEnemyPhase()
    {
        Debug.Log("End Enemy Phase");

        nextPhaseTime = Time.time + phaseWaitTime;

        // 승리 체크.
        // 실패 체크.

        phase = PHASE.Ready;
    }
}
