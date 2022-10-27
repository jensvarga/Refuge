using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpawnRig : NetworkBehaviour
{
   [SerializeField] private GameObject characterPrefab;

   private GameObject characterInstance;

    #region Server
 
   [Command]
   private void CmdSpawnCharacter()
   {
        characterInstance = Instantiate(
            characterPrefab,
            transform.position,
            transform.rotation);
        
        NetworkServer.Spawn(characterInstance, connectionToClient);
   }

   #endregion

   #region Client

    private void Start() 
    {
        if (!hasAuthority) { return; }

        CmdSpawnCharacter();
    }

   #endregion
}
