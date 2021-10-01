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

    float currentFill;
    public void UpdateHp(float current, float max)
    {
        currentFill = current / max;
    }

    private void Start()
    {
        currentFill = 1f;
    }
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        // (¿ùµå)targetÀÇ ÁÂÇ¥ -> (½ºÅ©¸°)Hp Ui ÁÂÇ¥.
        Vector3 screenPosition = mainCam.WorldToScreenPoint(target.position);
        transform.position = screenPosition;

        hpBarImage.fillAmount = Mathf.Lerp(hpBarImage.fillAmount, currentFill, 5f * Time.deltaTime);
    }
}
