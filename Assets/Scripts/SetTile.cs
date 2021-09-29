using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetTile : MonoBehaviour
{
    [SerializeField] Tower setTower;      // 내 위에 설치된 타워.
    public bool IsSetTower => setTower != null;

    public void Set(Tower newTower)
    {
        if(IsSetTower)
        {
            Debug.Log("타워가 이미 설치되어있음.");
            return;
        }

        setTower = newTower;
        newTower.transform.position = transform.position;   // 위치 값 갱신.
        newTower.transform.rotation = transform.rotation;   // 회전 값 갱신.
    }
    public Tower Remove()
    {
        Tower removeTower = setTower;
        setTower = null;

        return removeTower;
    }

}
