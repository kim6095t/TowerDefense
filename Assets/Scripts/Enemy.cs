using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float maxHP;
    [SerializeField] Transform hpBarPivot;

    float hp;
    HpBar hpBar;

    private void Start()
    {
        // юс╫ц
        float addHp = (GameManager.Instance.Wave - 1) * 10;

        maxHP += addHp;
        hp = maxHP;

        hpBar = HpBarManager.Instance.ConnectedHpBar(hpBarPivot);
    }

    public void OnDamaged(float power)
    {
        if ((hp -= power) <= 0.0f)
        {
            OnDead();
        }

        hpBar.UpdateHp(hp, maxHP);
    }

    private void OnDead()
    {
        GameManager.Instance.OnGetGold(2);
        Destroy(gameObject);
    }
}
