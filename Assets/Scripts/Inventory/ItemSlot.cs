using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerClickHandler
{
    public string itemName;
    public int quantity;
    public Sprite itemSprite;
    public bool isFull;
    public string itemDescription;
    public Sprite emptySprite;
    public MeshFilter itemFilter;
    public ItemType itemType;
    public Material itemMaterial;
    [SerializeField]
    private int maxNumberOfItems;

    [SerializeField]
    private TMP_Text quantityText;
    [SerializeField]
    private Image itemImage;

    public Image itemDescriptionImage;
    public TMP_Text ItemDescriptionTextName;
    public TMP_Text ItemDescriptionText;

    public GameObject selectedShader;
    public bool thisItemSelected;
    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("InventoryCanvas").GetComponent<InventoryManager>();
    }
    public int AddItem(string itemName, int quantity, Sprite itemSprite, string itemDescription, MeshFilter filter, Material material, ItemType itemType)
    {
        if(isFull)
        {
            return quantity;
        }
        this.itemType = itemType;
        this.itemMaterial = material;
        this.itemFilter = filter;
        this.itemName = itemName;
        this.itemSprite = itemSprite;
        itemImage.sprite = itemSprite;
        this.itemDescription = itemDescription;

        this.quantity += quantity;
        if (this.quantity >= maxNumberOfItems)
        {
            quantityText.text = maxNumberOfItems.ToString();
            quantityText.enabled = true;
            isFull = true;

            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            return extraItems;
        }
        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;

        return 0;

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

    public void OnLeftClick()
    {
        if (thisItemSelected)
        {

            bool usable = inventoryManager.UseItem(itemName);
            if (usable)
            {
                this.quantity -= 1;
                quantityText.text = this.quantity.ToString();
                if (this.quantity <= 0)
                {
                    EmptySlot();
                }
            }

        }
        else
        {
            inventoryManager.DeselectAllSlots();
            selectedShader.SetActive(true);
            thisItemSelected = true;

            ItemDescriptionTextName.text = itemName;
            ItemDescriptionText.text = itemDescription;
            itemDescriptionImage.sprite = itemSprite;

            if (itemDescriptionImage.sprite == null)
            {
                itemDescriptionImage.sprite = emptySprite;
            }
        }
        
    }

    private void EmptySlot()
    {
        quantityText.enabled = false;
        itemImage.sprite = emptySprite;

        ItemDescriptionTextName.text = "";
        ItemDescriptionText.text = "";
        itemDescriptionImage.sprite = emptySprite;
    }
    public void OnRightClick()
    {
        if (this.quantity > 0)
        {
            GameObject itemToDrop = new GameObject(itemName);
            Item newItem = itemToDrop.AddComponent<Item>();
            newItem.quantity = 1;
            newItem.itemName = itemName;
            newItem.meshFilter = itemFilter;
            newItem.material = itemMaterial;
            newItem.sprite = itemSprite;
            newItem.itemType = itemType;
            newItem.itemDescription = itemDescription;
            MeshFilter meshFilter = itemToDrop.AddComponent<MeshFilter>();
            meshFilter.mesh = itemFilter.mesh;
            MeshRenderer mr = itemToDrop.AddComponent<MeshRenderer>();
            mr.material = itemMaterial;
            mr.sortingOrder = 5;

            itemToDrop.AddComponent<Rigidbody>();
            itemToDrop.AddComponent<BoxCollider>();
            itemToDrop.transform.position = GameObject.FindWithTag("Player").transform.position + new Vector3(2.5f, 0, 0f);
            itemToDrop.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            this.quantity -= 1;
            quantityText.text = this.quantity.ToString();
            if (this.quantity <= 0)
            {
                EmptySlot();
            }
        }

    }
}
