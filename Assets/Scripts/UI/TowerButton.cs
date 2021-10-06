using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    public delegate void DlSelectedTower(Tower.TOWER_TYPE type);

    [SerializeField] Image towerImage;
    [SerializeField] Text priceText;

    Tower.TOWER_TYPE type;

    public void Setup(TowerData towerData)
    {
        // 데이터를 받아와 열거형으로 파싱.
        type = (Tower.TOWER_TYPE)System.Enum.Parse(typeof(Tower.TOWER_TYPE), towerData.GetData(Tower.KEY_TYPE));

        // towerImage.sprite = tower.towerSprite;
        priceText.text = string.Format("{0:#,##0}", towerData.GetData(Tower.KEY_PRICE));

        // 버튼에 이벤트 등록.
        GetComponent<Button>().onClick.AddListener(() => TowerManager.Instance.OnSelectedTower(type));
    }
}
