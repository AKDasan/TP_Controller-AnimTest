using System.Collections;
using UnityEngine;

public class Axe_Controller : MonoBehaviour
{
    [SerializeField] private GameObject back_Axe;
    [SerializeField] private GameObject hand_Axe;

    private void Start()
    {
        back_Axe.SetActive(true);
        hand_Axe.SetActive(false);
    }

    void AxeEquip()
    {
        back_Axe.SetActive(false);
        hand_Axe.SetActive(true);      
    }

    void AxeUnequip()
    {
        hand_Axe.SetActive(false);
        back_Axe.SetActive(true);
    }
}
