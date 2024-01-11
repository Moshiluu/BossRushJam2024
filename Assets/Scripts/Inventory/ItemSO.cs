using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = new StatToChange();
    public int amountToChangeStat;
    public AttributeToChange attributeToChange = new AttributeToChange();
    public int amountToChangeAttribute;


    public bool UseItem()
    {
        return true;
    }
    public enum StatToChange
    {
        none, health, ammo
    };
    public enum AttributeToChange
    {
        none, strength, defense, intelligence, agility
    };
}