using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButtonManager : MonoBehaviour
{
    [SerializeField] TowerButton buttonPrefab;
    public void Setup(Tower[] towers, TowerButton.DlSelectedTower buttonEvent)
    {
        foreach(Tower tower in towers)
        {
            TowerButton newButton = Instantiate(buttonPrefab, transform);
            newButton.Setup(tower, buttonEvent);
        }
    }
}
