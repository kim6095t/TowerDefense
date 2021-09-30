using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] Image hpBarImage;

    private Transform target;
    private Camera mainCam;

    public void Setup(Transform target)
    {
        this.target = target;
        mainCam = Camera.main;
    }
    public void UpdateHp(float current, float max)
    {
        hpBarImage.fillAmount = current / max;
    }



    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // (월드)target의 좌표 -> (스크린)Hp Ui 좌표.
        Vector3 screenPosition = mainCam.WorldToScreenPoint(target.position);
        transform.position = screenPosition;
    }
}
