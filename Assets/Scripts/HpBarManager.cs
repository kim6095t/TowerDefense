using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarManager : MonoBehaviour
{
    private static HpBarManager instance;
    public static HpBarManager Instance => instance;

    [SerializeField] HpBar hpBarPrefab;

    private void Awake()
    {
        instance = this;
    }

    public HpBar ConnectedHpBar(Transform target)
    {
        HpBar newHpBar = Instantiate(hpBarPrefab, transform);
        newHpBar.Setup(target);

        return newHpBar;
    }
}
