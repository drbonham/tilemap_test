using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ToolType
{
    Axe,
    PickAxe,
    Shovel,
    Hammer
}

public class Tool : MonoBehaviour {

    public string toolName;
    public ToolType toolType;
    public float workValue;
    public float toolRange;
    public bool selected;
    public Player player;
    public bool isCollding = false;
    // Simple find player object to set player for now, only one player should exist
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Check if we hit a gatherable object while swinging
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Obstacle obstacleHit = collision.GetComponentInParent<Obstacle>();
        if (obstacleHit != null && player.state == CharacterState.Slashing && !isCollding)
        {
            isCollding = true;
            Debug.Log("We have collider with an obstacle!");
            if (obstacleHit.gatherAble)
            {
                Debug.Log("The obstacle can be gathered, applying work!");
                obstacleHit.ApplyWork(workValue);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isCollding = false;
    }

    //public void TryGather()
    //{
    //    Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //    RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0f,LayerMask.GetMask("Gatherable"));
    //    Collider2D col = hit.collider;
    //    if (col != null)
    //    {
    //        Obstacle obstacleHit = col.GetComponentInParent<Obstacle>();
    //        if (obstacleHit != null)
    //        {
    //            Debug.Log("We've hit an obstacle game object!");
    //            if(obstacleHit.gatherAble && obstacleHit.gatherTool == toolType)
    //            {
    //                Debug.Log("We've hit a gatherable obstacle!");
    //                if(Vector2.Distance(
    //                    new Vector2(transform.position.x,transform.position.y),
    //                    new Vector2(col.transform.position.x,col.transform.position.y)) <= toolRange){
    //                    Debug.Log("We are in range to gather!");
    //                    obstacleHit.ApplyWork(workValue);
    //                }
    //            }
    //        }
    //    }
    //}
}
