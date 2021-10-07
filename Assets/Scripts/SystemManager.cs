using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SystemManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] float volumn;

    void Update()
    {
        mixer.SetFloat("SE", volumn);

        // 보통은 팝업을 띄워서 재차 질문을 한다.
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();     // 게임 종료.
            if(Application.platform == RuntimePlatform.Android)
            {
                //..
            }
            else if(Application.platform == RuntimePlatform.IPhonePlayer)
            {
                //...
            }
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        // 유저가 잠시 게임 화면을 전환했을 때 일시정지 용도로 사용.
        Debug.Log("OnApplicationFocus : " + focus);
    }
    private void OnApplicationPause(bool pause)
    {
        Debug.Log("OnApplicationPause : " + pause);
    }
    private void OnApplicationQuit()
    {
        // 게임을 종료했을 때 호출되는 이벤트.
        // 보통 중간 저장같은 상황을 만든다.
        Debug.Log("OnApplicationQuit");
    }
}
