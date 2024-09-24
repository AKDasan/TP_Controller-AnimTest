using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_status_Controller : MonoBehaviour
{
    [SerializeField] private float healthValue;
    private float maxHealthValue;

    [SerializeField] private float rageValue;
    private float maxRageValue;

    // Public (getter)
    public float HealthValue { get { return healthValue; } }
    public float MaxHealthValue { get {return maxHealthValue; } }
    public float RageValue { get { return rageValue; } }
    public float MaxRageValue { get { return maxRageValue; } }

    private DamageController damageController;

    private void Start()
    {
        damageController = GetComponent<DamageController>();

        EnemyTakeDamageController.isDamaged += IncreaseRage;
        AnimController.RageActive += DecreaseRage;

        maxHealthValue = 100f;
        healthValue = 100f;

        maxRageValue = 100f;
        rageValue = 0f;
    }

    private void Update()
    {
        HealthandRageController();
    }

    void HealthandRageController()
    {
        if (healthValue < 0) { healthValue = 0; }
        if (healthValue > 100) { healthValue = 100; }

        if (rageValue < 0) {  rageValue = 0; }
        if (rageValue > 100) {  rageValue = 100; }
    }

    void DecreaseHealth(float value)
    {
        healthValue -= value;
    }

    void IncreaseHealth(float value) 
    {
        healthValue += value;
    }

    void DecreaseRage(float value)
    {
        rageValue -= value;
    }

    void IncreaseRage(float value)
    {
        if (!damageController.IsRageActive)
        {
            rageValue += value;
        }     
    }

    private void OnDisable()
    {
        EnemyTakeDamageController.isDamaged -= IncreaseRage;
        AnimController.RageActive -= DecreaseRage;
    }
}
