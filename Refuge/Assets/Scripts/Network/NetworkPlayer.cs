using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;

public class NetworkPlayer : NetworkBehaviour 
{
    [SyncVar(hook = nameof(HandleDisplayNameUpdated))] 
    [SerializeField]
    private string displayName = "Unnamed";

    #region Server

    [Server]
    public void SetDisplayName(string newDisplayName)
    {
        displayName = newDisplayName;
    }

    [Command]
    private void CmdSetDisplayName(string newDisplayName)
    {
        // Validate player name
        if (newDisplayName.Length < 2) { return; }
        if (newDisplayName.Length > 20) { return; }
        if (newDisplayName.Contains(" ")) { return; }

        RpcLogNewName(newDisplayName);
        SetDisplayName(newDisplayName);
    }

    #endregion

    #region Client

    private void HandleDisplayNameUpdated(string oldName, string newName)
    {
        Debug.Log($"New name: {newName}");
    }

    [ClientRpc]
    private void RpcLogNewName(string newDisplayName)
    {
        Debug.Log(newDisplayName);
    }

    #endregion

}

