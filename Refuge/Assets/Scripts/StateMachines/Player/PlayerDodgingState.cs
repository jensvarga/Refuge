using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgingState : PlayerBaseState
{
  private readonly int DashBlendTreeHash = Animator.StringToHash("DashBlendTree");
  private readonly int DashForwardHash = Animator.StringToHash("DashForward");
  private readonly int DashRightHash = Animator.StringToHash("DashRight");

  private const float CrossFadeDuration = 0.1f;
  private Vector3 dodgingDirectionInput;
  private float remainingDodgeTime;

  public PlayerDodgingState(PlayerStateMachine stateMachine, Vector3 dodgingDirectionInput) : base(stateMachine)
  {
    this.dodgingDirectionInput = dodgingDirectionInput;
  }

  public override void Enter()
  {
    remainingDodgeTime = stateMachine.DodgeDuration;
    stateMachine.Health.SetInvulnerable(true);
    stateMachine.Animator.SetFloat(DashForwardHash, dodgingDirectionInput.y);
    stateMachine.Animator.SetFloat(DashRightHash, dodgingDirectionInput.x);
    stateMachine.Animator.CrossFadeInFixedTime(DashBlendTreeHash, CrossFadeDuration);
  }

  public override void Tick(float deltaTime)
  {
    Vector3 movement = new Vector3();
    movement += stateMachine.transform.right * dodgingDirectionInput.x * stateMachine.DodgeDistance / stateMachine.DodgeDuration;
    movement += stateMachine.transform.forward * dodgingDirectionInput.y * stateMachine.DodgeDistance / stateMachine.DodgeDuration;

    Move(movement, deltaTime);
    FaceTarget();

    remainingDodgeTime -= deltaTime;
    if (remainingDodgeTime <= 0f) {
      ReturnToLocomotion();
    }
  }

  public override void Exit()
  {
    stateMachine.Health.SetInvulnerable(false);
  }
}
