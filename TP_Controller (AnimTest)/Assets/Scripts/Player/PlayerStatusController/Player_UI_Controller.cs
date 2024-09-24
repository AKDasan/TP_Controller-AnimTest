using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_UI_Controller : MonoBehaviour
{
    private P_status_Controller p_Status_Controller;

    [SerializeField] private Image HealthBar;
    [SerializeField] private Image RageBar;

    private float lerpSpeed;

    private void Start()
    { 
        p_Status_Controller = GetComponent<P_status_Controller>();

        lerpSpeed = 5f;
    }

    private void Update()
    {
        PlayerBarController();
    }

    void PlayerBarController()
    {
        HealthBar.fillAmount = Mathf.Lerp(HealthBar.fillAmount, p_Status_Controller.HealthValue / p_Status_Controller.MaxHealthValue, Time.deltaTime * lerpSpeed);
        RageBar.fillAmount = Mathf.Lerp(RageBar.fillAmount, p_Status_Controller.RageValue / p_Status_Controller.MaxRageValue, Time.deltaTime * lerpSpeed);
    }
}
