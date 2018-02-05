using UnityEngine;

public class ItemPickup : MonoBehaviour {

    public float pickupRadius;
    public Item scriptableItem;
    CircleCollider2D col;

	// Use this for initialization
	void Start () {
        col = GetComponent<CircleCollider2D>();
        col.radius = pickupRadius;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void Pickup()
    {
        bool wasPickedUp = PlayerInventory._Instance.AddItem(scriptableItem);
        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Pickup();
        }
    }
}
