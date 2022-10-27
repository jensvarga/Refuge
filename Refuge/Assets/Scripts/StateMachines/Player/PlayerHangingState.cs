using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHangingState : PlayerBaseState
{
  private readonly int HangingHash = Animator.StringToHash("HangingIdle");

  private const float CrossFadeDuration = 0.05f;
  private Vector3 closestPoint;
  private Vector3 ledgeForward;

  public PlayerHangingState(PlayerStateMachine stateMachine, Vector3 closestPoint, Vector3 ledgeForward) : base(stateMachine)
  {
    this.closestPoint = closestPoint;
    this.ledgeForward = ledgeForward;
  }

  public override void Enter()
  {
    stateMachine.Controller.enabled = false;
    stateMachine.transform.position = closestPoint - (stateMachine.LedgeDetector.transform.position - stateMachine.transform.position);
    stateMachine.Controller.enabled = true;
    stateMachine.transform.rotation = Quaternion.LookRotation(ledgeForward, Vector3.up);
    stateMachine.Animator.CrossFadeInFixedTime(HangingHash, CrossFadeDuration);
  }

  public override void Tick(float deltaTime)
  {
    if (stateMachine.InputReader.MovementValue.y < 0f)
    {
      stateMachine.Controller.Move(Vector3.zero);
      stateMachine.ForceReciver.Reset();
      stateMachine.SwitchState(new PlayerFallingState(stateMachine));
    }
    else if (stateMachine.InputReader.MovementValue.y > 0f)
    {
      stateMachine.SwitchState(new PlayerPullupState(stateMachine));
    }
  }

  public override void Exit() {}
}
