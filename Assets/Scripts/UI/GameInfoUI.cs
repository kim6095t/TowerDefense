using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfoUI : MonoBehaviour
{
    private static GameInfoUI instance;
    public static GameInfoUI Instance => instance;

    [SerializeField] Text hpText;
    [SerializeField] Text goldText;
    [SerializeField] Text waveText;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        hpText.text = GameManager.Instance.HP.ToString();
        goldText.text = GameManager.Instance.Gold.ToString();
        waveText.text = string.Format("Wave {0}", GameManager.Instance.Wave);
    }
}
