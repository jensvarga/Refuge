using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPullupState : PlayerBaseState
{
  private readonly int PullupHash = Animator.StringToHash("Pullup");
  private readonly string PullupString = "Pullup";

  private const float CrossFadeDuration = 0.1f;
  private bool doFade = true;

  public PlayerPullupState(PlayerStateMachine stateMachine) : base(stateMachine) {}

  public override void Enter()
  {
    stateMachine.Animator.CrossFadeInFixedTime(PullupHash, CrossFadeDuration);
  }

  public override void Tick(float deltaTime)
  {
    // Wait until animation is done
    if (GetNormalizedTime(stateMachine.Animator, PullupString) < 1f) { return; }
    // Move transform the where the player ends up after animation
    stateMachine.Controller.enabled = false; // Work around for contoller not letting transform be moved
    stateMachine.transform.position = stateMachine.ClimbOffset.position;
    stateMachine.Controller.enabled = true;
    stateMachine.SwitchState(new PlayerFreeLookState(stateMachine, !doFade));
  }

  public override void Exit() {
    stateMachine.Controller.Move(Vector3.zero);
    stateMachine.ForceReciver.Reset();
  }
}
