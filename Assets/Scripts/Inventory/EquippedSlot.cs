using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class EquippedSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Image slotImage;
    [SerializeField]
    private TMP_Text slotName;
    [SerializeField]
    private Image playerDisplayImage;

    [SerializeField]
    private ItemType itemType = new ItemType();

    private Sprite itemSprite;
    private string itemName;
    private string itemDescription;
    private MeshFilter filter;
    private Material material;

    private bool slotInUse;
    [SerializeField]
    public GameObject selectedShader;
    [SerializeField]
    public bool thisItemSelected;
    [SerializeField]
    private Sprite emptySprite;

    private InventoryManager inventoryManager;
    private EquipmentSOLibrary equipmentSOLibrary;


    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
        equipmentSOLibrary = GameObject.Find("InventoryCanvas").GetComponent<EquipmentSOLibrary>();
    }
    public void EquipGear(Sprite itemSprite, string itemName, string itemDescription, MeshFilter filter, Material material, ItemType itemType)
    {
        if(slotInUse)
        {
            UnEquipGear();
        }
        this.itemSprite = itemSprite;
        slotImage.sprite = this.itemSprite;
        slotName.enabled = false;

        this.itemType = itemType;
        this.itemName = itemName;
        this.itemDescription = itemDescription;
        this.filter = filter;
        this.material = material;

        playerDisplayImage.sprite = itemSprite;

        for (int i = 0; i < equipmentSOLibrary.equipmentSO.Length; i++)
        {
            if (equipmentSOLibrary.equipmentSO[i].itemName == this.itemName)
            {
                equipmentSOLibrary.equipmentSO[i].EquipItem();
            }
        }
        slotInUse = true;
    }

    public void UnEquipGear()
    {
        inventoryManager.DeselectAllSlots();
        inventoryManager.AddItem(itemName,1,itemSprite,itemDescription,filter,material,itemType);
        this.itemSprite = emptySprite;
        slotImage.sprite = this.emptySprite;
        slotName.enabled = true;

        for (int i = 0; i < equipmentSOLibrary.equipmentSO.Length; i++)
        {
            if (equipmentSOLibrary.equipmentSO[i].itemName == this.itemName)
            {
                equipmentSOLibrary.equipmentSO[i].UnEquipItem();
            }
        }
        playerDisplayImage.sprite = emptySprite;

        GameObject.Find("StatManager").GetComponent<PlayerStats>().TurnOffPreview();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }

    }

    void OnLeftClick()
    {
        if(thisItemSelected && slotInUse)
        {
            UnEquipGear();
        }
        else
        {
            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            thisItemSelected = true;
            for (int i = 0; i < equipmentSOLibrary.equipmentSO.Length; i++)
            {
                if (equipmentSOLibrary.equipmentSO[i].itemName == this.itemName)
                {
                    equipmentSOLibrary.equipmentSO[i].PreviewEquipment();
                }
            }
        }
    }

    void OnRightClick()
    {
        UnEquipGear();
    }
}
