using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth,attack, defense, speed, wisdom;
    [SerializeField]
    private TMP_Text maxHealthText,attackText, defenseText, speedText, wisdomText;
    [SerializeField]
    private TMP_Text maxHealthPreText, attackPreText, defensePreText, speedPreText, wisdomPreText;
    [SerializeField]
    private Image previewImage;
    [SerializeField]
    private GameObject selectedItemStats;
    [SerializeField]
    private GameObject selectedItemImage;
    void Start()
    {
        UpdateEquipmentStats();
    }

    public void UpdateEquipmentStats()
    {
        maxHealthText.text = maxHealth.ToString();
        attackText.text = attack.ToString();
        defenseText.text = defense.ToString();
        speedText.text = speed.ToString();
        wisdomText.text = wisdom.ToString();
    }

    public void PreviewEquipmentStats(int maxHealth, int attack, int defense, int speed, int wisdom, Sprite itemSprite)
    {

        maxHealthPreText.text = maxHealth.ToString();
        attackPreText.text = attack.ToString();
        defensePreText.text = defense.ToString();
        speedPreText.text = speed.ToString();
        wisdomPreText.text = wisdom.ToString();

        previewImage.sprite = itemSprite;

        selectedItemImage.SetActive(true);
        selectedItemStats.SetActive(true);
    }

    public void TurnOffPreview()
    {
        selectedItemImage.SetActive(false);
        selectedItemStats.SetActive(false);
    }
}
