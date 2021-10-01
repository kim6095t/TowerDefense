using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [SerializeField] Tower towerPrefab;
    [SerializeField] LayerMask tileMask;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            // 마우스의 현재 위치를 Ray로 변환.
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, float.MaxValue, tileMask))
            {
                SetTile setTile = hit.collider.GetComponent<SetTile>();
                CreateTower(setTile);
            }
        }
    }

    private void CreateTower(SetTile setTile)
    {
        // 선택한 타일이 없거나 타일에 이미 설치가 되어있는 경우.
        if (setTile == null || setTile.IsSetTower)
            return;

        if (GameManager.Instance.OnUseGold(10))
        {
            Tower newTower = Instantiate(towerPrefab, transform);
            setTile.Set(newTower);
        }
    }

}
