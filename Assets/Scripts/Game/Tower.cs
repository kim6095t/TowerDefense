using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public static string KEY_NAME     = "Name";
    public static string KEY_TYPE     = "Type";
    public static string KEY_POWER    = "Power";
    public static string KEY_ATKSPD   = "AttackRate";
    public static string KEY_ATKRANGE = "AttackRadius";
    public static string KEY_PRICE    = "Price";

    public enum TOWER_TYPE
    {
        None = -1,

        Standard,
        Missile,
        Raser,

        Count,
    }

    [Header("Info")]
    [SerializeField] TOWER_TYPE type;
    [SerializeField] Sprite sprite;

    [Header("Search")]
    [SerializeField] protected Transform pivot;
    [SerializeField] protected LayerMask searchMask;

    public TOWER_TYPE Type => type;
    public Sprite towerSprite => sprite;
    public int Price => price;

    private int price;

    protected float attackPower;
    protected float attackRate;
    protected float attackRadius;

    protected Enemy target = null;
    

    void Update()
    {
        OnUpdate();
    }

    protected abstract void OnUpdate();
    protected virtual void LookTarget()
    {
        if (target == null)
            return;

        Vector3 direction = target.transform.position - pivot.position;     // (회전) 오일러 방향
        direction.Normalize();                                              // 0.0 ~ 1.0f 사이 값으로 정규화.
        Quaternion lookAt = Quaternion.LookRotation(direction);             // (회전) 오일러 -> 쿼터니언
        //pivot.rotation = lookAt;

        // Lerp : 현재 -> 목적지 값까지 시간의 경과에 따른 사이 값을 준다.
        // Smooth rotation.
        pivot.rotation = Quaternion.Lerp(pivot.rotation, lookAt, 10f * Time.deltaTime);

        //transform.position = new Vector3(100, 100, 100);
        //transform.rotation = Quaternion.Euler(90, 90, 90);                // 오일러 -> 쿼터니언.
    }
    protected void SearchEnemy()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, attackRadius, searchMask);
        if(targets.Length > 0)
        {
            Collider pick = targets[Random.Range(0, targets.Length - 1)];
            Enemy enemy = pick.GetComponent<Enemy>();

            if(enemy != null)
                target = enemy;
        }
    }
    protected abstract void AttackEnemy();

    public void Setup(TowerData data)
    {
        attackPower = float.Parse(data.GetData(KEY_POWER));
        attackRate = float.Parse(data.GetData(KEY_ATKSPD));
        attackRadius = float.Parse(data.GetData(KEY_ATKRANGE));

        price = int.Parse(data.GetData(KEY_PRICE));
    }


#if UNITY_EDITOR

    private void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, searchRadius);

        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.up, attackRadius);
    }

#endif
}
