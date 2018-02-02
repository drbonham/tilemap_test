using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Will probably seperate the actual inventory and UI components out later
public class PlayerInventory : MonoBehaviour {

    public static PlayerInventory _Instance;

    public int slotCnt;
    public PlayerInventorySlot slotPrefab;
    public Image inventoryPanel;

    //public List<InventoryItem> items = new List<InventoryItem>();
    public InventoryItem[] items; // Reference to the actual inventory items
    public PlayerInventorySlot[] allSlots; // Reference to the GUI item slots

    private void Awake()
    {
        // Singleton
        if (_Instance == null)
            _Instance = this;
    }

    private void Start()
    {
        items = new InventoryItem[slotCnt];
        for (int i = 0; i < slotCnt; i++)
        {
            items[i] = new InventoryItem(null,0);
        }
        // UI
        allSlots = new PlayerInventorySlot[slotCnt];
        AddSlots(slotCnt);
        inventoryPanel.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventoryPanel();
        }
        if(Input.GetKeyDown(KeyCode.Escape) && inventoryPanel.IsActive())
        {
            ToggleInventoryPanel();
        }
    }

    void ToggleInventoryPanel()
    {
        if (inventoryPanel.IsActive())
        {
            inventoryPanel.gameObject.SetActive(false);
        }
        else
        {
            inventoryPanel.gameObject.SetActive(true);
        }
    }

    // Rework all this later
    public bool AddItem(Item item)
    {
        bool wasAdded = false;
        if (item.stackable)
        {
            bool wasStacked = false;
            for (int i = 0; i < items.Length; i++)
            {
                if(items[i].item != null)
                {
                    if(items[i].item.name == item.name && items[i].count < item.maxStackSize)
                    {
                        wasStacked = true;
                        wasAdded = true;
                        items[i].count++;
                        break;
                    }
                }
            }
            if (!wasStacked)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    if (items[i].item == null)
                    {
                        wasAdded = true;
                        items[i] = new InventoryItem(item, 1);
                        break;
                    }
                }
                if(!wasAdded)
                    Debug.Log("Inventory is full!");
            }
        }
        else
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].item == null)
                {
                    wasAdded = true;
                    items[i] = new InventoryItem(item, 1);
                    break;
                }
            }
            if (!wasAdded)
                Debug.Log("Inventory is full!");
        }

        // Update ui after addding
        UpdateUI();
        return wasAdded;
    }

    // UI
    public void AddSlots(int cnt)
    {
        for (int i = 0; i < cnt; i++)
        {
            allSlots[i] = Instantiate(slotPrefab, inventoryPanel.transform);
        }
    }

    public void UpdateUI()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].item != null)
            {
                allSlots[i].itemIcon.sprite = items[i].item.icon;
                allSlots[i].itemIcon.color = Color.white;
                if (items[i].item.stackable)
                {
                    allSlots[i].itemCount.text = items[i].count.ToString();
                    allSlots[i].itemCount.color = Color.white;
                }
                else
                {
                    allSlots[i].itemCount.color = new Color(0, 0, 0, 0);
                }

            }
        }
    }

    [System.Serializable]
    public class InventoryItem
    {
        public Item item;
        public int count;

        public InventoryItem(Item item, int count)
        {
            this.item = item;
            this.count = count;
        }
    }
}
