using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class EquipmentSO : ScriptableObject
{
    public string itemName;
    public int maxHealth, attack, defense, speed, wisdom;
    [SerializeField]
    private Sprite itemSprite;

    public void PreviewEquipment()
    {
        GameObject.Find("StatManager").GetComponent<PlayerStats>().
            PreviewEquipmentStats(maxHealth, attack, defense,speed,wisdom,itemSprite);
    }

    public void EquipItem()
    {
        PlayerStats playerStats = GameObject.Find("StatManager").GetComponent<PlayerStats>();
        playerStats.maxHealth += maxHealth;
        playerStats.attack += attack;
        playerStats.defense += defense;
        playerStats.speed += speed;
        playerStats.wisdom += wisdom;

        playerStats.UpdateEquipmentStats();
    }

    public void UnEquipItem()
    {
        PlayerStats playerStats = GameObject.Find("StatManager").GetComponent<PlayerStats>();
        playerStats.maxHealth -= maxHealth;
        playerStats.attack -= attack;
        playerStats.defense -= defense;
        playerStats.speed -= speed;
        playerStats.wisdom -= wisdom;

        playerStats.UpdateEquipmentStats();
    }
}
