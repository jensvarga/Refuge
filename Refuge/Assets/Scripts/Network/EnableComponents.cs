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

    private void Start()
    {
        if (!hasAuthority) { return; }
        if (controller != null) controller.enabled = true;
        if (inputReader != null) inputReader.enabled = true;
        if (stateMachine != null) stateMachine.enabled = true;
        if (forceReciver != null) forceReciver.enabled = true;
        cameraRig?.SetActive(true);
    }
}
