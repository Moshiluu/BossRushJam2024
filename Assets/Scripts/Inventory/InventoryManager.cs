using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject inventoryMenu;
    public GameObject equipmentMenu;
    public ItemSlot[] itemSlot;
    public EquippedSlot[] equippedSlot;
    public ItemSO[] itemSO;
    public static bool inMenu = false;
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            Inventory();
        }
        if (Input.GetButtonDown("EquipmentMenu"))
        {
            Equipment();
        }
    }

    void Equipment()
    {
        if (equipmentMenu.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            inventoryMenu.SetActive(false);
            equipmentMenu.SetActive(false);
            inMenu = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            inventoryMenu.SetActive(false);
            equipmentMenu.SetActive(true);
            inMenu = true;
        }
    }
    void Inventory()
    {
        if (inventoryMenu.activeSelf)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            inventoryMenu.SetActive(false);
            equipmentMenu.SetActive(false);
            inMenu = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0;
            inventoryMenu.SetActive(true);
            equipmentMenu.SetActive(false);
            inMenu = true;
        }
    }
    public bool UseItem(string itemName)
    {
        for (int i = 0; i < itemSO.Length; i++)
        {
            if (itemSO[i].name == itemName)
            {
                bool usable = itemSO[i].UseItem();
                return usable;
            }
        }

        return false;
    }
    public int AddItem(string itemName, int quantity, Sprite sprite, string itemDescription, MeshFilter filter, Material material, ItemType itemType)
    {
        
        
            for (int i = 0; i < itemSlot.Length; i++)
            {
                if (itemSlot[i].isFull == false && itemSlot[i].itemName == itemName || itemSlot[i].quantity == 0)
                {
                    int leftOverItems = itemSlot[i].AddItem(itemName, quantity, sprite, itemDescription, filter, material, itemType);
                    if (leftOverItems > 0)
                    {
                        leftOverItems = AddItem(itemName, leftOverItems, sprite, itemDescription, filter, material, itemType);
                    }
                    return leftOverItems;
                }
            }

            return quantity;
        


        
    }

    public void DeselectAllSlots()
    {
        for (int i = 0;i < itemSlot.Length;i++)
        {
            itemSlot[i].selectedShader.SetActive(false);
            itemSlot[i].thisItemSelected = false;
        }
        for (int i = 0; i < equippedSlot.Length; i++)
        {
            equippedSlot[i].selectedShader.SetActive(false);
            equippedSlot[i].thisItemSelected = false;
        }
    }
}

public enum ItemType
{
    consumable,
    crafting,
    collectable,
    head,
    shirt,
    body,
    legs,
    mainHand,
    offHand,
    relic,
    feet,
    none
};
