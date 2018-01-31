using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {

    public int slotCnt;
    public GameObject slotPrefab;

    Item[] items;

    private void Start()
    {
        AddSlots(slotCnt);
        items = new Item[slotCnt];
    }

    public void AddSlots(int cnt)
    {
        for (int i = 0; i < cnt; i++)
        {
            Instantiate(slotPrefab, transform);
        }
    }
}
