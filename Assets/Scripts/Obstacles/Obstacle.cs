﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleType
{
    Tree,
    Rock
}

public class Obstacle : MonoBehaviour {

    SpriteRenderer sr;
    public float originalAlpha = 1f;
    public float transparentAlpha = 0.7f;
    public float health;
    public ObstacleType obstacleType;
    public bool gatherAble;
    public ToolType gatherTool;
    public Collider2D gatherCollider;
    public ItemPickup dropItem;
    public Vector2Int dropItemAmtRange;
    public Vector2 tilePlacementOffset = new Vector2(0.5f, 0.5f);
    Color originalColor;
    Color transparentColor;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        transparentColor = originalColor;
        transparentColor.a = transparentAlpha;
        transform.position = new Vector3(transform.position.x + tilePlacementOffset.x, 
            transform.position.y + tilePlacementOffset.y, transform.position.z);
    }

	public void SetTransparent(bool value)
    {
        if (value)
            sr.color = transparentColor;
        else
            sr.color = originalColor;
    }

    public void ApplyWork(float ammount)
    {
        health -= ammount;
        if (health <= 0)
            DeleteAndDropItems();
    }

    public void DeleteAndDropItems()
    {
        if (dropItem != null)
        {
            int ammountToDrop = Random.Range(dropItemAmtRange.x, dropItemAmtRange.y + 1);
            for (int i = 0; i < ammountToDrop; i++)
            {
                ItemPickup drop = Instantiate<ItemPickup>(dropItem);
                drop.transform.SetParent(transform.parent);
                drop.transform.position = new Vector3(transform.position.x + Random.Range(0f, 0.5f),
                    transform.position.y + Random.Range(0f, 0.5f), transform.position.z);
            }
        }
        Destroy(gameObject);
    }
}
