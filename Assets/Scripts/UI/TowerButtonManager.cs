using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tower;

public class TowerButtonManager : MonoBehaviour
{
    [SerializeField] TowerButton buttonPrefab;

    private void Start()
    {
        for(TOWER_TYPE type = 0; type < TOWER_TYPE.Count; type++)
        {
            TowerData towerData = TowerManager.Instance.GetData(type);
            TowerButton newButton = Instantiate(buttonPrefab, transform);

            newButton.Setup(towerData);
        }
    }
}
