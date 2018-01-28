using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObstacleSort : MonoBehaviour {

    List<GameObject> allObstacles;
    SpriteRenderer[] playerSRs;

	// Use this for initialization
	void Start () {
        allObstacles = new List<GameObject>();
        playerSRs = transform.parent.gameObject.GetComponentsInChildren<SpriteRenderer>();
    }

    void SetSpriteRenererOrder(int sortOrder)
    {
        foreach (SpriteRenderer sr in playerSRs)
        {
            if(sr.tag == "PlayerHead")
            {
                sr.sortingOrder = sortOrder + 1;
            }
            else
            {
                sr.sortingOrder = sortOrder;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Obstacle")
        {
            allObstacles.Add(collision.gameObject);
            collision.GetComponent<Obstacle>().SetTransparent(true);
            SetSpriteRenererOrder(collision.GetComponent<SpriteRenderer>().sortingOrder - 1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle")
        {
            allObstacles.Remove(collision.gameObject);
            collision.GetComponent<Obstacle>().SetTransparent(false);
            if(allObstacles.Count == 0)
            {
                SetSpriteRenererOrder(10);
            }
        }
    }
}
