using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    public delegate void DlSelectedTower(Tower.TOWER_TYPE type);

    [SerializeField] Image towerImage;
    [SerializeField] Text priceText;

    Tower linkedTower;

    public void Setup(Tower tower, DlSelectedTower buttonEvent)
    {
        linkedTower = tower;

        towerImage.sprite = tower.towerSprite;
        priceText.text = tower.Price.ToString("#,##0");
        
        GetComponent<Button>().onClick.AddListener(() => buttonEvent(linkedTower.Type));
    }
}
