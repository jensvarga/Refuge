using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public abstract class StateMachine : NetworkBehaviour
{
  private State currentState;

    private void Update()
    {
      if (!hasAuthority) { return; }

      currentState?.Tick(Time.deltaTime);
    }

    public void SwitchState(State newState)
    {
      if (!hasAuthority) { return; }
      
      currentState?.Exit();
      currentState = newState;
      currentState?.Enter();
    }
}
