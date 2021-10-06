using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Tower;     // Tower 클래스의 영역을 포함하겠다.

public class TowerData
{
    private Dictionary<string, string> data;

    public TowerData(Dictionary<string, string> data)
    {
        this.data = data;
    }

    public string GetData(string key)
    {
        return data[key];
    }
}

public class TowerManager : MonoBehaviour
{
    // 싱글톤 (SingleTon)
    static TowerManager instance;
    public static TowerManager Instance => instance;

    [SerializeField] TextAsset          data;
    [SerializeField] Tower[]            towerPrefabs;
    [SerializeField] LayerMask          tileMask;

    Dictionary<TOWER_TYPE, TowerData> towerDatas;           // 가공된 타워 데이터.
    TOWER_TYPE selectedType = TOWER_TYPE.None;              // 현재 선택한 타워의 타입.

    private void Awake()
    {
        instance = this;

        // CSV데이터를 우리가 원하는 데이터로 가공.
        towerDatas = new Dictionary<TOWER_TYPE, TowerData>();
        Dictionary<string, string>[] csvDatas = CSVReader.ReadCSV(data);
        for (int i = 0; i < csvDatas.Length; i++)
        {
            TowerData newData = new TowerData(csvDatas[i]);
            TOWER_TYPE type = (TOWER_TYPE)System.Enum.Parse(typeof(TOWER_TYPE), newData.GetData(KEY_TYPE));

            towerDatas.Add(type, newData);
        }
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && selectedType != Tower.TOWER_TYPE.None)
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

    public TowerData GetData(TOWER_TYPE type)
    {
        return towerDatas[type];
    }

    private void CreateTower(SetTile setTile)
    {
        // 선택한 타일이 없거나 타일에 이미 설치가 되어있는 경우.
        if (setTile == null || setTile.IsSetTower)
            return;

        int needGold = towerPrefabs[(int)selectedType].Price;   // 원하는 타워의 가격.
                
        if (GameManager.Instance.OnUseGold(needGold))           // 골드 소비 시도.
        {
            Tower newTower = Instantiate(towerPrefabs[(int)selectedType], transform);
            newTower.Setup(towerDatas[newTower.Type]);
            
            setTile.Set(newTower);
            selectedType = Tower.TOWER_TYPE.None;
        }
    }
    public void OnSelectedTower(Tower.TOWER_TYPE type)
    {
        Debug.Log($"Selected : {type}");
        selectedType = type;
    }
}
