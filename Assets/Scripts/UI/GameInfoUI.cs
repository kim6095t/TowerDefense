using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfoUI : MonoBehaviour
{
    private static GameInfoUI instance;
    public static GameInfoUI Instance => instance;

    [SerializeField] Text hpText;

    private void Awake()
    {

        instance = this;
    }
    
    public void OnUpdateHp(int hp)
    {
        hpText.text = hp.ToString();
    }
   
}
