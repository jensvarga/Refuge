using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class EnableComponents : NetworkBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private InputReader inputReader;
    [SerializeField] private PlayerStateMachine stateMachine;
    [SerializeField] private ForceReciver forceReciver;
    [SerializeField] private GameObject cameraRig;

    private void Start() {
        {
            if (!hasAuthority) { return; }
                controller.enabled = true;
                inputReader.enabled = true;
                stateMachine.enabled = true;
                forceReciver.enabled = true;
                cameraRig.SetActive(true);
        }
    }
}
