using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MMONetworkManager : NetworkManager
{
    // [SerializeField] private GameObject playerRigPrefab = null;

    public override void OnClientConnect() {
        base.OnClientConnect();

        Debug.Log("New client connected");
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);

        NetworkPlayer player = conn.identity.GetComponent<NetworkPlayer>();
        player.SetDisplayName($"Player{numPlayers}");

        // GameObject playerRigInstance = Instantiate(playerRigPrefab, 
        //             conn.identity.transform.position, 
        //             conn.identity.transform.rotation);
        
        // NetworkServer.Spawn(playerRigInstance, conn);
    
        Debug.Log($"Player{numPlayers} joined the server");
    }
}
