using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float hp;
    [SerializeField] Transform hpBarPivot;

    float maxHP;
    HpBar hpBar;

    private void Start()
    {
        maxHP = hp;
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
        Destroy(gameObject);
    }
}
