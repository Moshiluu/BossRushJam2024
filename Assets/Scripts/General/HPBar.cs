using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField]
    private Image currHealthBar;
    [SerializeField]
    private Image fullHealthBar;
    [SerializeField]
    Health health;

    float maxHP;

    void Start()
    {
        maxHP = health.HP;
        fullHealthBar.fillAmount = maxHP / 100;
    }

    // Update is called once per frame
    void Update()
    {
        currHealthBar.fillAmount = health.HP / 100;
    }
}
