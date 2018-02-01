using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayerConnectionObject : NetworkBehaviour {

    public GameObject playerPrefab;
	// Use this for initialization
	void Start () {
        if (!isLocalPlayer)
        {
            return;
        }
        CmdSpawnMyPlayer();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // Network commands
    [Command]
    void CmdSpawnMyPlayer()
    {
        // On the server, instantiate
        GameObject go = Instantiate(playerPrefab);

        // Send to all clients and set up networkidentity, set authority
        NetworkServer.SpawnWithClientAuthority(go,connectionToClient);
    }
}
